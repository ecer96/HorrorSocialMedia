using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HSStories.Models;

public partial class HsstoriesContext : DbContext
{
    public HsstoriesContext()
    {
    }

    public HsstoriesContext(DbContextOptions<HsstoriesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Horrorstory> Horrorstories { get; set; }

    public virtual DbSet<Like> Likes { get; set; }

    public virtual DbSet<Liketype> Liketypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-8T51CGN;Database=HSStories;Trusted_Connection=True;TrustServerCertificate=True;");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentsId);

            entity.ToTable("COMMENTS");

            entity.Property(e => e.CommentsId).HasColumnName("comments_id");
            entity.Property(e => e.CommentShared).HasColumnName("comment_shared");
            entity.Property(e => e.HorrorStoriesId).HasColumnName("horror_stories_id");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.Text)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("text");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.CommentSharedNavigation).WithMany(p => p.InverseCommentSharedNavigation)
                .HasForeignKey(d => d.CommentShared)
                .HasConstraintName("FK_COMMENTS_COMMENTS");

            entity.HasOne(d => d.HorrorStories).WithMany(p => p.Comments)
                .HasForeignKey(d => d.HorrorStoriesId)
                .HasConstraintName("FK_COMMENTS_HORRORSTORIES");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_COMMENTS_USERS");
        });

        modelBuilder.Entity<Horrorstory>(entity =>
        {
            entity.HasKey(e => e.StoriesId).HasName("PK__HORRORST__6E16F2616CC8B74D");

            entity.ToTable("HORRORSTORIES");

            entity.Property(e => e.StoriesId).HasColumnName("storiesId");
            entity.Property(e => e.Image1).HasColumnName("image1");
            entity.Property(e => e.Image2).HasColumnName("image2");
            entity.Property(e => e.Image3).HasColumnName("image3");
            entity.Property(e => e.Image4).HasColumnName("image4");
            entity.Property(e => e.Text)
                .IsUnicode(false)
                .HasColumnName("text");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.UsersId).HasColumnName("users_id");

            entity.HasOne(d => d.Users).WithMany(p => p.Horrorstories)
                .HasForeignKey(d => d.UsersId)
                .HasConstraintName("FK__HORRORSTO__users__4AB81AF0");
        });

        modelBuilder.Entity<Like>(entity =>
        {
            entity.HasKey(e => e.LikesId);

            entity.ToTable("LIKES");

            entity.Property(e => e.LikesId).HasColumnName("likes_id");
            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.LikeType).HasColumnName("like_type");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.StoriesId).HasColumnName("stories_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Comment).WithMany(p => p.Likes)
                .HasForeignKey(d => d.CommentId)
                .HasConstraintName("FK_LIKES_COMMENTS");

            entity.HasOne(d => d.LikeTypeNavigation).WithMany(p => p.Likes)
                .HasForeignKey(d => d.LikeType)
                .HasConstraintName("FK_LIKES_LIKETYPES");

            entity.HasOne(d => d.Stories).WithMany(p => p.Likes)
                .HasForeignKey(d => d.StoriesId)
                .HasConstraintName("FK_LIKES_HORRORSTORIES");

            entity.HasOne(d => d.User).WithMany(p => p.Likes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_LIKES_USERS");
        });

        modelBuilder.Entity<Liketype>(entity =>
        {
            entity.ToTable("LIKETYPES");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USERS__3213E83FFED13CC8");

            entity.ToTable("USERS");

            entity.HasIndex(e => e.Phone, "UQ__USERS__5C7E359E01873783").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__USERS__A9D10534E29FAFB1").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__USERS__C9F284560733790A").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Adress)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Birthday).HasColumnType("datetime");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProfilePhoto).HasColumnName("profilePhoto");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
