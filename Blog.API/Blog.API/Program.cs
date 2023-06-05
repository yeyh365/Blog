using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Blog.Application.Dto;
using Blog.Application.Model;
using Blog.EntityFrameworkCore;
using Blog.WebAPI.Config;
using System.Text;

//var builder = WebApplication.CreateBuilder(args);
//用 WebApplicationOptions 代替args
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Directory.GetCurrentDirectory(),
    EnvironmentName = Environments.Development,
    Args = args
});

// Add services to the container.

builder.Services.AddControllers();

//支持Cache
builder.Services.AddMemoryCache();

//支持 HttpContextAccessor
builder.Services.AddHttpContextAccessor();

//支持 获取静态文件


//获取IP
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//支持 SqlServer
builder.Services.AddDbContext<EFDBContext>(options =>
        //options.UseMySql(builder.Configuration.GetConnectionString("MySqlServerConnection"), ServerVersion.Parse("5.6.45")));
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));

builder.Services.AddHttpClient();

//支持Session
builder.Services.AddSession();

//支持Log4

builder.Logging.AddLog4Net("log4net.config");


//支持 JWT
builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("JwtSetting"));
var jwtSetting = new JwtSetting();
builder.Configuration.Bind("JwtSetting", jwtSetting);
builder.Services
   .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidIssuer = jwtSetting.Issuer,
           ValidateAudience = true,
           ValidAudience = jwtSetting.Audience,
           ValidateLifetime = true,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecurityKey)),
           // 默认 300s
           ClockSkew = TimeSpan.Zero
       };
   });

//解决返回值小驼峰命名法(默认Json以及NewtonsoftJson设置)
builder.Services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

//注册服务工厂
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    ////添加依赖注入，AddModule是一个自定义的拓展方法，将依赖注入的方法单独提取出来，方便管理
    builder.Services.AddModule(container, builder.Configuration);
});

//添加AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

//支持跨域请求
var Origin = builder.Configuration["Origin"].Split(';').ToArray();
//builder.Services.AddCors(cor =>
//{
//    cor.AddPolicy("Cors", policy =>
//    {
//        policy.WithOrigins(Origin)
//        .AllowAnyHeader()
//        .AllowAnyMethod()
//        .AllowCredentials();
//    });
//});
builder.Services.AddCors(cor =>
{
    cor.AddPolicy("Cors", policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
        /*.AllowCredentials()*/;
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Blog.API", Version = "v1" });
    c.AddSecurityDefinition("Bearer",
       new OpenApiSecurityScheme
       {
           In = ParameterLocation.Header,
           Description = "请输入OAuth接口返回的Token，前置Bearer。示例：Bearer {Roken}",
           Name = "Authorization",
           Type = SecuritySchemeType.ApiKey,
       });
    //声明一个Scheme，注意下面的Id要和上面AddSecurityDefinition中的参数name一致
    var scheme = new OpenApiSecurityScheme()
    {
        Reference = new OpenApiReference() { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
    };
    //注册全局认证（所有的接口都可以使用认证）
    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            [scheme] = new string[0]
        });
});

var app = builder.Build();
//添加跨域设置
app.UseCors("Cors");
//添加Session
app.UseSession();
//添加认证
app.UseAuthentication();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{ }
app.UseSwagger();
app.UseSwaggerUI();


app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
