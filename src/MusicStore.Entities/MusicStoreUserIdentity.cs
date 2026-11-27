using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Entities;

public class MusicStoreUserIdentity : IdentityUser // identityUser viene de la libreria que instalamos
{
    [StringLength(100)]
    public string FirstName { get; set; } = default!;

    [StringLength(100)]
    public string LastName { get; set; } = default!;

    public int Age { get; set; }

    public DocumentTypeEnum DocumentType { get; set; }

    [StringLength(20)]
    public string DocumentNumber { get; set; } = default!;
}

public enum DocumentTypeEnum : short
{
    Dni,
    Passport
}