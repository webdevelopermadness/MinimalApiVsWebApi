global using Microsoft.EntityFrameworkCore;
global using MinimalApiVsWebApi.Data;
using MinimalApiVsWebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers(); //To use controller WE have to add controller here
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("videogamesdb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers(); // so we need middleware 

app.MapGet("/videogame", async (DataContext context) => await context.VideoGames.ToListAsync());

app.MapGet("/videogame/{id}", async (DataContext context, int id) =>
    await context.VideoGames.FindAsync(id) is VideoGame videoGame ?
    Results.Ok(videoGame) :
    Results.NotFound("No Game Here "));

app.MapPost("/videogame", async (DataContext context, VideoGame videoGame) =>
{
     context.VideoGames.Add(videoGame); 
     await context.SaveChangesAsync();
    return Results.Ok(await context.VideoGames.ToListAsync());
});

app.MapPut("/videogame/{id}", async (DataContext context, VideoGame videoGame, int id) =>
{
    var dbvideoGame = await context.VideoGames.FindAsync(id);
    if (dbvideoGame == null) return Results.NotFound("Nope");

    dbvideoGame.Name = videoGame.Name;
    dbvideoGame.Developer = videoGame.Developer;
    dbvideoGame.Release = videoGame.Release;


    await context.SaveChangesAsync();
   return Results.Ok(await context.VideoGames.ToListAsync());
});

app.MapDelete("/videogame/{id}", async (DataContext context, int id) =>
{
    var dbvideoGame = await context.VideoGames.FindAsync(id);
    if (dbvideoGame == null) return Results.NotFound("No Game to Delete Here.");

    context.VideoGames.Remove(dbvideoGame);
    await context.SaveChangesAsync();

    return Results.Ok(await context.VideoGames.ToListAsync());
});

app.Run();
