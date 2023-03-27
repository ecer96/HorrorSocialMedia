using System;
using System.Collections.Generic;

namespace HSStories.Models;

public partial class Liketype
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Like> Likes { get; } = new List<Like>();
}
