import { BrowserRouter, Route, Routes } from "react-router-dom";
import App from "../layout/App";
import HomePage from "../pages/home/HomePage";
import { SignInCallback } from "../auth";
import RequireAuth from "./RequireAuth";
import UsersPage from "../pages/users/usersPage";
import {
  GOAL_TYPES_ROUTE,
  GOALS_ROUTE,
  USERS_ROUTE,
} from "../constants/routeConstants";
import GoalsPage from "../pages/goals/goalsPage";
import GoalTypesOuterContainer from "../pages/goalTypes/goalTypesOuterContainer";

export default function Router() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/signin-oidc" element={<SignInCallback />} />
        <Route path="/" element={<App />}>
          <Route path="" element={<HomePage />} />
          <Route element={<RequireAuth requiresAdmin={true} />}>
            <Route path={USERS_ROUTE} element={<UsersPage />} />
            <Route
              path={GOAL_TYPES_ROUTE}
              element={<GoalTypesOuterContainer />}
            />
          </Route>
          <Route element={<RequireAuth requiresAdmin={false} />}>
            <Route path={GOALS_ROUTE} element={<GoalsPage />} />
          </Route>
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
