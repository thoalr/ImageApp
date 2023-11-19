using System;
using System.Collections.Generic;

namespace InitialDatabase;

public partial class TagTable
{
    public long Id { get; set; }

    public string Tag { get; set; } = null!;

    public long TagTypeId { get; set; }

    public virtual ICollection<TagAlias> TagAliases { get; set; } = new List<TagAlias>();

    public virtual ICollection<TagToImage> TagToImages { get; set; } = new List<TagToImage>();

    public virtual TagType TagType { get; set; } = null!;

    public override string ToString()
    {
        return $"{Id} | {Tag} | {TagTypeId}";
    }
}
