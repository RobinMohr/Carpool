using DataService;
using TecAlliance.Carpools.Business;
using TecAlliance.Carpools.Business.Interfaces;
using TecAlliance.Carpools.Business.Service;
using TecAlliance.Carpools.Data.Interfaces;
using TecAlliance.Carpools.Data.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserBusinessService, UserBusinessService>();
builder.Services.AddScoped<IUserDataService, UserDataService>();

builder.Services.AddScoped<ICarpoolBusinessService, CarpoolBusinessService>();
builder.Services.AddScoped<ICarpoolDataService, CarpoolDataService>();

builder.Services.AddScoped<ICarpoolUserBusinessService, CarpoolUserBusinessService>();
builder.Services.AddScoped<ICarpoolUserDataService, CarpoolUserDataService>();

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
