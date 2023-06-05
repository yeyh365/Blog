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
//�� WebApplicationOptions ����args
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Directory.GetCurrentDirectory(),
    EnvironmentName = Environments.Development,
    Args = args
});

// Add services to the container.

builder.Services.AddControllers();

//֧��Cache
builder.Services.AddMemoryCache();

//֧�� HttpContextAccessor
builder.Services.AddHttpContextAccessor();

//֧�� ��ȡ��̬�ļ�


//��ȡIP
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//֧�� SqlServer
builder.Services.AddDbContext<EFDBContext>(options =>
        //options.UseMySql(builder.Configuration.GetConnectionString("MySqlServerConnection"), ServerVersion.Parse("5.6.45")));
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));

builder.Services.AddHttpClient();

//֧��Session
builder.Services.AddSession();

//֧��Log4

builder.Logging.AddLog4Net("log4net.config");


//֧�� JWT
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
           // Ĭ�� 300s
           ClockSkew = TimeSpan.Zero
       };
   });

//�������ֵС�շ�������(Ĭ��Json�Լ�NewtonsoftJson����)
builder.Services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

//ע����񹤳�
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    ////�������ע�룬AddModule��һ���Զ������չ������������ע��ķ���������ȡ�������������
    builder.Services.AddModule(container, builder.Configuration);
});

//���AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

//֧�ֿ�������
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
           Description = "������OAuth�ӿڷ��ص�Token��ǰ��Bearer��ʾ����Bearer {Roken}",
           Name = "Authorization",
           Type = SecuritySchemeType.ApiKey,
       });
    //����һ��Scheme��ע�������IdҪ������AddSecurityDefinition�еĲ���nameһ��
    var scheme = new OpenApiSecurityScheme()
    {
        Reference = new OpenApiReference() { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
    };
    //ע��ȫ����֤�����еĽӿڶ�����ʹ����֤��
    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            [scheme] = new string[0]
        });
});

var app = builder.Build();
//��ӿ�������
app.UseCors("Cors");
//���Session
app.UseSession();
//�����֤
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
