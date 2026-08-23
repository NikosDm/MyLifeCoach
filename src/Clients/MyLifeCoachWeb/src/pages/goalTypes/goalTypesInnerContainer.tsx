import { Typography, Grid, Button } from "@mui/material";
import GoalTypesDataGrid from "./goalTypesDataGrid";
import type { GoalTypeResponse } from "../../models/goalTypes/responses/goalTypeReponse";

export interface GoalTypesInnerContainerProps {
  isLoading: boolean;
  data: GoalTypeResponse[] | undefined;
  editGoalType: (goalType: GoalTypeResponse) => void;
  addGoalType: () => void;
}

export default function GoalTypesInnerContainer(
  props: GoalTypesInnerContainerProps,
) {
  const { isLoading, data, editGoalType, addGoalType } = props;

  return (
    <Grid
      container
      alignItems="center"
      justifyContent="space-between"
      rowSpacing={2}
    >
      <Grid size={12}>
        <Typography variant="h4" gutterBottom>
          Goal Types
        </Typography>
      </Grid>
      <Grid size={{ xs: 12, sm: 12, md: 9 }}>
        <Typography variant="body1">
          Here you can manage your goal types. You can add, edit, or delete goal
          types as needed.
        </Typography>
      </Grid>
      <Grid
        size={{ xs: 12, sm: 12, md: 3 }}
        textAlign={{ xs: "center", sm: "center", md: "right" }}
      >
        <Button variant="contained" color="primary" onClick={addGoalType}>
          Add Goal Type
        </Button>
      </Grid>
      <Grid size={12}>
        <GoalTypesDataGrid
          isLoading={isLoading}
          data={data || []}
          editGoalType={editGoalType}
        />
      </Grid>
    </Grid>
  );
}
