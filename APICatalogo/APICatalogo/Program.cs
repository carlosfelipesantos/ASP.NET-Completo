using APICatalogo.Context;
using APICatalogo.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

string sqlServerConnection = builder.Configuration.GetConnectionString("DefaultConnection"); //pegando a string de conexão do arquivo appsettings.json

builder.Services.AddDbContext<AppDbContext>(options => //adicionando o contexto do banco de dados ao contêiner de serviços
    options.UseSqlServer(sqlServerConnection));

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>(); //registrando o repositório de categorias no contêiner de serviços, para que ele possa ser injetado nos controladores

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "weather api"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
