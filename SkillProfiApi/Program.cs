using Microsoft.EntityFrameworkCore;
using SkillProfiApi.Data;
var builder = WebApplication.CreateBuilder(args);

PictureDirectory.Configurate();
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = int.MaxValue;
});
// Add services to the container.
builder.Services.AddCors(opt => opt.AddDefaultPolicy(
    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddControllers();
builder.Services.AddDbContext<SkillProfiDbContext>
    (options =>  options.UseInMemoryDatabase(databaseName: "SkillProfi"));

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

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
