using System;
using System.Collections.Generic;

namespace envanter.Models;

public partial class Zimmet
{
    public int ZimmetId { get; set; }

    public int CihazId { get; set; }

    public int PersonelId { get; set; }

    public DateTime VerilisTarihi { get; set; }

    public DateTime? IadeTarihi { get; set; }

    public string? Aciklama { get; set; }

    public virtual Cihazlar Cihaz { get; set; } = null!;

    public virtual Personel Personel { get; set; } = null!;
}
