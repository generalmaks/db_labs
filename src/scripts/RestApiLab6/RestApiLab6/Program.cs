using Microsoft.EntityFrameworkCore;
using RestApiLab6.MyDbContext;
using RestApiLab6.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SurveyDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DbConnection"));
});
builder.Services.AddControllers();
builder.Services.AddScoped<SurveyRepository>();
builder.Services.AddScoped<ExpertiseRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<UserRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStatusCodePages();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();