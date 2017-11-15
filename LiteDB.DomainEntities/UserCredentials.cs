using System;
using LiteDB.DomainEntities.Extensions;

namespace LiteDB.DomainEntities
{
    public class UserCredentials : Entities.UserCredentials
    {
        public void ValidateName()
        {
            if(string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentNullException(nameof(Name));
            }
            if(!Name.IsWhitelisted())
            {
                throw new ArgumentNullException($"Invalid {nameof(Name)}, allowed values are {Name.GetWhiteList()}");
            }
        }

        public void ValidatePassowrd()
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                throw new ArgumentNullException($"Invalid {nameof(Password)}, allowed values are {Name.GetWhiteList()}");
            }
        }
    }
}
