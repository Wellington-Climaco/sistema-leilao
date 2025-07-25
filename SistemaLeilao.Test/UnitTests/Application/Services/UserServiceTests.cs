using FluentResults;
using Moq;
using SistemaLeilao.Application.Interface;
using SistemaLeilao.Application.Request;
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
    
    [Fact]
    public async void DeveRetornarMensagemDeFalhaCasoNaoEncontreUserById()
    {
        //arrange
        var id = Guid.NewGuid();
        var expected = "Usuário não encontrado";
        _userRepository.Setup(x => x.GetUserById(id)).ReturnsAsync(() => null);
        
        //act
        var result = await _userService.GetUserById(id);
        
        //assert
        Assert.Equal(expected,result.Errors.FirstOrDefault().Message);
    }

    [Fact]
    public async void DeveRetornarResultOkCasoConsigaEncontrarUserById()
    {
        //arrange
        var id = Guid.NewGuid();
        var usuario = new Usuario("teste", new Email("teste@email.com"));
        var expected = Result.Ok();
        _userRepository.Setup(x => x.GetUserById(id)).ReturnsAsync(() => usuario);
        
        //act
        var result = await _userService.GetUserById(id);
        
        //assert
        Assert.Equal(expected.IsSuccess, result.IsSuccess);
    }
    
    [Fact]
    public async void CreateUserDeveRetornarMensagemDeErroCasoJaExistaUserComEmail()
    {
        //arrange
        var request = new CreateUserRequest("nome", "teste@email.com");
        var email = new Email(request.email);
        var usuario = new Usuario("teste", new Email("teste@email.com"));

        var expected = "Usuário com esse email já existe";
        
        _userRepository.Setup(x => x.GetUserByEmail(It.IsAny<Email>())).ReturnsAsync(() => usuario);

        //act
        var result = await _userService.CreateUser(request);
        
        //assert
        Assert.Equal(expected, result.Errors.FirstOrDefault().Message);
    }

    [Fact]
    public async void CreateUserDeveThrowExceptionCasoFormatoDeEmailInvalido()
    {
        //arrange
        var request =new CreateUserRequest("nome", "emailInvalido");
        
        //act & assert
        await Assert.ThrowsAsync<ArgumentException>(() => _userService.CreateUser(request));
    }
    
    [Fact]
    public async void DeveRetornarResultOkCasoConsigaCriarUsuario()
    {
        //arrange
        var request = new CreateUserRequest("nome", "teste@email.com");
        var expected = Result.Ok();
        var usuario = new Usuario("teste", new Email("teste@email.com"));
        
        _userRepository.Setup(x => x.GetUserByEmail(It.IsAny<Email>())).ReturnsAsync(() => null);
        _userRepository.Setup(x => x.RegisterUser(It.IsAny<Usuario>())).ReturnsAsync(() => usuario);
        
        //act
        var result = await _userService.CreateUser(request);
        
        //assert
        Assert.Equal(expected.IsSuccess, result.IsSuccess);
    }


}