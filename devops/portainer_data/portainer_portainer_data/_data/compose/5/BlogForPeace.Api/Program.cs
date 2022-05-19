using BlogForPeace.Api.Authorization;
using BlogForPeace.Api.Infrastructure;
using BlogForPeace.Infrastructure.Data;
using BlogForPeace.Api.Web;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiResponseExceptionFilter>();
});

builder.Services.AddEndpointsApiExplorer();

// Add Swagger with Bearer Configuration
builder.Services.AddSwaggerWithBearerConfig();
//builder.Services.AddSwaggerGen();

// Add Authentication configuration
builder.AddAuthenticationAndAuthorization();

// Add Database Context
builder.AddBlogForPeaceDbContext();

// Add MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Add Repositories
builder.Services.AddBlogForPeaceAggregateRepositories();

// Add Api Features Handlers
builder.Services.AddApiFeaturesHandlers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseAuthenticationAndAuthorization();

app.UseHttpLogging();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BlogForPeaceContext>();
    db.Database.EnsureCreated();
}

app.Run();