using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MinimalApiVsWebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        private readonly DataContext _context;

        public VideoGameController(DataContext context)
        {
                _context = context; 
        }
        [HttpGet]   
        public async Task<ActionResult<List<VideoGame>>> GetVideoGames()
        {
            return Ok(await _context.VideoGames.ToListAsync());
        } 
        [HttpGet("{id}")]
        public async Task<ActionResult<List<VideoGame>>> GetVideoGame(int id)
        {
            var videoGame = await _context.VideoGames.FindAsync(id);    
            if(videoGame == null)
             return NotFound("No Game Here.");

            return Ok(videoGame);            
        }   
        [HttpPost]
        public async Task<ActionResult<List<VideoGame>>> CreateVideoGame(VideoGame videoGame)
        {
            _context.VideoGames.Add(videoGame);

            await _context.SaveChangesAsync();
            return Ok(await _context.VideoGames.ToListAsync());
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<List<VideoGame>>> UpdateVideoGame(VideoGame videoGame, int id)
        {
            var dbvideoGame = await _context.VideoGames.FindAsync(id);
            if (dbvideoGame == null)
                return NotFound("No Game Here.");

            
            dbvideoGame.Name = videoGame.Name;
            dbvideoGame.Developer = videoGame.Developer;
            dbvideoGame.Release = videoGame.Release;


            await _context.SaveChangesAsync();
            return Ok(await _context.VideoGames.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<VideoGame>>> DeleteVideoGame(int id)
        {
            var dbvideoGame = await _context.VideoGames.FindAsync(id);
            if (dbvideoGame == null)
                return NotFound("No Game Here.");

            _context.VideoGames.Remove(dbvideoGame);

            await _context.SaveChangesAsync();
            return Ok(await _context.VideoGames.ToListAsync());
        }

    }
}
