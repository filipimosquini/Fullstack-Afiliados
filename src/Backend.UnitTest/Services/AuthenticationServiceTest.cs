using System.Security.Claims;
using Backend.Core.Services;
using Backend.Infra.Sections;
using Backend.UnitTest.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using Moq.AutoMock;

namespace Backend.UnitTest.Services;

[Trait("Service Tests", "Authentication service Test")]
public class AuthenticationServiceTest : IClassFixture<SigninFixture>
{
    private readonly SigninFixture _signinFixture;

    public AuthenticationServiceTest(SigninFixture signinFixture)
    {
        _signinFixture = signinFixture;
    }

    [Fact(DisplayName = "Should authenticate when authentication information is valid")]
    public async Task Should_Authenticate_When_AuthenticationInformationIsValid()
    {
        // Arrange
        var mocker = new AutoMocker();

        var userManager = mocker.GetMock<UserManager<IdentityUser>>();
        var identityValue = Options.Create(new Identity()
        {
            Issuer = "AffiliateFullstack",
            ExpiratesIn = 2,
            ValidOn = "https://localhost",
            Secret = "35a6ee9f9cc9e4a5230f18202c56a9ec"
        });

        var viewModel = _signinFixture.CreateWithoutErrors();
        var identityUser = new IdentityUser()
        {
            Email = viewModel.Email,
            EmailConfirmed = true
        };

        userManager
            .Setup(s => s.FindByEmailAsync(viewModel.Email))
            .ReturnsAsync(identityUser);

        userManager
            .Setup(s => s.GetClaimsAsync(identityUser))
            .ReturnsAsync(new List<Claim?>());

        userManager
            .Setup(s => s.GetRolesAsync(identityUser))
            .ReturnsAsync(new List<string>());

        var service = new Mock<AuthenticationService>(userManager.Object, identityValue);

        // Act
        var result = await service.Object.GenerateJwtToken(viewModel.Email);

        // Assert
        result.AccessToken.Should().NotBeNullOrWhiteSpace();

        mocker.Verify();
    }
}