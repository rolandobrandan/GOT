using GOT.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();




builder.Services.AddDbContext<GotDbContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("GotDB"),
 sqlBuilder =>
 {
     sqlBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
     sqlBuilder.CommandTimeout(30);
     sqlBuilder.EnableRetryOnFailure();
 }
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GOT.Api v1");
    });

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
