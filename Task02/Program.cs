using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistence.Context;
using Persistence.Entities.Model;
using Persistence.Repository.Implementation;
using Persistence.Repository.Interface;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Reflection;
using Tak01Api.Middleware;
using Task02;
using Task02.SwaggerHelper.TaskTwo.Swaggerhelper;
using Task02.SwaggerHelper;
using static ApplicationLayer.ApplicationLayerService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerVersionedApiExplorer();


builder.Services.AddDbContext<AppDBContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(IRepo<>), typeof(Repo<>));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddApplicationServices();
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());
builder.Services.AddApplicationServices();
builder.Services.AddSwaggerVersionedApiExplorer();


var app = builder.Build();



IApiVersionDescriptionProvider provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
    options =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.UseGlobalExceptionHandler();
app.UseSerilogRequestLogging();

app.MapControllers();


app.Run();
