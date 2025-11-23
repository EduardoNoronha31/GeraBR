using System.Reflection;
using Swashbuckle.AspNetCore.Annotations;
using GeraBR.Application.UseCases.ValidateCpf;
using GeraBR.Application.UseCases.GenerateCpf;
using GeraBR.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.EnableAnnotations(); });

builder.Services.AddScoped<ValidateCpfUseCase>();
builder.Services.AddTransient<ICpfGeneratorService, CpfGeneratorService>();
builder.Services.AddScoped<GenerateCpfUseCase>();

var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();