using ERPApi.Utilidades;

var builder = WebApplication.CreateBuilder(args);

//Tomar cadena de conexion
 var CadenaSql=builder.Configuration.GetConnectionString("default");

// Add services to the container.

builder.Services.AddControllers();

//Usar singleton para instanciarla una unica vez
builder.Services.AddSingleton(new UI(CadenaSql));

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

app.UseAuthorization();

app.MapControllers();

app.Run();
