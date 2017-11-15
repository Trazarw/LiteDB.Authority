using LiteDB.Authority.Entities;
using System;

namespace LiteDB.Authority._AuthorityLoginModule
{
    public class AuthorityLoginModule : IAuthorityLoginModule
    {
        private readonly string _databaseName;

        public AuthorityLoginModule(string databaseName)
        {
            if (string.IsNullOrWhiteSpace(databaseName))
            {
                throw new ArgumentNullException($"The database name is mandatory, if does not exist, it will be created.");
            }
            _databaseName = databaseName;
        }

        public bool LogInAuthority(AuthorityCredentials credentials)
        {
            using (var db = new LiteDatabase(_databaseName))
            {
                LiteCollection<AuthorityCredentials> authorities = db.GetCollection<AuthorityCredentials>();

                AuthorityCredentials match = authorities.FindOne(x =>
                    x.Id == credentials.Id && x.Password == credentials.Password);

                return (match == null) ? false : true;
            }
        }
    }
}
