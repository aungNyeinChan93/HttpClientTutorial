using System;
using System.Collections.Generic;

namespace HttpClient.GameStoreDb.Models;

public partial class Game
{
    public int Id { get; set; }

    public string Publisher { get; set; } = null!;

    public string Developer { get; set; } = null!;

    public DateTime ReleaseDate { get; set; }

    public string Platform { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public string AgeRating { get; set; } = null!;
}
