namespace MinimalApiVsWebApi
{
    public class VideoGame
    {
        public int  Id { get; set; }
        public string Name { get; set; } = String.Empty;    
        public string Developer { get; set; } = String.Empty;
        public DateTime Release { get; set; }

    }
}
