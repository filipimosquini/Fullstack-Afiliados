using Backend.Core.Services.ViewModels;

namespace Backend.UnitTest.Fixtures;

public class SigninFixtureCollection : ICollectionFixture<SigninFixture> { }

public class SigninFixture : IDisposable
{
    public SignInViewModel CreateWithoutErrors()
    {
        return new SignInViewModel()
        {
            Email = "teste@teste.com",
            Password = "Teste@123"
        };
    }

    public SignInViewModel CreateWithErrors()
    {
        return new SignInViewModel()
        {
            Email = "teste.teste.com",
            Password = "teste.123"
        };
    }


    public void Dispose()
    {
    }
}