using System.Text.Json.Serialization;

namespace Api.Application.Request;

public class UpdateProductRequest : BaseProductRequest
{
    [JsonIgnore]
    public int Id { get; set; }
}