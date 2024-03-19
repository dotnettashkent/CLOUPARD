using System.Runtime.Serialization;

namespace Shared.Infrastructures
{
    public sealed partial record TableOptions
    {
        [property: DataMember] public int page { get; set; } = 1;

        [property: DataMember] public int page_size { get; set; } = 10000;

        [property: DataMember] public string? sort_label { get; set; }

        [property: DataMember] public int sort_direction { get; set; } = 1;

        [property: DataMember] public string? search { get; set; }

        [property: DataMember] public string? token { get; set; }
    }
}
