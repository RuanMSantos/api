using test.db;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BdMercadoContext>(opt => {
    string connectionString = builder.Configuration.GetConnectionString("mercadoConnection")!;
    var serverVersion = ServerVersion.AutoDetect(connectionString);
    opt.UseMySql(connectionString, serverVersion);
});

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/funcionario", ([FromServices] BdMercadoContext _db, 
    [FromBody] TbFuncionario funcionario 
) => {

    var func = new TbFuncionario{
        Nome = funcionario.Nome,
    };

    _db.TbFuncionario.Add(func);
    _db.SaveChanges();

    var url = $"/funcionario/{func.Id}";

    return Results.Created(url, func);
});

app.Run();
