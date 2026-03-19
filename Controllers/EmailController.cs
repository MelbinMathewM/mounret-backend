using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/email")]
public class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;

    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> Send()
    {
        await _emailService.SendEmailAsync(
            "melbinppmathewp@gmail.com",
            "Test Mail",
            "<h1>Hello from Mounret 🚀</h1>"
        );

        return Ok("Email sent");
    }
}