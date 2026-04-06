using Microsoft.AspNetCore.Authentication.Negotiate;
using DotNetEnv;
using GerenciadorPatrimonio.Contexts;
using Microsoft.EntityFrameworkCore;
using GerenciadorPatrimonio.Interfaces;
using GerenciadorPatrimonio.Repositorys;
using GerenciadorPatrimonio.Aplications.Services;

var builder = WebApplication.CreateBuilder(args);

//carregando o .env
Env.Load();

//PEGANDO A CONNECTION string
string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")!;

//conexão com o banco
builder.Services.AddDbContext<GestaoPatrimoniosContext>(options => options.UseSqlServer(connectionString));


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Áreas
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<AreaService>();
//Bairro
builder.Services.AddScoped<IBairroRepository, BairroRepository>();
builder.Services.AddScoped<BairroService>();
//Cidade
builder.Services.AddScoped<ICidadeRepository, CidadeRepository>();
builder.Services.AddScoped<CidadeService>();
//Endereco
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddScoped<EnderecoService>();
//Localizacao
builder.Services.AddScoped<ILocalizacaoRepository, LocalizacaoRepository>();
builder.Services.AddScoped<LocalizacaoService>();
//Cargo
builder.Services.AddScoped<ICargoRepository, CargoRepository>();
builder.Services.AddScoped<CargoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
