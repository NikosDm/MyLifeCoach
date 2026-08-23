import { useState } from "react";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import type { GoalTypeResponse } from "../../models/goalTypes/responses/goalTypeReponse";
import type { GoalTypeFormData } from "../../models/formData/goalTypeFormData";

export interface GoalTypeModalProps {
  goalType: GoalTypeResponse | null;
  onClose: () => void;
  onSubmit: (data: GoalTypeFormData) => void;
}

export default function GoalTypeModal(props: GoalTypeModalProps) {
  const { goalType, onClose, onSubmit } = props;

  const [name, setName] = useState(goalType?.name || "");
  const [description, setDescription] = useState(goalType?.description || "");
  const [errors, setErrors] = useState<{
    name?: string;
    description?: string;
  }>({});

  const validateForm = (): boolean => {
    const newErrors: { name?: string; description?: string } = {};

    const sanitizedName = name.trim();
    if (!sanitizedName) {
      newErrors.name = "Name is required";
    } else if (sanitizedName.length > 50) {
      newErrors.name = "Name must be maximum 50 characters";
    }

    // Validate Description
    const sanitizedDescription = description.trim();
    if (sanitizedDescription && sanitizedDescription.length > 1000) {
      newErrors.description = "Description must be maximum 1000 characters";
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (!validateForm()) {
      return;
    }

    const formData: GoalTypeFormData = {
      id: goalType ? goalType.id : "",
      name: name.trim(),
      description: description.trim(),
      isActive: goalType ? goalType.isActive : true,
    };

    onSubmit(formData);
  };

  const handleClose = () => {
    setName("");
    setDescription("");
    setErrors({});
    onClose();
  };

  return (
    <Dialog fullWidth open={true} onClose={handleClose}>
      <DialogTitle>{goalType ? "Update" : "Create"} Goal Type</DialogTitle>
      <DialogContent>
        <DialogContentText>
          To {goalType ? "update" : "create"} a goal type, please enter the
          details here.
        </DialogContentText>
        <form onSubmit={handleSubmit} id="goal-type-form">
          <TextField
            autoFocus
            margin="dense"
            id="name-field"
            name="name"
            label="Name"
            type="text"
            fullWidth
            variant="standard"
            value={name}
            onChange={(event) => setName(event.target.value)}
            error={!!errors.name}
            helperText={errors.name}
            slotProps={{
              htmlInput: {
                maxLength: 50,
              },
            }}
          />
          <TextField
            margin="dense"
            id="description-field"
            name="description"
            label="Description"
            fullWidth
            variant="standard"
            value={description}
            onChange={(event) => setDescription(event.target.value)}
            error={!!errors.description}
            helperText={errors.description}
            multiline
            slotProps={{
              htmlInput: {
                maxLength: 1000,
              },
            }}
          />
        </form>
      </DialogContent>
      <DialogActions>
        <Button variant="outlined" color="primary" onClick={handleClose}>
          Cancel
        </Button>
        <Button
          type="submit"
          variant="contained"
          color="primary"
          form="goal-type-form"
        >
          {goalType ? "Update" : "Create"}
        </Button>
      </DialogActions>
    </Dialog>
  );
}
