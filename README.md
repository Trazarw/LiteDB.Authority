# Authority LiteDB Module

Ready to go LiteDB Module

  - Unique Authority Registration 
  - Authority Login

Nuget : Install-Package LiteDB.Authority -Version 1.1.0

LiteDB https://github.com/mbdavid/LiteDB

### Registration Usage

```csharp
new AuthorityRegistrationModule("databaseName")
.RegisterAuthorithy(new AuthorityCredentials
{
    Id = "authid!",
    Password = "authpass"
});
```

### Login Usage

```csharp
bool isSucess = new AuthorityLoginModule("databaseName")
.LogInAuthority(new AuthorityCredentials
{
     Id = "authid!",
     Password = "authpass"
});
```