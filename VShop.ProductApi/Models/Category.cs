using System.Text.Json.Serialization;

namespace VShop.ProductApi.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }

        // relacionamento um para muitos, uma categoria pode ter mais de um produto, vai ser uma coleção de produtos
        //[JsonIgnore]  // pra não aparecer a exibição da informação no serialização
        public ICollection<Product>? Products { get; set; }
    }
}