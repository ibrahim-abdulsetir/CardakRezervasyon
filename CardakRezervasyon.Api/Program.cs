using CardakRezervasyon.Api.Data;
using CardakRezervasyon.Api.Repositories;
using CardakRezervasyon.Api.Services;
using Microsoft.EntityFrameworkCore;
using CardakRezervasyon.Api.Repositories;
using CardakRezervasyon.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IMesireAlaniRepository, MesireAlaniRepository>();
builder.Services.AddScoped<IMesireAlaniService, MesireAlaniService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    CardakRezervasyon.Api.Data.Seed.DbSeeder.Seed(db);
}


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
