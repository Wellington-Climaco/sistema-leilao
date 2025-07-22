using FluentResults;
using Moq;
using SistemaLeilao.Application.Interface;
using SistemaLeilao.Application.Services;
using SistemaLeilao.Core;
using SistemaLeilao.Core.Interface;
using SistemaLeilao.Core.ValueObject;

namespace SistemaLeilao.Test.UnitTests.Application.Services;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepository = new();
    private readonly IUserService _userService;
    public UserServiceTests()
    {
        _userService = new UserService(_userRepository.Object);
    }
    
    [Fact]
    public async void DeveRetornarMensagemDeFalhaCasoNaoEncontreUserByEmail()
    {
        //arrange
        var email = "teste@email.com";
        var expected = "usuário não encontrado.";
        _userRepository.Setup(x => x.GetUserByEmail(new Email(email))).ReturnsAsync(() => null);
        
        //act
        var result = await _userService.GetUserByEmail(email);
        
        //assert
        Assert.Equal(expected,result.Errors.FirstOrDefault().Message);
    }

    [Fact]
    public async void DeveRetornarResultOkCasoConsigaEncontrarUserByEmail()
    {
        //arrange
        var email = "teste@email.com";
        var usuario = new Usuario("teste", new Email(email));
        
        _userRepository
            .Setup(x => x.GetUserByEmail(It.Is<Email>(e => e.EmailAdress == email)))
            .ReturnsAsync(() => usuario);
        
        var expected = Result.Ok();
        
        //act
        var result = await _userService.GetUserByEmail(email);

        //assert
        Assert.Equal(expected.IsSuccess, result.IsSuccess);
    }


}