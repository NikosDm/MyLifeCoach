import { Button, CircularProgress } from "@mui/material";
import { useAuth } from "../../hooks/useAuth";

interface LogoutButtonProps {
  variant?: "contained" | "outlined" | "text";
  size?: "small" | "medium" | "large";
  fullWidth?: boolean;
}

function LogoutButton({
  variant = "outlined",
  size = "medium",
  fullWidth = false,
}: LogoutButtonProps) {
  const { logout, isLoading } = useAuth();

  const handleLogout = async () => {
    try {
      await logout();
    } catch (error) {
      console.error("Logout failed:", error);
    }
  };

  return (
    <Button
      onClick={handleLogout}
      disabled={isLoading}
      variant={variant}
      size={size}
      fullWidth={fullWidth}
      startIcon={isLoading ? <CircularProgress size={20} /> : undefined}
    >
      {isLoading ? "Logging out..." : "Logout"}
    </Button>
  );
}

export default LogoutButton;
