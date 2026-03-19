using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/chat")]
public class ChatController : ControllerBase
{
    private readonly ChatService _chatService;

    public ChatController(ChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpPost]
    public async Task<IActionResult> Ask([FromBody] ChatRequestDto dto)
    {
        var result = await _chatService.AskAsync(dto.Message);
        return Ok(result);
    }
}