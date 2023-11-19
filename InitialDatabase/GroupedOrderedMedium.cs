using System;
using System.Collections.Generic;

namespace InitialDatabase;

public partial class GroupedOrderedMedium
{
    public long Id { get; set; }

    public long GroupId { get; set; }

    public long MediaId { get; set; }

    public long Order { get; set; }

    public virtual GroupTable Group { get; set; } = null!;

    public virtual MediaTable Media { get; set; } = null!;
}
