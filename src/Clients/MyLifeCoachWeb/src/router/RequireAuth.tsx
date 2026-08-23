import { Navigate, Outlet, useLocation } from "react-router-dom";
import { useAuth } from "../hooks";
import { ADMIN_ROLE } from "../constants/userConstants";

interface RequireAuthProps {
  requiresAdmin: boolean;
}

export default function RequireAuth({ requiresAdmin }: RequireAuthProps) {
  const { isAuthenticated, isActive, hasRole } = useAuth();
  const location = useLocation();

  if (requiresAdmin && !hasRole(ADMIN_ROLE)) {
    return <Navigate to="/" state={{ from: location }} />;
  }

  if (!isAuthenticated) {
    return <Navigate to="/login" state={{ from: location }} />;
  }

  if (!isActive) {
    return <Navigate to="/" state={{ from: location }} />;
  }

  return <Outlet />;
}
