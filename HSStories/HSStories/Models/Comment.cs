using System;
using System.Collections.Generic;

namespace HSStories.Models;

public partial class Comment
{
    public long CommentsId { get; set; }

    public long UserId { get; set; }

    public long? HorrorStoriesId { get; set; }

    public long? PostId { get; set; }

    public long? CommentShared { get; set; }

    public string? Text { get; set; }

    public byte[]? Image { get; set; }

    public virtual Comment? CommentSharedNavigation { get; set; }

    public virtual Horrorstory? HorrorStories { get; set; }

    public virtual ICollection<Comment> InverseCommentSharedNavigation { get; } = new List<Comment>();

    public virtual ICollection<Like> Likes { get; } = new List<Like>();

    public virtual User User { get; set; } = null!;
}
