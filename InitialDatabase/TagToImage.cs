using System;
using System.Collections.Generic;

namespace InitialDatabase;

public partial class TagToImage
{
    public long Id { get; set; }

    public long TagId { get; set; }

    public long MediaId { get; set; }

    public virtual MediaTable Media { get; set; } = null!;

    public virtual TagTable Tag { get; set; } = null!;
}
