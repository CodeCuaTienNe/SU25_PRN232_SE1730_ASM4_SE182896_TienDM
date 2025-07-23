using DNATesting.SoapAPIServices.NhanVT.SoapServices;
using DNATestingSystem.Services.NhanVT;
using SoapCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add SoapCore services - THÊM DÒNG NÀY
builder.Services.AddSoapCore();

builder.Services.AddScoped<IServiceProviders, ServiceProviders>();
builder.Services.AddScoped<IServiceNhanVTSoapServices, ServiceNhanVTSoapServices>();    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSoapEndpoint<IServiceNhanVTSoapServices>("/ServiceNhanVT.asmx", new SoapEncoderOptions());

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
