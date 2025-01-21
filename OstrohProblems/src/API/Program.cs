using API;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder);
// builder.Services.AddApplication();
// builder.Services.SetupServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options
    .WithOrigins(new[] { "http://localhost:5146" })
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
);

// app.UseAuthentication();
// app.UseAuthorization();

await app.InitialiseDb();
app.MapControllers();

app.UseHttpsRedirection();

app.Run();
