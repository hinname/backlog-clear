using BacklogClear.Application.UseCases.User.Register;
using BacklogClear.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BacklogClear.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    public IActionResult RegisterUser([FromBody] RequestRegisterUserJson request)
    {
        var useCase = new RegisterUserUseCase();
        var response = useCase.Execute(request);
        return Created(string.Empty, response);
    }
}