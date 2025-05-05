using BacklogClear.Application.UseCases.Users.Register;
using BacklogClear.Communication.Requests.Users;
using BacklogClear.Communication.Responses;
using BacklogClear.Communication.Responses.Games;
using Microsoft.AspNetCore.Mvc;

namespace BacklogClear.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredGameJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public IActionResult RegisterUser(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] RequestRegisterUserJson request)
    {
        var response = useCase.Execute(request);
        return Created(string.Empty, response);
    }
}