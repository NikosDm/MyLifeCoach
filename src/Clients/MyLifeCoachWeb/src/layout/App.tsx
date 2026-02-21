import {
  Container,
  CssBaseline,
  ThemeProvider,
  Typography,
  createTheme,
} from "@mui/material";
import { Outlet } from "react-router-dom";
import { Box, CircularProgress } from "@mui/material";
import NavBar from "./NavBar";
import { useAuth } from "../hooks/useAuth";
import { useState } from "react";
import RootPage from "../pages/root/rootPage";

export default function App() {
  const { isLoading } = useAuth();

  const [darkMode, setDarkMode] = useState(false);
  const palleteType = darkMode ? "dark" : "light";
  const theme = createTheme({
    palette: {
      mode: palleteType,
      background: {
        default: palleteType === "light" ? "#eaeaea" : "#121212",
      },
    },
  });

  if (isLoading) {
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

  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <NavBar
        darkMode={darkMode}
        handleThemeChange={() => setDarkMode(!darkMode)}
      />
      {isLoading ? (
        <Typography variant="h6" sx={{ mt: 4, textAlign: "center" }}>
          Initialising app...
        </Typography>
      ) : location.pathname === "/" ? (
        <RootPage />
      ) : (
        <Container sx={{ mt: 4 }}>
          <Outlet />
        </Container>
      )}
    </ThemeProvider>
  );
}
