import axios, {
  type AxiosInstance,
  type InternalAxiosRequestConfig,
} from "axios";
import { userManager } from "./userManager";

const createApiClient = (baseURL: string): AxiosInstance => {
  const client = axios.create({
    baseURL,
    timeout: 10000,
    headers: {
      "Content-Type": "application/json",
    },
  });

  // Request interceptor: Add Bearer token to requests
  client.interceptors.request.use(
    async (config: InternalAxiosRequestConfig) => {
      try {
        const user = await userManager.getUser();

        if (user && user.access_token) {
          config.headers.Authorization = `Bearer ${user.access_token}`;
        }
      } catch (error) {
        console.error("Error getting access token:", error);
      }

      return config;
    },
    (error) => Promise.reject(error),
  );

  // Response interceptor: Handle token expiration and errors
  client.interceptors.response.use(
    (response) => response,
    async (error) => {
      const originalRequest = error.config;

      // If 401 Unauthorized and haven't retried yet, try silent renew
      if (error.response?.status === 401 && !originalRequest._retry) {
        originalRequest._retry = true;

        try {
          const user = await userManager.signinSilent();

          if (user && user.access_token) {
            originalRequest.headers.Authorization = `Bearer ${user.access_token}`;
            return client(originalRequest);
          }
        } catch (renewError) {
          console.error("Silent sign-in failed:", renewError);
          // Token renewal failed, user needs to re-authenticate
          await userManager.signoutRedirect();
        }
      }

      // Handle other HTTP errors
      if (error.response) {
        console.error(
          `API Error [${error.response.status}]:`,
          error.response.data,
        );
      } else if (error.request) {
        console.error("No response received:", error.request);
      } else {
        console.error("Request error:", error.message);
      }

      return Promise.reject(error);
    },
  );

  return client;
};

export default createApiClient;
