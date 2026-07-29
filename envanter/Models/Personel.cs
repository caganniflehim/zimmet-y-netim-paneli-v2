using System;
using System.Collections.Generic;

namespace envanter.Models;

public partial class Personel
{
    public int PersonelId { get; set; }

    public string AdSoyad { get; set; } = null!;

    public string Departman { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime? KayitTarihi { get; set; }

    public string? Durum { get; set; }

    public virtual ICollection<Zimmet> Zimmets { get; set; } = new List<Zimmet>();
}
