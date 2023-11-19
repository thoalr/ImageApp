using System;
using System.Collections.Generic;

namespace InitialDatabase;

public partial class RatingTable
{
    public long Id { get; set; }

    public double Score { get; set; }

    public long ImageId { get; set; }

    public virtual MediaTable Image { get; set; } = null!;
}
