import type { UserManagerSettings } from "oidc-client-ts";

const AUTHORITY =
  import.meta.env.VITE_AUTH_AUTHORITY || "http://localhost:3010";
const CLIENT_ID = import.meta.env.VITE_AUTH_CLIENT_ID || "my-life-coach-web";
// const CLIENT_SECRET =
//   import.meta.env.VITE_AUTH_CLIENT_SECRET || "ThisIsASecretToBeKeptHere123";
const REDIRECT_URI =
  import.meta.env.VITE_AUTH_REDIRECT_URI || "http://localhost:9000/signin-oidc";
const POST_LOGOUT_REDIRECT_URI =
  import.meta.env.VITE_AUTH_POST_LOGOUT_REDIRECT_URI ||
  "http://localhost:9000/";
const SCOPES =
  import.meta.env.VITE_AUTH_SCOPES || "openid profile goals-api profiles-api";

export const oidcConfig: UserManagerSettings = {
  authority: AUTHORITY,
  client_id: CLIENT_ID,
  // client_secret: CLIENT_SECRET,
  redirect_uri: REDIRECT_URI,
  post_logout_redirect_uri: POST_LOGOUT_REDIRECT_URI,
  response_type: "code",
  scope: SCOPES,
  automaticSilentRenew: true,
  monitorSession: false,
  loadUserInfo: true,
  revokeTokensOnSignout: true,
};

export const API_URLS = {
  GOALS_API: import.meta.env.VITE_GOALS_API_URL || "http://localhost:3000",
  PROFILES_API:
    import.meta.env.VITE_PROFILES_API_URL || "http://localhost:3001",
};
