using MediatR;
using price_list.Application.Queries;
using price_list.Infraestructura.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Registrar repositorios y servicios
builder.Services.AddScoped<IGoogleSheetRepository, GoogleSheetRepository>(sp =>
    new GoogleSheetRepository("credentialGoogle.json"));

builder.Services.AddScoped<GetPriceListDataQueryHandler>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar MediatR
builder.Services.AddMediatR(typeof(Program).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()  // Permite cualquier origen (dominio)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


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

app.UseCors("AllowAllOrigins");


app.Run();
