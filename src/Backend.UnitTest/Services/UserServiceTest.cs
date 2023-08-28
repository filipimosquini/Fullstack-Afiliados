using Backend.Core.Services.Contracts.ViewModels;
using Backend.Core.Services;
using Backend.Core.Services.Interfaces;
using Backend.UnitTest.Fixtures;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Identity;
using Moq.AutoMock;
using KellermanSoftware.CompareNetObjects;
using Moq;

namespace Backend.UnitTest.Services;

[Trait("Service Tests", "User service Test")]
public class UserServiceTest : IClassFixture<UserFixture>, IClassFixture<AuthenticationFixture>
{
    private readonly CompareLogic _comparator = new CompareLogic();

    private readonly UserFixture _userFixture;
    private readonly AuthenticationFixture _authenticationFixture;

    public UserServiceTest(UserFixture userFixture, AuthenticationFixture authenticationFixture)
    {
        _userFixture = userFixture;
        _authenticationFixture = authenticationFixture;
    }

    private Mock<UserManager<TUser>> MockUserManager<TUser>(TUser user, bool identityResultStatusSuccess = true) where TUser : class
    {
        List<TUser> userList = new List<TUser>();

        var userStore = new Mock<IUserStore<TUser>>();
        var userManager = new Mock<UserManager<TUser>>(userStore.Object, null, null, null, null, null, null, null, null);

        userManager.Object.UserValidators.Add(new UserValidator<TUser>());
        userManager.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

        var identityResult = identityResultStatusSuccess
            ? IdentityResult.Success
            : IdentityResult.Failed(new List<IdentityError>(){new IdentityError
            {
                Description = "Error"
            }}.ToArray());

        userManager
            .Setup(x => x.DeleteAsync(It.IsAny<TUser>()))
            .ReturnsAsync(identityResult);

        userManager
            .Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>()))
            .ReturnsAsync(identityResult)
            .Callback<TUser, string>((x, y) => userList.Add(x));

        userManager
            .Setup(x => x.UpdateAsync(It.IsAny<TUser>()))
            .ReturnsAsync(identityResult);

        return userManager;
    }

    [Fact(DisplayName = "Should add user when user information is valid")]
    public async Task Should_AddUser_When_UserInformationIsValid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var signInManager = mocker.GetMock<SignInManager<IdentityUser>>();
        var authenticationService = mocker.GetMock<IAuthenticationService>();
        var userViewModelContract = new UserViewModelContract();

        var viewModel = _userFixture.CreateWithoutErrors();

        var identityUser = new IdentityUser()
        {
            UserName = viewModel.Email,
            Email = viewModel.Email,
            EmailConfirmed = true
        };

        var userManager = MockUserManager(identityUser);

        var auth = _authenticationFixture.Generate();
        authenticationService
            .Setup(s => s.GenerateJwtToken(viewModel.Email))
            .ReturnsAsync(auth);

        var service = new UserService(signInManager.Object, userManager.Object, authenticationService.Object, userViewModelContract);

        // Act
        var validationModel = userViewModelContract.TestValidate(viewModel);
        var result = await service.AddUserAsync(viewModel);

        // Assert
        validationModel.IsValid.Should().Be(true);
        _comparator.Compare(auth, result.Data).AreEqual.Should().Be(true);

        mocker.Verify();
    }

    [Fact(DisplayName = "Should not add user when user information is incorrect")]
    public async Task Should_NotAddUser_When_UserInformationIsIncorrect()
    {
        // Arrange
        var mocker = new AutoMocker();

        var signInManager = mocker.GetMock<SignInManager<IdentityUser>>();
        var userManager = mocker.GetMock<UserManager<IdentityUser>>();
        var authenticationService = mocker.GetMock<IAuthenticationService>();
        var userViewModelContract = new UserViewModelContract();

        var viewModel = _userFixture.CreateWithErrors();

        var service = new UserService(signInManager.Object, userManager.Object, authenticationService.Object, userViewModelContract);

        // Act
        var validationModel = userViewModelContract.TestValidate(viewModel);
        var result = await service.AddUserAsync(viewModel);

        // Assert
        validationModel.IsValid.Should().Be(false);
        result.Errors.Should().HaveCountGreaterThan(0);

        mocker.Verify();
    }

    [Fact(DisplayName = "Should get errors at try to add user when user information is invalid")]
    public async Task Should_GerErrorsAtTryToAddUser_When_UserInformationIsInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var signInManager = mocker.GetMock<SignInManager<IdentityUser>>();
        var authenticationService = mocker.GetMock<IAuthenticationService>();
        var userViewModelContract = new UserViewModelContract();

        var viewModel = _userFixture.CreateWithoutErrors();

        var identityUser = new IdentityUser()
        {
            UserName = viewModel.Email,
            Email = viewModel.Email,
            EmailConfirmed = true
        };

        var userManager = MockUserManager(identityUser, false);

        var service = new UserService(signInManager.Object, userManager.Object, authenticationService.Object, userViewModelContract);

        // Act
        var validationModel = userViewModelContract.TestValidate(viewModel);
        var result = await service.AddUserAsync(viewModel);

        // Assert
        validationModel.IsValid.Should().Be(true);
        result.Errors.Should().HaveCountGreaterThan(0);

        mocker.Verify();
    }
}