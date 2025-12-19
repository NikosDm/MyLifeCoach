import { Container, Typography } from "@mui/material";
import NavBar from "./NavBar";

export default function App() {
  return (
    <>
      <NavBar />
      <Container maxWidth="xl" sx={{ mt: 16 }}>
        <Typography variant="h4">Welcome to My Life Coach!</Typography>
      </Container>
    </>
  );
}
