using System;
using System.Collections.Generic;

namespace SpeechtoTextProject.Models;

public partial class FavoriteWord
{
    public string UserId { get; set; } = null!;

    public string Word { get; set; } = null!;

    public string Definition { get; set; } = null!;

    public string Source { get; set; } = null!;

    public string? Phonetics { get; set; }

    public string? Context { get; set; }

    public string? AudioSource { get; set; }

    public int Id { get; set; }

    public virtual UsersTable User { get; set; } = null!;
}
