using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Shared.Infrastructures.Extensions
{
    public partial class TableResponse<T> where T : class
    {
        [property: DataMember][JsonPropertyName("items")] public List<T> Items { get; set; } = new List<T>();

        [property: DataMember][JsonPropertyName("total_items")] public int TotalItems { get; set; }

        [property: DataMember][JsonPropertyName("all_page")] public int AllPage { get; set; }
        [property: DataMember][JsonPropertyName("current_page")] public int CurrentPage { get; set; }
    }
}
