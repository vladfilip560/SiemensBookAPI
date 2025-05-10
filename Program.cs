using System.Data;
using MySql.Data.MySqlClient;
using Dapper;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors((options) =>
    {
        options.AddPolicy("DevCors", (corsBuilder) =>
            {
                corsBuilder.WithOrigins("http://localhost:4200", "http://localhost:3000", "http://localhost:8000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        options.AddPolicy("ProdCors", (corsBuilder) =>
            {
                corsBuilder.WithOrigins("https://myProductionSite.com")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
    });
 
var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseCors("DevCors");
    app.UseSwagger(options => { });
    app.UseSwaggerUI();

}
else
{
    app.UseCors("ProdCors");
    app.UseHttpsRedirection();
}
app.MapControllers();
app.Run();