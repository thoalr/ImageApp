using System;
using System.Collections.Generic;

namespace AppDB.Models;

public partial class Tag
{
    public long Id { get; set; }

    public string Tag1 { get; set; } = null!;

    public long TagTypeId { get; set; }

    public virtual ICollection<TagAlias> TagAliases { get; set; } = new List<TagAlias>();

    public virtual TagType TagType { get; set; } = null!;

    public virtual ICollection<Media> Media { get; set; } = new List<Media>();
}
