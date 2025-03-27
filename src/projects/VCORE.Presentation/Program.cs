using VCORE.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);
/*################################
  ||                            ||
  ||          SERVICES          ||
  ||                            ||
  ################################*/
//----------------------------------------------
builder.Services.AddPresentationServices();


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