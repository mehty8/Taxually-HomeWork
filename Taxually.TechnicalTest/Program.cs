using Taxually.TechnicalTest.Service.Client;
using Taxually.TechnicalTest.Service.CompanyRegisterForVat;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddLogging(logging => logging.AddConsole());

builder.Services.AddTransient<ITaxuallyQueueClient, TaxuallyQueueClient>();
builder.Services.AddTransient<ITaxuallyHttpClient, TaxuallyHttpClient>();
builder.Services.AddTransient<ICompanyRegisterForVat, GBCompanyRegisterForVat>();
builder.Services.AddTransient<ICompanyRegisterForVat, DECompanyRegisterForVat>();
builder.Services.AddTransient<ICompanyRegisterForVat, FRCompanyRegisterForVat>();


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
