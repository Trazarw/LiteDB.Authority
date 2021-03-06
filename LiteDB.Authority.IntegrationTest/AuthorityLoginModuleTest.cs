﻿using LiteDB.Authority._AuthorityLoginModule;
using LiteDB.Authority._AuthorityRegistrationModule;
using NUnit.Framework;
using System.IO;
using System.Reflection;
using System.Linq;
using LiteDB.Authority.Entities;

namespace LiteDB.Authority.IntegrationTest
{
    [TestFixture]
    public class AuthorityLoginModuleTest
    {
        private string _dbName;
        private IAuthorityLoginModule _loginModule;

        [SetUp]
        public void Setup()
        {
            _dbName = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}.db";
            _loginModule = new AuthorityLoginModule(_dbName);
        }

        [Test]
        public void LogIn_Should_Return_False()
        {
            bool actual = _loginModule.LogInAuthority(new AuthorityCredentials
            {
                Id = "BossId!Yo!?",
                Password = "UltraSecurePass"
            });

            Assert.IsFalse(actual);
        }

        [Test]
        public void Login_Should_Return_True()
        {
            var authority = new AuthorityCredentials
            {
                Id = "BossId!Yo!?",
                Password = "UltraSecurePass"
            };

            new AuthorityRegistrationModule(_dbName).RegisterAuthorithy(authority);

            bool actual = _loginModule.LogInAuthority(authority);

            Assert.IsTrue(actual);
        }

        [TearDown]
        public void TearDown()
        {
            using (var db = new LiteDatabase(_dbName))
            {
                db.GetCollectionNames().ToList().ForEach(x =>
                {
                    db.DropCollection(x);
                });
            }
        }
    }
}
