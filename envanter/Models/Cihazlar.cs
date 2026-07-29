using System;
using System.Collections.Generic;

namespace envanter.Models;

public partial class Cihazlar
{
    public int CihazId { get; set; }

    public string Kategori { get; set; } = null!;

    public string MarkaModel { get; set; } = null!;

    public string SeriNo { get; set; } = null!;

    public DateTime? AlisTarihi { get; set; }

    public string? Durum { get; set; }

    public virtual ICollection<Zimmet> Zimmets { get; set; } = new List<Zimmet>();
}
