using LiteDB.Entities;

namespace LiteDB.Authority._UserRegistrationModule
{
    public interface IAuthorityRegistrationModule
    {
        string RegisterAuthorithy(AuthorityCredentials credentials);
    }
}
