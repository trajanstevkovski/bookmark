using Newtonsoft.Json;
using System.Collections.Generic;

namespace ReadLater.Services.Models.Categories
{
    [JsonObject]
    public class GetCategoriesResponse
    {
        public IEnumerable<CategoryDto> Categories { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
