using Newtonsoft.Json;

namespace ReadLater.Services.Models.Categories
{
    public class CategoryDto
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
