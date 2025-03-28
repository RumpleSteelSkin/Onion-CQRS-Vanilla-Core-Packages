using VCORE.Persistence.Extensions;
using VCORE.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);
/*################################
  ||                            ||
  ||          SERVICES          ||
  ||                            ||
  ################################*/
//----------------------------------------------
builder.Services.AddPresentationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

//----------------------------------------------
var app = builder.Build();
/*################################
  ||                            ||
  ||        APPLICATIONS        ||
  ||                            ||
  ################################*/
//----------------------------------------------


app.AddPresentationApp(); //<<<The Last
//----------------------------------------------