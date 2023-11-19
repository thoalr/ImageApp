using System;
using System.Collections.Generic;

namespace AppDB.Models;

public partial class TagType
{
    public long Id { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
