using Microsoft.EntityFrameworkCore;
using VShop.ProductApi.Context;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;  // vai ser meu contexto
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        // retornar todas as categoriais na memoria
        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        // retornar todas as categoriais com seus produtos na memoria
        public async Task<IEnumerable<Category>> GetCategoriesProducts()
        {
            return await _context.Categories.Include(c => c.Products).ToListAsync();
        }

        // retornar categoria por id
        public async Task<Category> GetById(int id)
        {
            return await _context.Categories.Where(p => p.CategoryId == id).FirstOrDefaultAsync();  // FirstOrDefaultAsync(), localiza o primeiro item que atende esse criterio
        }

        // criar uma categoria
        public async Task<Category> Create(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        // alterar uma categoria
        public async Task<Category> Update(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return category;
        }

        // deletar uma categoria
        public async Task<Category> Delete(int id)
        {
            var product = await GetById(id);
            _context.Categories.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
