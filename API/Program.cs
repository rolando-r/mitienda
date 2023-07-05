using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<MiTiendaContext>(options =>
{
    string ? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using(var scope= app.Services.CreateScope()){
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try{
            var context = services.GetRequiredService<MiTiendaContext>();
            await context.Database.MigrateAsync();
            }
            catch(Exception ex){
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex,"Ocurrió un error durante la migración");
            }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
