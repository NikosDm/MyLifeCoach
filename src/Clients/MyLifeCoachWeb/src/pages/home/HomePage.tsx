import { ADMIN_ROLE, USER_ROLE } from "../../constants/userConstants";
import { Box, Grid, Typography } from "@mui/material";
import { useAuth } from "../../hooks";

export default function HomePage() {
  const { user, isAuthenticated, isActive, role } = useAuth();

  const getGreeting = (): string => {
    if (!isAuthenticated) {
      return "Please log in to access your personalized coaching experience.";
    }

    if (!isActive) {
      return "Your account is not active. Please contact support.";
    }

    switch (role) {
      case ADMIN_ROLE:
        return "Manage your coaching platform and support your users.";
      case USER_ROLE:
        return "Here you can set your own profiles, set goals and improve your life in every aspect.";
      default:
        return "Explore your coaching dashboard, set goals, and track your progress.";
    }
  };

  return (
    <Grid
      container
      justifyContent="center"
      alignItems="center"
      spacing={2}
      sx={{ mb: 4 }}
    >
      <Box component="section">
        <Typography align="center" alignContent="center" variant="h4">
          Welcome to My Life Coach{" "}
          {isAuthenticated ? (
            <u>{user?.profile?.name || user?.profile?.email || "User"}</u>
          ) : (
            ""
          )}
          !
        </Typography>
        <br />
        <Typography align="center" alignContent="center" variant="h6">
          {getGreeting()}
        </Typography>
      </Box>
    </Grid>
  );
}
