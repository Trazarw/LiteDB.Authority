using System;
using LiteDB.Authority.Extensions;
using LiteDB.Authority.Entities;

namespace LiteDB.Authority._AuthorityRegistrationModule
{
    public class AuthorityRegistrationModule : IAuthorityRegistrationModule
    {
        private readonly string _databaseName;

        public AuthorityRegistrationModule(string databaseName)
        {
            if(string.IsNullOrWhiteSpace(databaseName))
            {
                throw new ArgumentNullException($"The database name is mandatory, if does not exist, it will be created.");
            }
            _databaseName = databaseName;
        }

        public string RegisterAuthorithy(AuthorityCredentials credentials)
        {
            ValidateId(credentials?.Id);
            ValidatePassowrd(credentials?.Password);

            using (var db = new LiteDatabase(_databaseName))
            {
                LiteCollection<AuthorityCredentials> authorities = db.GetCollection<AuthorityCredentials>();

                ValidateIdentificatorDoesNotExist(credentials.Id, authorities);

                authorities.Insert(credentials);

                return credentials.Id;
            }
        }

        private static void ValidateId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }
            if (!id.IsWhitelisted())
            {
                throw new ArgumentException($"Invalid {nameof(id)}, allowed values are {id.GetWhiteList()}.");
            }
        }

        private static void ValidatePassowrd(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (!password.IsWhitelisted())
            {
                throw new ArgumentException($"Invalid {nameof(password)}, allowed values are {password.GetWhiteList()}.");
            }

            if (password.Length <= 7)
            {
                throw new ArgumentException($"Invalid {nameof(password)}, password should contain at least 8 characters.");
            }
        }

        private static void ValidateIdentificatorDoesNotExist(string id, LiteCollection<AuthorityCredentials> currentCollection)
        {
            AuthorityCredentials existingUser = currentCollection.FindOne(x => x.Id == id);
            if (existingUser != null)
            {
                throw new InvalidOperationException($"Sorry that {nameof(id)} is currently in use.");
            }
        }
    }
}
