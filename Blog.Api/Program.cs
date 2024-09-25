using Blog.Api.Infrastructure.JwtUtil;
using Blog.Api.Infrastructure;
using Common.Application.FileUtil.Interfaces;
using Common.Application.FileUtil.Services;
using Common.Application;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Blog.Config;
using AspNetCoreRateLimit;
using Common.AspNetCore.Middlewares;
using Blog.Domain.RoleAgg;
using Blog.Infrastructure.Persistent.Ef;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(option =>
    {
        option.InvalidModelStateResponseFactory = (context =>
        {
            var result = new ApiResult()
            {
                IsSuccess = false,
                MetaData = new()
                {
                    AppStatusCode = AppStatusCode.BadRequest,
                    Message = ModelStateUtil.GetModelStateErrors(context.ModelState)
                }
            };
            return new BadRequestObjectResult(result);
        });
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    //var jwtSecurityScheme = new OpenApiSecurityScheme
    //{
    //    Scheme = "bearer",
    //    BearerFormat = "JWT",
    //    Name = "JWT Authentication",
    //    In = ParameterLocation.Header,
    //    Type = SecuritySchemeType.Http,
    //    Description = "Enter Token",

    //    Reference = new OpenApiReference
    //    {
    //        Id = JwtBearerDefaults.AuthenticationScheme,
    //        Type = ReferenceType.SecurityScheme
    //    }
    //};

    //option.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    //option.AddSecurityRequirement(new OpenApiSecurityRequirement
    //{
    //    { jwtSecurityScheme, Array.Empty<string>() }
    //});
});
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.RegisterBlogDependency(connectionString!);
builder.Services.RegisterApiDependency(builder.Configuration);

CommonBootstrapper.Init(builder.Services);
builder.Services.AddTransient<IFileService, FileService>();

//builder.Services.AddJwtAuthentication(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}



app.UseIpRateLimiting();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("BlogApi");
app.UseAuthentication();
app.UseAuthorization();

app.UseApiCustomExceptionHandler();
app.MapControllers();

app.Run();
