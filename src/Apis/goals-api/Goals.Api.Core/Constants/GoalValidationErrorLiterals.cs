namespace Goals.Api.Core.Constants;

public static class GoalValidationErrorLiterals
{
    public const string NonExistentOrInactiveGoalType = "Given type does not exist or is inactive";
    public const string CannotAddStepsToCancelledOrCompletedGoal = "Steps cannot be added to a cancelled or completed goal";
}
