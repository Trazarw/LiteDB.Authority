using LiteDB.Entities;

namespace LiteDB.Authority._AuthorityGetterModule
{
    public interface IAuthorityLoginModule
    {
        bool LogInAuthority(AuthorityCredentials credentials);
    }
}
