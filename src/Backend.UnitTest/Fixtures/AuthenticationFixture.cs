using Backend.Core.Services.DataTransferObjects;
using Bogus;

namespace Backend.UnitTest.Fixtures;

public class AuthenticationFixtureCollection : ICollectionFixture<AuthenticationFixture> { }

public class AuthenticationFixture : IDisposable
{
    public AuthenticationDto Generate()
    {

        var userClaims = new Faker<UserClaim>().CustomInstantiator(c => new UserClaim()
        {
            Type = "Type",
            Value = "Value"
        }).Generate(3);

        var userToken = new Faker<UserToken>().CustomInstantiator(c => new UserToken()
        {
            Email = "teste@teste.com",
            Claims = userClaims
        }).Generate();

        return new Faker<AuthenticationDto>()
            .CustomInstantiator(c => new AuthenticationDto()
            {
                AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhZTI1MDdlOS1kM2IzLTRkMWMtOWM2YS1iMmFlNDZlMDk2MTkiLCJlbWFpbCI6InRlc3RlQHRlc3RlLmNvbSIsImp0aSI6IjdiZDE5YWMwLTkxMzEtNDc0Zi05ZTY3LWIzMzExOWU3N2Y3ZCIsIm5iZiI6MTY5MzE1NTM1OSwiaWF0IjoxNjkzMTU1MzU5LCJleHAiOjE2OTMxNjI1NTksImlzcyI6IkFmZmlsaWF0ZUZ1bGxzdGFjayIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0In0.EzsJibxa3w6A1a0a1-VGxXxqDxumoj-CE4G1KY8GhNE",
                ExpiresIn = 7200,
                UserToken = userToken
            }).Generate();

    }

    public void Dispose()
    {
    }
}