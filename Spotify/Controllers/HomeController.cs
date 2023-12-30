using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spotify.DAL;
using Spotify.Models;
using Spotify.ViewModels;

namespace Spotify.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SpotifyDbContext _db;

    public HomeController(SpotifyDbContext db, ILogger<HomeController> logger)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index()
    {
        var songs = _db.Songs
            .Include(s => s.Artist)
            .Include(s => s.Playlists)
            .ThenInclude(ps => ps.Playlist)
            .Select(s => new SongViewModel
            {
                Title = s.Title,
                ArtistName = s.Artist.Name,
                Playlists = s.Playlists.Select(p => p.Playlist.Name).ToList()
            })
            .ToList();

        return View(songs);
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}