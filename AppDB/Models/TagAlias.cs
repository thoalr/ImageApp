using System;
using System.Collections.Generic;

namespace AppDB.Models;

public partial class TagAlias
{
    public long Id { get; set; }

    public string Alias { get; set; } = null!;

    public long TagId { get; set; }

    public virtual Tag Tag { get; set; } = null!;
}
