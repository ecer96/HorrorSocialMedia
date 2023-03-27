using System;
using System.Collections.Generic;

namespace HSStories.Models;

public partial class Horrorstory
{
    public long StoriesId { get; set; }

    public string Title { get; set; } = null!;

    public string Text { get; set; } = null!;

    public byte[]? Image1 { get; set; }

    public byte[]? Image2 { get; set; }

    public byte[]? Image3 { get; set; }

    public byte[]? Image4 { get; set; }

    public long? UsersId { get; set; }

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<Like> Likes { get; } = new List<Like>();

    public virtual User? Users { get; set; }
}
