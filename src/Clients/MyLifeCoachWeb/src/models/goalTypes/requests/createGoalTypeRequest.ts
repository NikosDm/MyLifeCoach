export interface CreateGoalTypeRequest {
  name: string;
  description: string;
}

export interface UpdateGoalTypeRequest {
  id: string;
  name: string;
  description: string;
  isActive: boolean;
}
