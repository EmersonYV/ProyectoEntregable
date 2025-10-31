using Data;
using Services;

var builder = WebApplication.CreateBuilder(args);

// CORS para Angular
const string cors = "_allowNg";
builder.Services.AddCors(o => o.AddPolicy(cors, p =>
    p.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI del servicio
builder.Services.AddScoped<IPersonaService, PersonaService>();

var app = builder.Build();

app.UseCors(cors);
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();