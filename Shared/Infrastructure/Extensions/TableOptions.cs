using System.Runtime.Serialization;

namespace Shared.Infrastructures
{
    public sealed partial record TableOptions
    {
        [property: DataMember] public int Page { get; set; } = 1;

        [property: DataMember] public int PageSize { get; set; } = 10000;

        [property: DataMember] public string? SortLabel { get; set; }

        [property: DataMember] public int SortDirection { get; set; } = 1;

        [property: DataMember] public string? Search { get; set; }
    }
}
