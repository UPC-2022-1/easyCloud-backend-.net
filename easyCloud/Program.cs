using easyCloud.Quote.Domain.Repositories;
using easyCloud.Quote.Domain.Services;
using easyCloud.Quote.Persistence.Repositories;
using easyCloud.Quote.Services;
using easyCloud.Record.Domain.Repositories;
using easyCloud.Record.Domain.Services;
using easyCloud.Record.Persistence.Repositories;
using easyCloud.Record.Services;
using easyCloud.User.Domain.Repositories;
using easyCloud.User.Domain.Services;
using easyCloud.User.Persistence.Repositories;
using easyCloud.User.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IQuoteRepository, QuoteRepository>();
builder.Services.AddScoped<IQuoteService, QuoteService>();
builder.Services.AddScoped<IRecordService, RecordService>();
builder.Services.AddScoped<IRecordRepository, RecordRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
// </Snippet1>