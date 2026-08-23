import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import type { UserListItem } from "../models/users/userListItem";
import { userManager } from "../auth/userManager";
import { API_URLS } from "../auth/config";

export const usersApi = createApi({
  reducerPath: "usersApi",
  baseQuery: fetchBaseQuery({
    baseUrl: API_URLS.PROFILES_API,
    prepareHeaders: async (headers) => {
      try {
        const user = await userManager.getUser();

        if (user && user.access_token) {
          headers.set("Authorization", `Bearer ${user.access_token}`);
        }
      } catch (error) {
        console.error("Error getting access token:", error);
      }

      headers.set("Content-Type", "application/json");
      return headers;
    },
  }),
  tagTypes: ["Users"],
  endpoints: (builder) => ({
    // TODO: Create new endpoint for getting users. No point loading all personal profiles when we just need a list of users.
    getUsers: builder.query<UserListItem[], void>({
      query: () => ({ url: "/personal-profiles" }),
      providesTags: (result) =>
        result
          ? [
              ...result.map(({ userId }) => ({
                type: "Users" as const,
                id: userId,
              })),
              { type: "Users", id: "LIST" },
            ]
          : [{ type: "Users", id: "LIST" }],
    }),
    activateUser: builder.mutation<void, string>({
      query: (userId) => ({
        url: `/users/${userId}`,
        method: "PUT",
        body: { setActive: true },
      }),
      invalidatesTags: () => [{ type: "Users", id: "LIST" }],
    }),
    deactivateUser: builder.mutation<void, string>({
      query: (userId) => ({
        url: `/users/${userId}`,
        method: "PUT",
        body: { setActive: false },
      }),
      invalidatesTags: () => [{ type: "Users", id: "LIST" }],
    }),
  }),
});

export const {
  useGetUsersQuery,
  useActivateUserMutation,
  useDeactivateUserMutation,
} = usersApi;
