import {
  configureStore,
  legacy_createStore as createStore,
} from "@reduxjs/toolkit";
import counterReducer from "./reducers/counterReducer";
import { useDispatch, useSelector } from "react-redux";
import { usersApi } from "./api/usersApi";
import { goalTypesApi } from "./api/goalTypesApi";

export function configureReduxStore() {
  return createStore(counterReducer);
}

export const store = configureStore({
  reducer: {
    [usersApi.reducerPath]: usersApi.reducer,
    [goalTypesApi.reducerPath]: goalTypesApi.reducer,
    counter: counterReducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(usersApi.middleware, goalTypesApi.middleware), // this is responsible for requests, caching, invalidation, polling, and other features of RTK Query
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export const useAppDispatch = useDispatch.withTypes<AppDispatch>();
export const useAppSelector = useSelector.withTypes<RootState>();
