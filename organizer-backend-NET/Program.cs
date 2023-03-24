using organizer_backend_NET.Inits;
using organizer_backend_NET.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
DatabasesInit.Init(builder);
RepositoriesInit.Init(builder);
ServicesInit.Init(builder);
AuthInit.Init(builder);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
