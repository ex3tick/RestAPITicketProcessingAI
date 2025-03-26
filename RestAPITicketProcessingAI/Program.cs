using RestAPITicketProcessingAI.Models.DeepSeekEntitys;
using RestAPITicketProcessingAI.Models.DeepSeekLogic;

namespace RestAPITicketProcessingAI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var deepSeekSettings = new DeepSeekSettings
        {
            ApiKey = builder.Configuration.GetValue<string>("DeepSeek:ApiKey"),
            ModelName = builder.Configuration.GetValue<string>("DeepSeek:ModelName"),
            SystemRoleDefinition = builder.Configuration.GetValue<string>("DeepSeek:SystemRoleDefinition")
            
        };
        DeepSeekConnection.deepSeekSettings = deepSeekSettings;

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}