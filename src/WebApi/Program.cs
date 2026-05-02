
using Swashbuckle.AspNetCore.SwaggerUI;
// src/webapi/Program.cs
var builder = WebApplication.CreateBuilder(args);

// 1. Add OpenAPI services
builder.Services.AddOpenApi();

// (Optional) Keep your CORS policy here
builder.Services.AddCors(options => {
    options.AddPolicy("DevPolicy", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

// 2. Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    // Generates the JSON metadata
    app.MapOpenApi();
    
    // This provides the visual Swagger UI (Standard at /swagger)
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseCors("DevPolicy");

app.MapGet("/weatherforecast", () => {
    return Results.Ok(new[] { new { Date = DateTime.Now, Summary = "API is Healthy" } });
});

app.Run();