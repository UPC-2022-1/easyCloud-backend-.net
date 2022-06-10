using easyCloud.Provider.Domain.Repositories;
using easyCloud.Provider.Domain.Services;
using easyCloud.Provider.Persistence.Repositories;
using easyCloud.Provider.Services;
using easyCloud.Quote.Domain.Repositories;
using easyCloud.Quote.Domain.Services;
using easyCloud.Quote.Persistence.Repositories;
using easyCloud.Quote.Services;
using easyCloud.Record.Domain.Repositories;
using easyCloud.Record.Domain.Services;
using easyCloud.Record.Persistence.Repositories;
using easyCloud.Record.Services;
using easyCloud.Shared.Domain.Repositories;
using easyCloud.Shared.Mapping;
using easyCloud.Shared.Persistence.Contexts;
using easyCloud.Shared.Persistence.Repositories;
using easyCloud.User.Domain.Repositories;
using easyCloud.User.Domain.Services;
using easyCloud.User.Persistence.Repositories;
using easyCloud.User.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy  =>
        {
            policy.WithOrigins("*");
        });
});

// Add services to the container.
 
builder.Services.AddControllers();
 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Add API Documentation Information
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ACME Learning Center API",
        Description = "ACME Learning Center RESTful API",
        TermsOfService = new Uri("https://acme-learning.com/tos"),
        Contact = new OpenApiContact
        {
            Name = "ACME.studio",
            Url = new Uri("https://acme.studio")
        },
        License = new OpenApiLicense
        {
            Name = "ACME Learning Center Resources License",
            Url = new Uri("https://acme-learning.com/license")
        }
    });
    options.EnableAnnotations();
});
 
// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
 
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySQL(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());
 
// Add lowercase routes
builder.Services.AddRouting(options => options.LowercaseUrls = true);
 
//dependency injection configuration
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IQuoteRepository, QuoteRepository>();
builder.Services.AddScoped<IQuoteService, QuoteService>();
builder.Services.AddScoped<IRecordService, RecordService>();
builder.Services.AddScoped<IRecordRepository, RecordRepository>();
builder.Services.AddScoped<IProviderRepository, ProviderRepository>();
builder.Services.AddScoped<IProviderService, ProviderService>();

//automapper
builder.Services.AddAutoMapper(typeof(ModelToResourceProfile),typeof(ResourceToModelProfile));

var app = builder.Build();
 
//validation
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context.Database.EnsureCreated();
}
 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("v1/swagger.json", "v1");
        options.RoutePrefix = "swagger";
    });
}
 
app.UseHttpsRedirection();
 
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();
 
app.MapControllers();
 
app.Run();
// </Snippet1>