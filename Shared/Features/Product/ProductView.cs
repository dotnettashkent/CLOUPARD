using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Shared.Features.Product
{
    public partial class ProductView
    {
        [property : DataMember]
        [property : JsonPropertyName("id")]
        public Guid Id { get; set; }
        [property: DataMember]
        [property: JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [property: DataMember]
        [property: JsonPropertyName("description")]
        public string? Description { get; set; }

    }
}
