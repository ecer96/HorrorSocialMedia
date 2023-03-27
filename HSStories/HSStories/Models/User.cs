using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HSStories.Models;

public partial class User
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Favor de  Ingresar Tu Nombre"), MinLength(3, ErrorMessage = "Nombre debe contener por lo menos 3 caracteres"), MaxLength(30, ErrorMessage = "Nombre solo Puede Contener 30 Caracteres como Maximo")]
    public string Name { get; set; } = null!;
    [Required(ErrorMessage = "Favor de  Ingresar Su Apellido"), MinLength(3, ErrorMessage = "Apelldio  debe contener por lo menos 3 caracteres"), MaxLength(30, ErrorMessage = "Apellido solo Puede Contener 30 Caracteres como Maximo")]
    public string LastName { get; set; } = null!;
    [Required(ErrorMessage = "Favor de  Ingresar Su Nombre de Usuario"), MinLength(3, ErrorMessage = "Debe de Contener al menos 3 caracteres"), MaxLength(30, ErrorMessage = "30 es el Max Numero de Caracteres")]
    public string UserName { get; set; } = null!;
    [Required(ErrorMessage ="Favor de ingresar su Email")]
    [EmailAddress(ErrorMessage ="Favor de Ingresar Un Correo Valido")]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "Favor de Ingresar Su Contraseña"),MinLength(6,ErrorMessage ="La contraseña debe contener al menos 6 caracteres")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    public string? Salt { get; set; }
    [Required(ErrorMessage ="Favor de Ingresar su fecha de nacimiento")]
    [DataType(DataType.DateTime)]
    public DateTime? Birthday { get; set; }
    [Required(ErrorMessage ="Favor de Ingresar un Numero Valido de Telefono")]
    [Phone]
    public string Phone { get; set; } = null!;
  
    public string? Adress { get; set; }

    public DateTime? CreatedAt { get; set; }

    public byte[]? ProfilePhoto { get; set; }

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<Horrorstory> Horrorstories { get; } = new List<Horrorstory>();

    public virtual ICollection<Like> Likes { get; } = new List<Like>();
}
