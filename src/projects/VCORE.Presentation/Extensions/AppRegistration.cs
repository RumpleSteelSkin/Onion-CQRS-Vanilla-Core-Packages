namespace VCORE.Presentation.Extensions;

public static class AppRegistration
{
    public static WebApplication AddPresentationApp(this WebApplication app)
    {
        app.UseExceptionHandler(_ => { });//First Place "Always"
        
        if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }

        //Warning: Priority...
        app.UseAuthentication();//1st
        app.UseAuthorization();//2nd
        
        app.MapControllers();
        
        app.Run();
        
        return app;
    }
}