import { User } from "oidc-client-ts";

export interface AuthContextType {
  user: User | null;
  isAuthenticated: boolean;
  isLoading: boolean;
  role: string | null;
  hasRole: (role: string) => boolean;
  isActive: boolean;
  error: string | null;
  login: () => Promise<void>;
  logout: () => Promise<void>;
  getAccessToken: () => string | null;
  refreshUser: () => Promise<void>;
}

export interface ApiErrorResponse {
  statusCode: number;
  message: string;
  details?: Record<string, unknown>;
}
