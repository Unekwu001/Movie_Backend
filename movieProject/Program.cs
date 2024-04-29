using movieProject.MovieServices;
using movieProject.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
 builder.Services.AddScoped<IMovieService,MovieService>();




builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://movie-5ey6vp3sp-unekwu001s-projects.vercel.app/", "https://jemmimah-moviefrontend.vercel.app/") // Replace with your React app's URL
               .AllowAnyHeader()
               .AllowAnyMethod()
               .WithExposedHeaders("Authorization"); // This adds the custom authorization header to response
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
