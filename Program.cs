using ApiMSCOFFIE.Data;
using ApiMSCOFFIE.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MSCOFFIEDBSettings>(builder.Configuration.GetSection("ConfiguracionBaseDatos"));
builder.Services.AddSingleton<EmpleadosService>();
builder.Services.AddSingleton<ProductosService>();
builder.Services.AddSingleton<ClienteService>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
