import { useState } from "react";
import {
  useCreateGoalTypeMutation,
  useGetGoalTypesQuery,
  useUpdateGoalTypeMutation,
} from "../../api/goalTypesApi";
import GoalTypeModal from "../../components/modals/goalTypeModal";
import GoalTypesInnerContainer from "./goalTypesInnerContainer";
import type { GoalTypeResponse } from "../../models/goalTypes/responses/goalTypeReponse";
import type { GoalTypeFormData } from "../../models/formData/goalTypeFormData";
import type {
  CreateGoalTypeRequest,
  UpdateGoalTypeRequest,
} from "../../models/goalTypes/requests/createGoalTypeRequest";

export default function GoalTypesOuterContainer() {
  const { data, isLoading } = useGetGoalTypesQuery();
  const [openModal, setOpenModal] = useState(false);
  const [selectedGoalType, setSelectedGoalType] =
    useState<GoalTypeResponse | null>(null);
  const [createGoalType, { isLoading: isCreating }] =
    useCreateGoalTypeMutation();
  const [updateGoalType, { isLoading: isUpdating }] =
    useUpdateGoalTypeMutation();

  const editGoalType = (goalType: GoalTypeResponse) => {
    setSelectedGoalType(goalType);
    setOpenModal(true);
  };

  const submitGoalType = (formData: GoalTypeFormData) => {
    setOpenModal(false);
    if (formData.id) {
      const updateGoalRequest: UpdateGoalTypeRequest = {
        id: formData.id,
        name: formData.name,
        description: formData.description,
        isActive: formData.isActive,
      };
      updateGoalType(updateGoalRequest);
    } else {
      const createGoalRequest: CreateGoalTypeRequest = {
        name: formData.name,
        description: formData.description,
      };
      createGoalType(createGoalRequest);
    }
  };

  const handleCloseModal = () => {
    setSelectedGoalType(null);
    setOpenModal(false);
  };

  return (
    <>
      {openModal && (
        <GoalTypeModal
          goalType={selectedGoalType}
          onClose={handleCloseModal}
          onSubmit={submitGoalType}
        />
      )}
      <GoalTypesInnerContainer
        isLoading={isLoading || isCreating || isUpdating}
        data={data}
        editGoalType={editGoalType}
        addGoalType={() => setOpenModal(true)}
      />
    </>
  );
}
