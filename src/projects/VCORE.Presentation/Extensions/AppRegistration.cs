namespace VCORE.Presentation.Extensions;

public static class AppRegistration
{
    public static WebApplication AddPresentationApp(this WebApplication app)
    {
        
        if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }
        
        app.UseAuthorization();
        
        app.MapControllers();
        
        app.Run();
        
        return app;
    }
}