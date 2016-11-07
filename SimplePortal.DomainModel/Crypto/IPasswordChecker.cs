namespace SimplePortal.DomainModel.Crypto
{
    public interface IPasswordChecker
    {
        string ClearTextPassword { get; set; }

        string HashedPassword { get; set; }

        bool PasswordCheckOk { get; }
    }
}