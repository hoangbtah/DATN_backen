using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Infrastructure;
using MISA_WEBHAUI_AMIS_Core.Interfaces.Services;
using MISA_WEBHAUI_AMIS_Core.Services;
using MISA_WEBHAUI_Infrastructure.Repository;
using System.Text;
using System.Net.Mail;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// xử lý DI
builder.Services.AddTransient<SmtpClient>();
builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();
builder.Services.AddScoped<IEmailService,EmailService>();
builder.Services.AddScoped<ICommentRepository,CommentRepository>();
builder.Services.AddScoped<ICommentService,CommentService>();
builder.Services.AddScoped<IEmployeeService,EmployeeService>();
builder.Services.AddScoped(typeof(IBaseService<>),typeof(BaseService<>));
builder.Services.AddScoped(typeof(IBaseRepository<>),typeof(BaseRepository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IOrderProductRepository,OrderProductRepositorycs>();
builder.Services.AddScoped<IOrderProductService, OrderProductService>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddScoped<IVoucherService, VoucherService>();
builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();




// Xử lý cros policy
//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(
//       policy =>
//       {
//           policy.WithOrigins("*");
//           policy.WithHeaders("Origin", "Content-Type", "Accept");
//           policy.WithMethods("GET", "POST", "PUT", "DELETE");
//       });
//});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:8080") // Thay đổi thành nguồn gốc của Vue.js của bạn
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
// xac thuc
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
