
### Registration Usage

```csharp
            new AuthorityRegistrationModule("databaseName").RegisterAuthorithy(new AuthorityCredentials
            {
                Id = "authid!",
                Password = "authpass"
            });
```

### Login Usage

```csharp
            bool isSucess = _loginModule.LogInAuthority(new AuthorityCredentials
            {
                Id = "authid!",
                Password = "authpass"
            });
```
