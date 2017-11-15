using LiteDB.Entities;

namespace LiteDB.Authority._AuthorityLoginModule
{
    public interface IAuthorityLoginModule
    {
        bool LogInAuthority(AuthorityCredentials credentials);
    }
}
