using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayer.API.Filters;
using NLayer.API.Middlewares;
using NLayer.API.Modules;
using NLayer.Service.Mapping;
using NLayer.Service.Validations;
using NLayerRepository;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(option => option.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

builder.Services.Configure<ApiBehaviorOptions>(optios =>
{
    optios.SuppressModelStateInvalidFilter = true;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

// Kursla beraber eklenen satýrlar.
builder.Services.AddScoped(typeof(NotFoundFilter<>));

builder.Services.AddAutoMapper(typeof(MapProfile));





//--

//-- Db Connection 
builder.Services.AddDbContext<AppDbContext>(x =>
 {
     x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
     {
         option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
     });

 });

//AutoFac Library added to the Course
builder.Host.UseServiceProviderFactory
    (new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(ContainerBuilder => ContainerBuilder.RegisterModule(new RepoServiceModule()));









var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//CustomExceptionHandler
app.UseCustomException();

app.UseAuthorization();

app.MapControllers();

app.Run();
