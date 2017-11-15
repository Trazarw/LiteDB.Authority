using LiteDB.Authority.Entities;

namespace LiteDB.Authority._AuthorityRegistrationModule
{
    public interface IAuthorityRegistrationModule
    {
        string RegisterAuthorithy(AuthorityCredentials credentials);
    }
}
