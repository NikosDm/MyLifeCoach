import { Chip, IconButton } from "@mui/material";
import {
  DataGrid,
  type GridColDef,
  type GridRenderCellParams,
} from "@mui/x-data-grid";
import type { GoalTypeResponse } from "../../models/goalTypes/responses/goalTypeReponse";
import EditIcon from "@mui/icons-material/Edit";
import LoadingIndicator from "../../components/generic/loadingIndicator";

const gridColumns = (
  editGoalType: (goalType: GoalTypeResponse) => void,
): GridColDef<GoalTypeResponse>[] => [
  {
    field: "edit",
    headerName: "",
    width: 100,
    align: "left",
    editable: false,
    sortable: false,
    renderCell: (params: GridRenderCellParams<GoalTypeResponse>) => (
      <IconButton color="primary" onClick={() => editGoalType(params.row)}>
        <EditIcon />
      </IconButton>
    ),
  },
  {
    field: "name",
    headerName: "Name",
    width: 250,
    align: "left",
    editable: false,
    sortable: true,
  },
  {
    field: "description",
    headerName: "Description",
    width: 500,
    align: "left",
    editable: false,
    sortable: true,
  },
  {
    field: "isActive",
    headerName: "Active",
    width: 150,
    align: "left",
    editable: false,
    sortable: true,
    renderCell: (params: GridRenderCellParams<GoalTypeResponse>) =>
      params.value ? (
        <Chip label="Active" variant="outlined" color="success" />
      ) : (
        <Chip label="Inactive" variant="outlined" color="warning" />
      ),
  },
];

export interface GoalTypesDataGridProps {
  isLoading: boolean;
  data: GoalTypeResponse[];
  editGoalType: (goalType: GoalTypeResponse) => void;
}

export default function GoalTypesDataGrid(props: GoalTypesDataGridProps) {
  const { isLoading, data, editGoalType } = props;

  if (isLoading) {
    return <LoadingIndicator />;
  }

  return (
    <DataGrid
      getRowId={(row) => row.id}
      autoHeight={true}
      rows={data || []}
      columns={gridColumns(editGoalType)}
      initialState={{
        pagination: {
          paginationModel: {
            pageSize: 10,
            page: 0,
          },
          rowCount: data?.length || 0,
        },
      }}
      pageSizeOptions={[10, 20, 50, 100]}
      checkboxSelection={false}
      disableRowSelectionOnClick
      isCellEditable={() => false}
      isRowSelectable={() => false}
    />
  );
}
