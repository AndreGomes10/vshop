using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(3)]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "The Price is Required")]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The Price is Required")]
        [Column(TypeName = "decimal(12,2)")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "The Stock is Required")]
        [Range(1, 9999)] // intervalo de valores de 1 a 9999
        public long Stock { get; set; }

        [MaxLength(250)]
        [DisplayName("Product Image")]
        public string? ImageURL { get; set; }

        [JsonIgnore]  // a propriedade Category vai ser ignorado na serialização
        public Category? Category { get; set; }
        public int CategoryId { get; set; }
    }
}
