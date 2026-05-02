using Microsoft.AspNetCore.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevPolicy", p =>
        p.AllowAnyOrigin()
         .AllowAnyHeader()
         .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "ProductStore API v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseCors("DevPolicy");

app.MapGet("/weatherforecast", () =>
{
    return Results.Ok(new[] { new { Date = DateTime.Now, Summary = "API is Healthy" } });
});

app.MapControllers();

app.Run();