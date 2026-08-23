import { Typography, IconButton, Chip, Grid } from "@mui/material";
import Tooltip from "@mui/material/Tooltip";
import {
  DataGrid,
  type GridColDef,
  type GridRenderCellParams,
} from "@mui/x-data-grid";
import EditIcon from "@mui/icons-material/Edit";
import DoneIcon from "@mui/icons-material/Done";
import BlockIcon from "@mui/icons-material/Block";
import CheckCircleIcon from "@mui/icons-material/CheckCircle";
import PriorityHighIcon from "@mui/icons-material/PriorityHigh";
import HighlightOffIcon from "@mui/icons-material/HighlightOff";
import type { UserListItem } from "../../models/users/userListItem";
import {
  useGetUsersQuery,
  useActivateUserMutation,
  useDeactivateUserMutation,
} from "../../api/usersApi";
import LoadingIndicator from "../../components/generic/loadingIndicator";

const columns = (
  editUserCallback: (userId: string) => void,
  activateUser: (userId: string) => void,
  deactivateUser: (userId: string) => void,
): GridColDef<UserListItem>[] => {
  return [
    {
      field: "edit",
      headerName: "",
      width: 100,
      align: "left",
      editable: false,
      sortable: false,
      renderCell: (params: GridRenderCellParams<UserListItem>) => (
        <IconButton
          color="primary"
          onClick={() => editUserCallback(params.row.id)}
        >
          <EditIcon />
        </IconButton>
      ),
    },
    {
      field: "fullName",
      headerName: "Full name",
      width: 250,
      align: "left",
      editable: false,
      sortable: true,
    },
    {
      field: "username",
      headerName: "Username",
      width: 250,
      align: "left",
      editable: false,
      sortable: true,
    },
    {
      field: "email",
      headerName: "Email",
      width: 250,
      align: "left",
      editable: false,
      sortable: true,
    },
    {
      field: "status",
      headerName: "Status",
      width: 250,
      align: "left",
      editable: false,
      sortable: false,
      renderCell: (params: GridRenderCellParams<UserListItem>) => {
        const { isActive, deactivationDate } = params.row;

        if (isActive) {
          return (
            <Chip
              icon={<DoneIcon />}
              label="Active"
              variant="outlined"
              color="success"
            />
          );
        }

        if (deactivationDate === null) {
          return (
            <Chip
              icon={<PriorityHighIcon />}
              label="Pending activation"
              variant="outlined"
              color="warning"
            />
          );
        }

        return (
          <Chip
            icon={<HighlightOffIcon />}
            label="Deactivated"
            variant="outlined"
            color="error"
          />
        );
      },
    },
    {
      field: "deactivationDate",
      headerName: "Deactivation date",
      width: 250,
      align: "left",
      editable: false,
      sortable: false,
      renderCell: (params: GridRenderCellParams<UserListItem>) => {
        const { deactivationDate } = params.row;

        if (!deactivationDate) {
          return "N/A";
        }

        return deactivationDate.toLocaleDateString();
      },
    },
    {
      field: "actions",
      headerName: "Actions",
      width: 100,
      align: "left",
      editable: false,
      sortable: false,
      renderCell: (params: GridRenderCellParams<UserListItem>) => {
        const { isActive } = params.row;

        if (isActive) {
          return (
            <IconButton
              color="error"
              onClick={() => deactivateUser(params.row.userId)}
            >
              <Tooltip title="Deactivate User">
                <BlockIcon />
              </Tooltip>
            </IconButton>
          );
        }

        return (
          <IconButton
            color="success"
            onClick={() => activateUser(params.row.userId)}
          >
            <Tooltip title="Activate User">
              <CheckCircleIcon />
            </Tooltip>
          </IconButton>
        );
      },
    },
  ];
};

export default function UsersPage() {
  const { data, isLoading, error } = useGetUsersQuery();
  const [activateUser] = useActivateUserMutation();
  const [deactivateUser] = useDeactivateUserMutation();

  const editUser = (userId: string) => {
    console.log(`Edit user with ID: ${userId}`);
    // Implement the logic to edit the user here
  };

  if (isLoading) {
    <LoadingIndicator />;
  }

  if (error) {
    return <Typography>Error loading users</Typography>;
  }

  return (
    <Grid
      container
      alignItems="center"
      justifyContent="space-between"
      rowSpacing={2}
    >
      <Grid size={12}>
        <Typography variant="h4" gutterBottom>
          Users
        </Typography>
      </Grid>
      <Grid size={12}>
        <Typography variant="body1" sx={{ mb: "12px" }}>
          Here you can manage your users. You can add, edit, or delete users as
          needed.
        </Typography>
      </Grid>

      <Grid size={12}>
        <DataGrid
          getRowId={(row) => row.id}
          autoHeight={true}
          rows={data || []}
          columns={columns(editUser, activateUser, deactivateUser)}
          initialState={{
            pagination: {
              paginationModel: {
                pageSize: 20,
                page: 0,
              },
              rowCount: data?.length || 0,
            },
          }}
          pageSizeOptions={[20, 50, 100]}
          checkboxSelection={false}
          disableRowSelectionOnClick
          isCellEditable={() => false}
          isRowSelectable={() => false}
        />
      </Grid>
    </Grid>
  );
}
