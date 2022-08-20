var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();

var app = builder.Build();

var api = app.Configuration["Seq:ServerUrl"];
if (string.IsNullOrWhiteSpace(api))
{
    app.Logger.LogError("Seq:ServerUrl is required");
    return;
}

api = api.TrimEnd('/');

var key = app.Configuration["Seq:ApiKey"];
if (string.IsNullOrWhiteSpace(key))
{
    app.Logger.LogError("Seq:key is required");
    return;
}

var seqApi = new Uri($"{api}/api/events/raw?clef&apiKey=${key}");

app.MapPost("/", async context =>
{
    var httpClientFactory = context.RequestServices.GetRequiredService<IHttpClientFactory>();
    var client = httpClientFactory.CreateClient();

    var content = new StreamContent(context.Request.Body);
    var response = await client.PostAsync(seqApi, content);
    if (!response.IsSuccessStatusCode)
    {
        app.Logger.LogError(await response.Content.ReadAsStringAsync());
    }
});

app.Run();