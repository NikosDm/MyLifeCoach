namespace Libraries.Common.Constants;

public static class SecurityConstants
{
    public const string ADMIN_ROLE = "Admin";
    public const string USER_ROLE = "User";

    public static readonly string[] APPLICATION_ROLES = [ADMIN_ROLE, USER_ROLE];

    public const string SUB_CLAIM = "sub";
    public const string USERNAME_CLAIM = "username";
    public const string ROLE_CLAIM = "role";
    public const string IS_ACTIVE_CLAIM = "is_active";
    public const string IS_PENDING_VERIFICATION = "is_pending_verification";
}
