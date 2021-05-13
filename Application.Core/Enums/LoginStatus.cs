namespace Application.Core.Enums
{
    public enum LoginStatus
    {
        Success = 1,
        NotExisted = 0,
        EmailNotConfirmed = -1,
        LockedOut = -2,
        RequiresTwoFactorAuthentication = -3,
        Failure = -4
    }
}
