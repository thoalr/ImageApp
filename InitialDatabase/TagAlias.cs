using System;
using System.Collections.Generic;

namespace InitialDatabase;

public partial class TagAlias
{
    public long Id { get; set; }

    public string Alias { get; set; } = null!;

    public long TagId { get; set; }

    public virtual TagTable Tag { get; set; } = null!;
}
