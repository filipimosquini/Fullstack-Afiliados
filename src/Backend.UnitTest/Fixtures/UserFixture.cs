using Backend.Core.Services.ViewModels;

namespace Backend.UnitTest.Fixtures;

public class UserFixtureCollection : ICollectionFixture<UserFixture> { }

public class UserFixture : IDisposable
{
    public UserViewModel CreateWithoutErrors()
    {
        return new UserViewModel()
        {
            Email = "teste@teste.com",
            Password = "Teste@123",
            ConfirmPassword = "Teste@123"
        };
    }

    public UserViewModel CreateWithErrors()
    {
        return new UserViewModel()
        {
            Email = "teste.teste.com",
            Password = "teste.123",
            ConfirmPassword = "teste.345"
        };
    }

    public void Dispose()
    {
    }
}