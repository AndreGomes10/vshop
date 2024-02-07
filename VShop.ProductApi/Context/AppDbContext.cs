using Microsoft.EntityFrameworkCore;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // definir o mapeamento entre as classes e as tabelas
        // a entidade usa no singlular, a tabela que vai ser gerada usa no plural
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Product>? Products { get; set; }

        // Fluent API
        // vamos configurar as propriedades das nossas entidades e definir o relacionamento
        // entre elas de forma explicita usando a Fluent API
        protected override void OnModelCreating(ModelBuilder mb)
        {
            // Category
            // HasKey, pra definir a chave primaria e depois defini a propriedade
            mb.Entity<Category>().HasKey(c => c.CategoryId);

            // definir tamanho das colunas
            mb.Entity<Category>().Property(c => c.Name)
                                  .HasMaxLength(100)
                                  .IsRequired();

            // Product
            // HasKey, pra definir a chave primaria e depois defini a propriedade
            //mb.Entity<Product>().HasKey(c => c.Id);

            mb.Entity<Product>().Property(c => c.Name)
                                  .HasMaxLength(100)
                                  .IsRequired();

            mb.Entity<Product>().Property(c => c.Price).HasPrecision(12, 2);

            mb.Entity<Product>().Property(c => c.Description)
                                  .HasMaxLength(255)
                                  .IsRequired();

            mb.Entity<Product>().Property(c => c.Stock);

            mb.Entity<Product>().HasKey(c => c.Id);

            mb.Entity<Product>().Property(c => c.ImageURL)
                                  .HasMaxLength(100)
                                  .IsRequired();


            // relacionamento
            // produto tem uma categoria com muitos produtos e a chave estrangeira é categoriaId
            mb.Entity<Category>()
                .HasMany(g => g.Products)
                 .WithOne(c => c.Category)
                  .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);  // quando excluir uma categoria os produtos relacionado vão ser excluidos em cascata

            // se não houver dados iniciais ele vai incluir, ele vai popular a tabela do banco de dados
            // aqui é obrigatório informar o valor de CategoryId
            mb.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    Name = "Material Escolar",
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "Acessórios",
                }
            );

        }

    }
}
