using LiteDB.Authority._UserRegistrationModul;
using LiteDB.Authority._UserRegistrationModule;
using LiteDB.Entities;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace LiteDB.Authority.IntegrationTest
{
    [TestFixture]
    public class AuthorityRegistrationModuleTest
    {
        private string _dbName;
        private IAuthorityRegistrationModule _registrationModule;

        [SetUp]
        public void Setup()
        {
            _dbName = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}.db";
            _registrationModule = new AuthorityRegistrationModule(_dbName);
        }

        [Test]
        public void RegisterAuthorithy_Should_Return_Expected()
        {
            var credentials = new AuthorityCredentials
            {
                Id = "BossUser!",
                Password = "BossPassword"
            };
            string actual = _registrationModule.RegisterAuthorithy(credentials);

            Assert.That(actual, Is.EqualTo(credentials.Id));
        }

        public static object[] RegisterAuthorithy_Should_Throw_Exception_TestCaseSource =
        {
            new object[] 
            {
                null, typeof(ArgumentNullException),
                "Value cannot be null.\r\nParameter name: id"
            },
            new object[]
            {
                new AuthorityCredentials{Password = "Pass!" }, typeof(ArgumentNullException),
                "Value cannot be null.\r\nParameter name: id"
            },
            new object[]
            {
                new AuthorityCredentials{Id = "theDoctor0!" }, typeof(ArgumentNullException),
                "Value cannot be null.\r\nParameter name: password"
            },
            new object[] 
            {
                new AuthorityCredentials{Id = "theDoctor0_!;" }, typeof(ArgumentException),
                "Invalid id, allowed values are 1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz.,'()?!#$."
            },
            new object[]
            {
                new AuthorityCredentials{Id = "TheDoctor!?", Password = "Pass_;!;" }, typeof(ArgumentException),
                "Invalid password, allowed values are 1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz.,'()?!#$."
            },
            new object[]
            {
                new AuthorityCredentials{Id = "TheDoctor!?", Password = "Passs!" }, typeof(ArgumentException),
                "Invalid password, password should contain at least 8 characters."
            },
        };
        [Test, TestCaseSource(nameof(RegisterAuthorithy_Should_Throw_Exception_TestCaseSource))]
        public void RegisterAuthorithy_Should_Throw_Exception(
            AuthorityCredentials credentials, Type expectedType, string expectedMessage)
        {
            Exception actual = Assert.Throws(expectedType, ()=> _registrationModule.RegisterAuthorithy(credentials));
            Assert.That(actual.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void RegisterAuthorithy_Should_Throw_Exception_If_Id_Already_Exsist()
        {
            var credentials = new AuthorityCredentials
            {
                Id = "DuplicatedIdOmg!",
                Password = "SoSecret"
            };

            _registrationModule.RegisterAuthorithy(credentials);

            Exception actual = Assert.Throws<InvalidOperationException>(() => _registrationModule.RegisterAuthorithy(credentials));

            Assert.That(actual.Message, Is.EqualTo("Sorry that id is currently in use."));
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
