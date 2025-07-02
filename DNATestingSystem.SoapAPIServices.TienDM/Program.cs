using DNATestingSystem.Services.TienDM;
using DNATestingSystem.SoapAPIServices.TienDM.SoapServices;
using SoapCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Service Providers
builder.Services.AddScoped<IServiceProviders, ServiceProviders>();

// Add SOAP Services
builder.Services.AddScoped<IAppointmentsTienDmSoapService, AppointmentsTienDmSoapService>();

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

// Configure SOAP endpoint
app.UseSoapEndpoint<IAppointmentsTienDmSoapService>("/AppointmentsTienDmSoapService.asmx", new SoapEncoderOptions());

app.Run();
