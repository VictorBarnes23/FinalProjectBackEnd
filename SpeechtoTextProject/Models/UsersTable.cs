using System;
using System.Collections.Generic;

namespace SpeechtoTextProject.Models;

public partial class UsersTable
{
    public string GoogleId { get; set; } = null!;

    public string? LangPreference { get; set; }
}
