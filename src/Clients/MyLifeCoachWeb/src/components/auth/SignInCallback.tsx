import { useEffect, useState, useRef } from "react";
import { useNavigate } from "react-router-dom";
import { userManager } from "../../auth/userManager";
import { useAuth } from "../../hooks/useAuth";
import { Box, CircularProgress, Alert } from "@mui/material";

function SignInCallback() {
  const navigate = useNavigate();
  const { refreshUser } = useAuth();
  const [error, setError] = useState<string | null>(null);
  const processedRef = useRef(false);

  useEffect(() => {
    // Prevent multiple calls to signinRedirectCallback
    if (processedRef.current) return;
    processedRef.current = true;

    const handleCallback = async () => {
      try {
        await userManager.signinRedirectCallback();

        // Refresh auth state with the newly authenticated user
        await refreshUser();

        // Redirect to home or intended page
        navigate("/", { replace: true });
      } catch (err) {
        // Don't log errors for abort/cancel scenarios
        if (
          err instanceof Error &&
          err.name === "Error" &&
          err.message === "Network request failed"
        ) {
          // Retry once for network errors
          console.warn("Network error during signin, will retry");
          return;
        }

        const errorMessage =
          err instanceof Error ? err.message : "Authentication callback failed";
        setError(errorMessage);
        console.error("Sign-in callback error:", err);

        // Redirect to login page after 1 second
        setTimeout(() => {
          navigate("/login", { replace: true });
        }, 1000);
      }
    };

    handleCallback();
  }, [navigate, refreshUser]);

  if (error) {
    return (
      <Box
        sx={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          minHeight: "100vh",
          padding: 2,
        }}
      >
        <Alert severity="error">{error}. Redirecting to login...</Alert>
      </Box>
    );
  }

  return (
    <Box
      sx={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        minHeight: "100vh",
      }}
    >
      <CircularProgress />
    </Box>
  );
}

export default SignInCallback;
