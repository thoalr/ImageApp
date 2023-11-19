using System;
using System.Collections.Generic;

namespace InitialDatabase;

public partial class TagType
{
    public long Id { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<TagTable> TagTables { get; set; } = new List<TagTable>();
}
