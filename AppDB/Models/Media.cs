using System;
using System.Collections.Generic;

namespace AppDB.Models;

public partial class Media
{
    public long Id { get; set; }

    public string Location { get; set; } = null!;

    public double? Rating { get; set; }

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
