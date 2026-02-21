import { BrowserRouter, Route, Routes } from "react-router-dom";
import App from "../layout/App";
import RootPage from "../pages/root/rootPage";
import { SignInCallback } from "../auth";

function Root() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/signin-oidc" element={<SignInCallback />} />
        <Route element={<App />}>
          <Route path="/" element={<RootPage />} />
          {/* Additional routes can be added here */}
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default Root;
