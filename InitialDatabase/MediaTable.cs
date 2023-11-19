using System;
using System.Collections.Generic;

namespace InitialDatabase;

public partial class MediaTable
{
    public long Id { get; set; }

    public string Location { get; set; } = null!;

    public double? Rating { get; set; }

    public virtual ICollection<GroupedOrderedMedium> GroupedOrderedMedia { get; set; } = new List<GroupedOrderedMedium>();

    public virtual RatingTable? RatingTable { get; set; }

    public virtual ICollection<TagToImage> TagToImages { get; set; } = new List<TagToImage>();
}
