namespace Spotify.Models;


public class Playlist
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<PlaylistSong> Songs { get; set; }
    public bool IsPublic { get; set; }
}