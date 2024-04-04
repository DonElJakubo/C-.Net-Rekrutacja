// Pobieranie tagów z API Stack Overflow
public async Task<IEnumerable<Tag>> GetTagsAsync(CancellationToken cancellationToken)
{
    // Utwórz klienta API Stack Overflow
    var client = new StackExchangeApiClient("stackoverflow");

    // Pobierz listę tagów
    var tags = await client.Tags.GetAllAsync(cancellationToken);

    // Zapisz tagi do bazy danych lub cache
    await _tagRepository.SaveTagsAsync(tags);

    return tags;
}

// Obliczanie procentowego udziału tagów
public static IEnumerable<Tag> CalculateTagShare(IEnumerable<Tag> tags)
{
    // Pobierz całkowitą liczbę tagów
    var totalCount = tags.Sum(t => t.Count);

    // Oblicz procentowy udział każdego tagu
    foreach (var tag in tags)
    {
        tag.Share = (double)tag.Count / totalCount;
    }

    return tags;
}

// Kontroler API dla tagów
[Route("api/[controller]")]
[ApiController]
public class TagsController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagsController(ITagService tagService)
    {
        _tagService = tagService;
    }

    // Pobieranie listy tagów
    [HttpGet]
    public async Task<IActionResult> GetTags([FromQuery] GetTagsRequest request)
    {
        var tags = await _tagService.GetTagsAsync(request.Sort, request.PageNumber, request.PageSize);

        return Ok(tags);
    }

    // Pobieranie szczegółów tagu
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTag(int id)
    {
        var tag = await _tagService.GetTagByIdAsync(id);

        if (tag == null)
        {
            return NotFound();
        }

        return Ok(tag);
    }

    // Wymuszenie ponownego pobrania tagów
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshTags()
    {
        await _tagService.RefreshTagsAsync();

        return Ok();
    }
}

// Logowanie
public void ConfigureLogging(ILoggingBuilder loggingBuilder)
{
    loggingBuilder.AddSerilog(new LoggerConfiguration()
        .WriteTo.File("logs.log")
        .CreateLogger());
}

// Obsługa błędów
public class ApiException : Exception
{
    public ApiException(string message) : base(message)
    {
    }

    public ProblemDetails ToProblemDetails()
    {
        return new ProblemDetails
        {
            Title = "An error occurred",
            Detail = Message
        };
    }
}

// Konfiguracja uruchomieniowa
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();

    // Dodaj Swagger
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    });

    // Dodaj zależności
    services.AddScoped<ITagService, TagService>();
    services.AddScoped<ITagRepository, TagRepository>();
}

// Uruchamianie aplikacji
public static void Main(string[] args)
{
    var host = new WebHostBuilder()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseIISIntegration()
        .UseStartup<Startup>()
        .Build();

    host.Run();
}
