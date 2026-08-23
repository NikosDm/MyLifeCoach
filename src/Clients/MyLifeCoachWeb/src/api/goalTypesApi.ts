import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { API_URLS } from "../auth/config";
import { userManager } from "../auth/userManager";
import type { GoalTypeResponse } from "../models/goalTypes/responses/goalType";
import type {
  CreateGoalTypeRequest,
  UpdateGoalTypeRequest,
} from "../models/goalTypes/requests/createGoalTypeRequest";

export const goalTypesApi = createApi({
  reducerPath: "goalTypesApi",
  baseQuery: fetchBaseQuery({
    baseUrl: API_URLS.GOALS_API,
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
  tagTypes: ["GoalTypes"],
  endpoints: (builder) => ({
    getGoalTypes: builder.query<GoalTypeResponse[], void>({
      query: () => ({ url: "/goal-types" }),
      providesTags: (result) =>
        result
          ? [
              ...result.map(({ id }) => ({ type: "GoalTypes" as const, id })),
              { type: "GoalTypes", id: "LIST" },
            ]
          : [{ type: "GoalTypes", id: "LIST" }],
      transformErrorResponse: (response) => {
        console.error("Error fetching goal types:", response);
        return response;
      },
    }),
    createGoalType: builder.mutation<GoalTypeResponse, CreateGoalTypeRequest>({
      query: (request) => ({
        url: "/goal-types",
        method: "POST",
        body: request,
      }),
      invalidatesTags: (result, error) =>
        error ? [] : result ? [{ type: "GoalTypes", id: "LIST" }] : [],
      transformErrorResponse: (response) => {
        console.error("Error fetching goal types:", response);
        return response;
      },
    }),
    updateGoalType: builder.mutation<GoalTypeResponse, UpdateGoalTypeRequest>({
      query: (request) => ({
        url: `/goal-types/${request.id}`,
        method: "PUT",
        body: request,
      }),
      invalidatesTags: (result, error) =>
        error
          ? []
          : result
            ? [{ type: "GoalTypes", id: result.id }]
            : [{ type: "GoalTypes", id: "LIST" }],
      transformErrorResponse: (response) => {
        console.error("Error fetching goal types:", response);
        return response;
      },
    }),
    deleteGoalType: builder.mutation<void, string>({
      query: (id) => ({
        url: `/goal-types/${id}`,
        method: "DELETE",
      }),
      invalidatesTags: (result, error) =>
        error ? [] : result ? [{ type: "GoalTypes", id: "LIST" }] : [],
    }),
  }),
});

export const {
  useGetGoalTypesQuery,
  useCreateGoalTypeMutation,
  useUpdateGoalTypeMutation,
  useDeleteGoalTypeMutation,
} = goalTypesApi;
