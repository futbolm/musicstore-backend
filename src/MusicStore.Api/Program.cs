using Microsoft.EntityFrameworkCore;
using MusicStore.Api.Middleware;
using MusicStore.Persistence;
using MusicStore.Repositories;
using MusicStore.Services;
using MusicStore.Services.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//esto añadimos para el swagger 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//  registramos el contexto de la base de datos 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});

builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IConcertRepository, ConcertRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();



builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<ISaleService, SaleService>(); 
builder.Services.AddScoped<IConcertService, ConcertService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddAutoMapper(config =>
{
   config.AddProfile<GenreMapperProfile>();
   config.AddProfile<ConcertMapperProfile>();
   config.AddProfile<SaleMapperProfile>();
}); 

var app = builder.Build();

// para que funciones los middleware de las excepciones
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // paso 2 para el swagger
    app.UseSwagger(); 
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers(); 


app.Run();

