/** THIS IS JUST AN EXAMPLE OF REDUX TOOLKIT REDUCER */

import { createSlice } from "@reduxjs/toolkit";

export type CounterState = {
  value: number;
};

const initialState: CounterState = {
  value: 0,
};

// Redux Toolkit enables us to create slices of the store,
// which automatically generate action creators and action types.
export const counterSlice = createSlice({
  name: "counter",
  initialState,
  reducers: {
    increment: (state, action) => {
      state.value += action.payload;
    },
    decrement: (state, action) => {
      state.value -= action.payload;
    },
  },
});

export const { increment: incrementAction, decrement: decrementAction } =
  counterSlice.actions;

// For legacy Redux without Toolkit, we can define action creators like this:
export function incrementLegacy(amount = 1) {
  return { type: "INCREMENT", payload: amount };
}

export function decrementLegacy(amount = 1) {
  return { type: "DECREMENT", payload: amount };
}

export default function counterReducer(
  state: CounterState = initialState,
  action: { type: string; payload?: number },
): CounterState {
  switch (action.type) {
    case "INCREMENT":
      return { ...state, value: state.value + (action.payload || 1) };
    case "DECREMENT":
      return { ...state, value: state.value - (action.payload || 1) };
    default:
      return state;
  }
}
