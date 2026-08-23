import Box from "@mui/material/Box";
import List from "@mui/material/List";
import ListItem from "@mui/material/ListItem";
import ListItemButton from "@mui/material/ListItemButton";
import ListItemIcon from "@mui/material/ListItemIcon";
import ListItemText from "@mui/material/ListItemText";
import OutlinedFlagIcon from "@mui/icons-material/OutlinedFlag";
import FlagIcon from "@mui/icons-material/Flag";
import PeopleIcon from "@mui/icons-material/People";
import { Drawer } from "@mui/material";
import { NavLink } from "react-router-dom";
import {
  GOAL_TYPES_ROUTE,
  GOALS_ROUTE,
  USERS_ROUTE,
} from "../constants/routeConstants";

const navStyle = {
  color: "inherit",
  textDecoration: "none",
  typography: "h6",
  "&:hover": {
    color: "grey.500",
  },
  "&.active": {
    color: "text.secondary",
  },
};

interface SidebarMenuListProps {
  open: boolean;
  toggleDrawer: () => void;
}

export default function SidebarMenuList(props: SidebarMenuListProps) {
  const { open, toggleDrawer } = props;

  return (
    <Drawer anchor="left" open={open} onClose={toggleDrawer}>
      <Box sx={{ width: 250 }} role="presentation" onClick={toggleDrawer}>
        <List>
          <ListItem
            component={NavLink}
            to={GOALS_ROUTE}
            sx={navStyle}
            disablePadding
          >
            <ListItemButton>
              <ListItemIcon>
                <OutlinedFlagIcon />
              </ListItemIcon>
              <ListItemText primary="Goals" />
            </ListItemButton>
          </ListItem>
          <ListItem
            component={NavLink}
            to={GOAL_TYPES_ROUTE}
            sx={navStyle}
            disablePadding
          >
            <ListItemButton>
              <ListItemIcon>
                <FlagIcon />
              </ListItemIcon>
              <ListItemText primary="Goal Types" />
            </ListItemButton>
          </ListItem>
          <ListItem
            component={NavLink}
            to={USERS_ROUTE}
            disablePadding
            sx={navStyle}
          >
            <ListItemButton>
              <ListItemIcon>
                <PeopleIcon />
              </ListItemIcon>
              <ListItemText primary="Users" />
            </ListItemButton>
          </ListItem>
        </List>
      </Box>
    </Drawer>
  );
}
