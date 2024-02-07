using System.Text.Json.Serialization;

namespace VShop.ProductApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public long Stock { get; set; }
        public string? ImageURL { get; set; }


        //[JsonIgnore]  // pra não aparecer a exibição da informação no serialização
        public Category? Category { get; set; }

        // essas dias propriedades estão reforçando o relacionamento um para muitos entre categoria e produto
        public int CategoryId { get; set; }
    }
}
