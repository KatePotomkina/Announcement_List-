using Announcement_List.BLL.Service;
using Announcement_List.DAL.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AnnouncementDbContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")));
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();