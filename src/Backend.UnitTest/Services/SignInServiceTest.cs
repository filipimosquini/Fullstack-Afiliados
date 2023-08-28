using Backend.Core.Services;
using Backend.Core.Services.Contracts.ViewModels;
using Backend.Core.Services.Interfaces;
using Backend.UnitTest.Fixtures;
using FluentAssertions;
using FluentValidation.TestHelper;
using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.Identity;
using Moq;
using Moq.AutoMock;

namespace Backend.UnitTest.Services;

[Trait("Service Tests", "Sign-in service Test")]
public class SignInServiceTest : IClassFixture<SigninFixture>, IClassFixture<AuthenticationFixture>
{
    private readonly SigninFixture _signinFixture;
    private readonly AuthenticationFixture _authenticationFixture;

    private readonly CompareLogic _comparator = new CompareLogic();

    public SignInServiceTest(SigninFixture signinFixture, AuthenticationFixture authenticationFixture)
    {
        _signinFixture = signinFixture;
        _authenticationFixture = authenticationFixture;
    }

    [Fact(DisplayName = "Should sign-in when authentication information is valid")]
    public async Task Should_SignIn_When_AuthenticationInformationIsValid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var signInManager = mocker.GetMock<SignInManager<IdentityUser>>();
        var authenticationService = mocker.GetMock<IAuthenticationService>();
        var signInViewModelContract = new SignInViewModelContract();

        var viewModel = _signinFixture.CreateWithoutErrors();

        signInManager
            .Setup(s => s.PasswordSignInAsync(viewModel.Email, viewModel.Password, false, true))
            .ReturnsAsync(SignInResult.Success);

        var auth = _authenticationFixture.Generate(); 
        authenticationService
            .Setup(s => s.GenerateJwtToken(viewModel.Email))
            .ReturnsAsync(auth);

        var service = new SignInService(signInManager.Object, authenticationService.Object, signInViewModelContract);

        // Act
        var validationModel = signInViewModelContract.TestValidate(viewModel);
        var result = await service.SignInAsync(viewModel);

        // Assert
        validationModel.IsValid.Should().Be(true);
        _comparator.Compare(auth, result.Data).AreEqual.Should().Be(true);

        mocker.Verify();
    }

    [Fact(DisplayName = "Should not sign-in when authentication information is incorrect")]
    public async Task Should_NotSignIn_When_AuthenticationInformationIsIncorrect()
    {
        // Arrange
        var mocker = new AutoMocker();

        var signInManager = mocker.GetMock<SignInManager<IdentityUser>>();
        var authenticationService = mocker.GetMock<IAuthenticationService>();
        var signInViewModelContract = new SignInViewModelContract();

        var viewModel = _signinFixture.CreateWithErrors();

        var service = new SignInService(signInManager.Object, authenticationService.Object, signInViewModelContract);

        // Act
        var validationModel = signInViewModelContract.TestValidate(viewModel);
        var result = await service.SignInAsync(viewModel);

        // Assert
        validationModel.IsValid.Should().Be(false);
        result.Errors.Should().HaveCountGreaterThan(0);

        mocker.Verify();
    }

    public static IEnumerable<object[]> PossibleSignInResultNotAllowed =>
        new List<SignInResult[]>
        {
            new SignInResult [] { SignInResult.NotAllowed }
        };
    public static IEnumerable<object[]> PossibleSignInResultLockedOut =>
        new List<SignInResult[]>
        {
            new SignInResult [] { SignInResult.LockedOut }
        };

    [Theory(DisplayName = "Should not sign-in when authentication information is Not Allowed and Locked Out")]
    [MemberData(nameof(PossibleSignInResultNotAllowed))]
    [MemberData(nameof(PossibleSignInResultLockedOut))]
    public async Task Should_NotSignIn_When_AuthenticationInformationIsNotAllowedAndLockedOut(SignInResult signInResult)
    {
        // Arrange
        var mocker = new AutoMocker();

        var signInManager = mocker.GetMock<SignInManager<IdentityUser>>();
        var authenticationService = mocker.GetMock<IAuthenticationService>();
        var signInViewModelContract = new SignInViewModelContract();

        var viewModel = _signinFixture.CreateWithoutErrors();

        signInManager
            .Setup(s => s.PasswordSignInAsync(viewModel.Email, viewModel.Password, false, true))
            .ReturnsAsync(signInResult);

        var service = new SignInService(signInManager.Object, authenticationService.Object, signInViewModelContract);

        // Act
        var validationModel = signInViewModelContract.TestValidate(viewModel);
        var result = await service.SignInAsync(viewModel);

        // Assert
        validationModel.IsValid.Should().Be(true);
        result.Errors.Should().HaveCountGreaterThan(0);

        mocker.Verify();
    }
}