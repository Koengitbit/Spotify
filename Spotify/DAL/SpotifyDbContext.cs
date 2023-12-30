
using Microsoft.EntityFrameworkCore;
using Spotify.Models;

namespace Spotify.DAL;

public class SpotifyDbContext : DbContext
{
    public SpotifyDbContext()
    {
    }

    public DbSet<Album> Albums { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Song> Songs { get; set; }
    public DbSet<Playlist> Playlists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Song>()
            .HasOne(s => s.Artist)
            .WithMany(a => a.Songs)
            .HasForeignKey(s => s.ArtistId);

        // Many-to-Many: Song to Playlist relationship
        modelBuilder.Entity<PlaylistSong>()
            .HasKey(ps => new { ps.PlaylistId, ps.SongId });

        modelBuilder.Entity<PlaylistSong>()
            .HasOne(ps => ps.Playlist)
            .WithMany(p => p.Songs)
            .HasForeignKey(ps => ps.PlaylistId);

        modelBuilder.Entity<PlaylistSong>()
            .HasOne(ps => ps.Song)
            .WithMany(s => s.Playlists)
            .HasForeignKey(ps => ps.SongId);

        modelBuilder.Entity<Artist>().HasData(
            new Artist { Id = 1, Name = "Artist One" },
            new Artist { Id = 2, Name = "Artist Two" }
        );

        modelBuilder.Entity<Album>().HasData(
            new Album { Id = 1, Title = "Album One", Name = "Album One"},
            new Album { Id = 2, Title = "Album Two", Name = "Album Two"}
        );

        modelBuilder.Entity<Song>().HasData(
            new Song { Id = 1, Title = "Song One", ArtistId = 1 },
            new Song { Id = 2, Title = "Song Two", ArtistId = 2 }
        );

        modelBuilder.Entity<Playlist>().HasData(
            new Playlist { Id = 1, Name = "Playlist One" },
            new Playlist { Id = 2, Name = "Playlist Two" }
        );

        // For Many-to-Many relationship, you would need to add entries in the join table
        modelBuilder.Entity<PlaylistSong>().HasData(
            new PlaylistSong { PlaylistId = 1, SongId = 1 },
            new PlaylistSong { PlaylistId = 2, SongId = 2 }
        );

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=SpotifyDb;Integrated Security=true;");
    }
}