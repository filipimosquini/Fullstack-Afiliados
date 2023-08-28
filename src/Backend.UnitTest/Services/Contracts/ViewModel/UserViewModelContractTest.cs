using Backend.Core.Services.Contracts.ViewModels;
using Backend.UnitTest.Fixtures;
using FluentValidation.TestHelper;

namespace Backend.UnitTest.Services.Contracts.ViewModel;

public class UserViewModelContractTest : IClassFixture<UserFixture>
{
    private readonly UserFixture _userFixture;

    public UserViewModelContractTest(UserFixture userFixture)
    {
        this._userFixture = userFixture;
    }

    [Fact(DisplayName = "Should validate contract when user view model contract is valid")]
    public void Should_ValidateContract_When_UserViewModelContractIsValid()
    {
        // Arrange
        var contract = new UserViewModelContract();
        var viewModel = _userFixture.CreateWithoutErrors();

        // Act
        var result = contract.TestValidate(viewModel);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Should no validate contract when user view model contract is invalid")]
    public void Should_NoValidateContract_When_UserViewModelContractIsInvalid()
    {
        // Arrange
        var contract = new UserViewModelContract();
        var viewModel = _userFixture.CreateWithErrors();

        // Act
        var result = contract.TestValidate(viewModel);

        // Assert
        result.ShouldHaveAnyValidationError();
    }
}