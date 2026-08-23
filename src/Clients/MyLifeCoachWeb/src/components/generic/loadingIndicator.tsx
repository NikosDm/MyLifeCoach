import { CircularProgress, Grid } from "@mui/material";

export default function LoadingIndicator() {
  return (
    <Grid
      container
      justifyContent="center"
      style={{ height: "100%", marginTop: "2.5rem" }}
    >
      <CircularProgress color="secondary" aria-label="Loading…" />
    </Grid>
  );
}
