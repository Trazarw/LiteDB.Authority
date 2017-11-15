using LiteDB.Authority.Entities;

namespace LiteDB.Authority._AuthorityLoginModule
{
    public interface IAuthorityLoginModule
    {
        bool LogInAuthority(AuthorityCredentials credentials);
    }
}
