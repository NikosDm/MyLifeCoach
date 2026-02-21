import { Button, CircularProgress } from "@mui/material";
import { useAuth } from "../../hooks/useAuth";

interface LoginButtonProps {
  variant?: "contained" | "outlined" | "text";
  size?: "small" | "medium" | "large";
  fullWidth?: boolean;
}

function LoginButton({
  variant = "contained",
  size = "medium",
  fullWidth = false,
}: LoginButtonProps) {
  const { login, isLoading } = useAuth();

  const handleLogin = async () => {
    try {
      await login();
    } catch (error) {
      console.error("Login failed:", error);
    }
  };

  return (
    <Button
      onClick={handleLogin}
      disabled={isLoading}
      variant={variant}
      size={size}
      fullWidth={fullWidth}
      startIcon={isLoading ? <CircularProgress size={20} /> : undefined}
    >
      {isLoading ? "Logging in..." : "Login"}
    </Button>
  );
}

export default LoginButton;
