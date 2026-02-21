import React, { createContext, useEffect, useState, useCallback } from "react";
import { User } from "oidc-client-ts";
import { userManager } from "../../auth/userManager";
import type { AuthContextType } from "../../auth/types";

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export { AuthContext };

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [user, setUser] = useState<User | null>(null);
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  // Initialize UserManager
  useEffect(() => {
    const initializeAuth = async () => {
      try {
        // Handle silent sign-in callback
        userManager.events.addUserLoaded((loadedUser) => {
          setUser(loadedUser);
          setIsAuthenticated(!loadedUser.expired);
          setError(null);
        });

        userManager.events.addUserUnloaded(() => {
          setUser(null);
          setIsAuthenticated(false);
        });

        userManager.events.addAccessTokenExpiring(() => {
          console.warn("Access token expiring, attempting silent renew");
        });

        userManager.events.addAccessTokenExpired(() => {
          console.warn("Access token expired");
          setUser(null);
          setIsAuthenticated(false);
        });

        userManager.events.addUserSignedOut(() => {
          setUser(null);
          setIsAuthenticated(false);
        });

        userManager.events.addSilentRenewError((error) => {
          console.error("Silent renew error:", error);
        });

        // Check if user is already logged in
        const currentUser = await userManager.getUser();
        if (currentUser && !currentUser.expired) {
          setUser(currentUser);
          setIsAuthenticated(true);
        } else {
          setUser(null);
          setIsAuthenticated(false);
        }
      } catch (err) {
        const errorMessage =
          err instanceof Error
            ? err.message
            : "Failed to initialize authentication";
        setError(errorMessage);
        console.error("Auth initialization error:", err);
      } finally {
        setIsLoading(false);
      }
    };

    initializeAuth();
  }, []);

  const login = useCallback(async () => {
    try {
      setError(null);
      await userManager.signinRedirect();
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : "Login failed";
      setError(errorMessage);
      console.error("Login error:", err);
      throw err;
    }
  }, []);

  const logout = useCallback(async () => {
    try {
      setError(null);
      await userManager.signoutRedirect();
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : "Logout failed";
      setError(errorMessage);
      console.error("Logout error:", err);
      throw err;
    }
  }, []);

  const getAccessToken = useCallback(() => {
    return user?.access_token || null;
  }, [user]);

  const refreshUser = useCallback(async () => {
    try {
      const currentUser = await userManager.getUser();
      if (currentUser && !currentUser.expired) {
        setUser(currentUser);
        setIsAuthenticated(true);
      } else {
        setUser(null);
        setIsAuthenticated(false);
      }
      setError(null);
    } catch (err) {
      const errorMessage =
        err instanceof Error ? err.message : "Failed to refresh user";
      setError(errorMessage);
      console.error("User refresh error:", err);
    }
  }, []);

  const value: AuthContextType = {
    user,
    isAuthenticated,
    isLoading,
    error,
    login,
    logout,
    getAccessToken,
    refreshUser,
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}
