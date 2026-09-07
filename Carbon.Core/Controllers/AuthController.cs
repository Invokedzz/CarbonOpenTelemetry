using Microsoft.AspNetCore.Mvc;

namespace Carbon.Core.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost]
    public string Register()
        => "Registered!";

    [HttpPost]
    public string Login()
        => "Logged in!";
}