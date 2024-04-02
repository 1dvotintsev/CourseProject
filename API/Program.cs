using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace API

{
    public class Program
    {
        //public static void Main(string[] args)
        //{
        //    var builder = WebApplication.CreateBuilder(args);
        //    var app = builder.Build();

        //    // ������� ��� ����������� ����������� �� ����� index.html � ����� Content
        //    app.MapGet("/", (HttpContext context) =>
        //    {
        //        var contentPath = Path.Combine(app.Environment.ContentRootPath, "Views", "Home", "index.cshtml");
        //        //return contentPath;
        //        var htmlContent = File.ReadAllText(contentPath);

        //        // ������������� ��������� Content-Type ��� ���������
        //        context.Response.Headers.Add("Content-Type", "text/html");

        //        return htmlContent;
        //    });

        //    app.Run();

        //}

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(); // ��������� ��������� ����������� ������
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

