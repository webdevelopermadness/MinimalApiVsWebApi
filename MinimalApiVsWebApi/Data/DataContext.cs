

namespace MinimalApiVsWebApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)   
        {
                
        }
        public DbSet<VideoGame> VideoGames => Set<VideoGame>();
    }

  //   {
  //  "id": 1,
  //  "name": "Life IS vital ",
  //  "developer": "Eagle ",
  //  "release": "2005-04-16T00:26:41.367Z"
  //},
  //{
  //  "id": 2,
  //  "name": "Legend League ",
  //  "developer": "Michael Starch ",
  //  "release": "1995-09-16T00:26:41.367Z"
  //}
}
