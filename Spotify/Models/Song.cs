namespace Spotify.Models;

public class Song
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int ArtistId { get; set; }
    public Artist Artist { get; set; }
    public List<PlaylistSong> Playlists { get; set; }
}