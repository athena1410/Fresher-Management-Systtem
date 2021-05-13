namespace Application.Core.Enums
{
    public enum LoginStatus
    {
        Success,
        NotExisted,
        EmailNotConfirmed,
        LockedOut,
        RequiresTwoFactorAuthentication,
        Failure
    }
}
