using FluentValidation.AspNetCore;
using FluentValidation;
using Serilog;
using System.Text.Json.Serialization;
using SharedKernel.Constants;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using AutoWrapper;
using SharedKernel.Helpers;
using Swashbuckle.AspNetCore.SwaggerUI;
using Application;
using Infrastructure;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

builder.Host.UseSerilog(((ctx, lc) =>
{
    lc.WriteTo.Console();
    lc.ReadFrom.Configuration(ctx.Configuration);
}));

// Add services Start
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure();
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: true));
    options.SerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.SerializerOptions.UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement;
});
// Add services End

//Add Cors Start
var MyCorsPolicy = "devCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyCorsPolicy, builder =>
    {
        builder.WithOrigins("https://localhost:7083").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
//Add Cors End

builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
{
    options.ClaimsIssuer = "https://localhost:7230";
    options.RequireHttpsMetadata = false;
    options.Authority = builder.Configuration["App:Authority"];

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidAudience = "MSPL"
    };
});

builder.Services.AddSwaggerGen(options =>
{
    options.TagActionsBy(api =>
    {
        if (api.GroupName != null)
        {
            return new[] { api.GroupName };
        }

        var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
        if (controllerActionDescriptor != null)
        {
            return new[] { controllerActionDescriptor.ControllerName };
        }

        throw new InvalidOperationException("Unable to determine tag for endpoint.");
    });

    options.DocInclusionPredicate((name, api) => true);

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API - CleanArchitecture",
        Description = Config.GitHubBranch
    });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token * *_only_ * *",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme, // must be lower case
        BearerFormat = "JWT",
        Flows = new OpenApiOAuthFlows
        {
            //Implicit = new OpenApiOAuthFlow
            //{
            //    AuthorizationUrl = new Uri("http://192.168.1.200:9800/connect/token"),
            //}
        },
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            securityScheme, 
            new string[] 
            { 
            }
        }
    });
});

builder.Services.AddControllers().AddJsonOptions(jsonOptions =>
{
    jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
}).AddDataAnnotationsLocalization(o =>
{
    o.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(Program));
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

if (Config.DoMigrate)
{
    using var s = app.Services.CreateScope();
    var dbContextFactory = s.ServiceProvider.GetService<IDbContextFactory<AppDbContext>>();
    var db = await dbContextFactory!.CreateDbContextAsync();
    await db.Database.MigrateAsync();
}

app.UseApiResponseAndExceptionWrapper<MapResponseObject>(new AutoWrapperOptions
{
    IsDebug = true,
    ShowApiVersion = true,
    IsApiOnly = false,
    ApiVersion = Config.ApiVersion,
    WrapWhenApiPathStartsWith = "/api"
});

app.UseCors(MyCorsPolicy);
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.DocExpansion(DocExpansion.None);
    options.DefaultModelExpandDepth(-1);
});

app.MapControllers().AllowAnonymous();

app.Run();

