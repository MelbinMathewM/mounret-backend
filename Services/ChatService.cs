using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Mounret.API.Data;
using OpenAI;
using OpenAI.Chat;

public class ChatService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _config;
    private readonly IMemoryCache _cache;

    public ChatService(
        ApplicationDbContext context,
        IConfiguration config,
        IMemoryCache cache
    )
    {
        _context = context;
        _config = config;
        _cache = cache;
    }

    public async Task<string> AskAsync(string message)
    {
        // ✅ CACHE CHECK (save money 💰)
        if (_cache.TryGetValue(message, out string cached))
        {
            return cached;
        }

        var keyword = message.ToLower();

        // 🔍 PRODUCTS
        var products = await _context.Products
            .Where(p => p.Name.ToLower().Contains(keyword))
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .Take(3) // ✅ reduce cost
            .ToListAsync();

        // 🔍 CATEGORIES
        var categories = await _context.Categories
            .Where(c => c.Name.ToLower().Contains(keyword))
            .Take(3)
            .ToListAsync();

        // 🔍 BRANDS
        var brands = await _context.Brands
            .Where(b => b.Name.ToLower().Contains(keyword))
            .Take(3)
            .ToListAsync();

        // ❌ NO DATA → NO AI CALL (save money)
        if (!products.Any() && !categories.Any() && !brands.Any())
        {
            return "Sorry, I couldn't find relevant products. Try a different query.";
        }

        // 🧠 FORMAT (short to reduce tokens)
        var productText = string.Join("\n", products.Select(p =>
            $"- {p.Name} ({p.Brand?.Name}) | {p.Material} | {p.Dimensions}"
        ));

        var categoryText = string.Join(", ", categories.Select(c => c.Name));
        var brandText = string.Join(", ", brands.Select(b => b.Name));

        var prompt = $@"
User: {message}

Categories: {categoryText}
Brands: {brandText}

Products:
{productText}

Answer like a premium furniture sales assistant.
Keep it short and helpful.
";

        var result = await CallOpenAI(prompt);

        // ✅ CACHE RESULT (10 mins)
        _cache.Set(message, result, TimeSpan.FromMinutes(10));

        return result;
    }

    // private async Task<string> CallOpenAI(string prompt)
    // {
    //     var apiKey = _config["OpenAI:ApiKey"];

    //     var client = new OpenAIClient(apiKey);

    //     var chatClient = client.GetChatClient("gpt-4o-mini");

    //     // ✅ FIXED TYPE (IMPORTANT)
    //     var messages = new List<ChatMessage>
    //     {
    //         new SystemChatMessage("You are a premium furniture sales assistant."),
    //         new UserChatMessage(prompt)
    //     };

    //     var response = await chatClient.CompleteChatAsync(messages);

    //     return response.Value.Content[0].Text;
    // }

    private async Task<string> CallOpenAI(string prompt)
    {
        // 🔥 TEMP MOCK (no cost, no API needed)
        await Task.Delay(500); // simulate delay

        return "This is a demo AI response. Your system is working correctly.";
    }
}