using System;
using System.Collections.Generic;

namespace HSStories.Models;

public partial class Like
{
    public long LikesId { get; set; }

    public int? LikeType { get; set; }

    public long? StoriesId { get; set; }

    public long? PostId { get; set; }

    public long? CommentId { get; set; }

    public long? UserId { get; set; }

    public virtual Comment? Comment { get; set; }

    public virtual Liketype? LikeTypeNavigation { get; set; }

    public virtual Horrorstory? Stories { get; set; }

    public virtual User? User { get; set; }
}
