using System;
using System.Collections.Generic;

namespace InitialDatabase;

public partial class GroupTable
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<GroupedOrderedMedium> GroupedOrderedMedia { get; set; } = new List<GroupedOrderedMedium>();
}
