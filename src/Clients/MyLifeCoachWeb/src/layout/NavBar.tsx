import {
  AppBar,
  Button,
  IconButton,
  Toolbar,
  Typography,
  Box,
  Menu,
  MenuItem,
  Switch,
} from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import { useState } from "react";
import { useAuth } from "../hooks/useAuth";
import { LoginButton, LogoutButton } from "../auth/index.ts";
import { NavLink } from "react-router-dom";
import SidebarMenuList from "./SidebarMenuList.tsx";

const navStyle = {
  color: "inherit",
  textDecoration: "none",
  typography: "h6",
  "&:hover": {
    color: "grey.500",
  },
};

interface HeaderProps {
  darkMode: boolean;
  handleThemeChange: () => void;
}

export default function NavBar(props: HeaderProps) {
  const { darkMode, handleThemeChange } = props;
  const { isAuthenticated, isActive, user } = useAuth();
  const [openSidebar, setOpenSidebar] = useState(false);
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);

  const handleMenuOpen = (event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
  };

  const onClickMenu = () => {
    if (!isAuthenticated || !isActive) {
      return;
    }

    setOpenSidebar(!openSidebar);
  };

  return (
    <AppBar position="fixed">
      <SidebarMenuList
        open={openSidebar}
        toggleDrawer={() => setOpenSidebar(false)}
      />
      <Toolbar>
        <IconButton
          size="large"
          edge="start"
          color="inherit"
          aria-label="menu"
          onClick={onClickMenu}
          sx={{ mr: 2 }}
        >
          <MenuIcon />
        </IconButton>
        <Box display="flex" alignItems="center">
          <Typography variant="h6" component={NavLink} to="/" sx={navStyle}>
            My Life Coach
          </Typography>
          <Switch checked={darkMode} onChange={handleThemeChange} />
        </Box>

        {isAuthenticated ? (
          <Box display="flex" justifyContent="flex-end" flexGrow={1}>
            <Button
              id="account-button"
              aria-controls={open ? "account-menu" : undefined}
              aria-haspopup="true"
              aria-expanded={open ? "true" : undefined}
              onClick={handleMenuOpen}
              color="inherit"
              startIcon={<AccountCircleIcon />}
            >
              {user?.profile?.name || "Account"}
            </Button>
            <Menu
              id="account-menu"
              anchorEl={anchorEl}
              open={open}
              onClose={handleMenuClose}
              anchorOrigin={{
                vertical: "bottom",
                horizontal: "right",
              }}
              transformOrigin={{
                vertical: "top",
                horizontal: "right",
              }}
            >
              <MenuItem>
                <Button variant="text" size="small">
                  My Account
                </Button>
              </MenuItem>
              <MenuItem onClick={handleMenuClose}>
                <LogoutButton variant="text" size="small" fullWidth />
              </MenuItem>
            </Menu>
          </Box>
        ) : (
          <Box display="flex" justifyContent="flex-end" flexGrow={1}>
            <LoginButton variant="contained" size="small" />
          </Box>
        )}
      </Toolbar>
    </AppBar>
  );
}
