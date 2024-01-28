using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
namespace MISA_WEBHAUI_Api
{
    public class Startup
    {
        //public void Configure(IApplicationBuilder app)
        //{
        //    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        //}
        public void ConfigureServices(IServiceCollection services)
        {
            // Thêm các dịch vụ cần thiết cho ứng dụng
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            // Thêm các dịch vụ khác...

            // Đăng ký các dịch vụ của ứng dụng
        }

        public void Configure(IApplicationBuilder app)
        {
            // Xác định thứ tự của các middleware cần thiết
            app.UseCors("CorsPolicy");

            // Sử dụng các middleware khác...

            // Cấu hình ứng dụng
        }
    }
}
