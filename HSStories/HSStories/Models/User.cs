
using System.ComponentModel.DataAnnotations;
namespace HSStories.Models;

public partial class User
{
    public long Id { get; set; }

   
    public string Name { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string UserName { get; set; } = null!;
  
    public string Email { get; set; } = null!;
  
    public string Password { get; set; } = null!;

 
    public DateTime? Birthday { get; set; }
  
    public string Phone { get; set; } = null!;

    public string? Adress { get; set; }

    public DateTime? CreatedAt { get; set; }

    public byte[]? ProfilePhoto { get; set; }

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<Horrorstory> Horrorstories { get; } = new List<Horrorstory>();

    public virtual ICollection<Like> Likes { get; } = new List<Like>();
}
