using VCORE.Application.Extensions;
using VCORE.Persistence.Extensions;
using VCORE.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddPresentationServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);


var app = builder.Build();


app.AddPresentationApp(); //<<<The Last