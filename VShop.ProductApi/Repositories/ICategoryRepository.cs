using VShop.ProductApi.Models;

namespace VShop.ProductApi.Repositories
{
    // foi criado uma classe Interface
    public interface ICategoryRepository
    {
        // Task é pra uma implementação asyncrona
        Task<IEnumerable<Category>> GetAll();
        Task<IEnumerable<Category>> GetCategoriesProducts();
        Task<Category> GetById(int id);
        Task<Category> Create(Category category);  // retornando category, mas aqui pode retornar qualquer coisa, void, int etc
        Task<Category> Update(Category category);  // retornando category, mas aqui pode retornar qualquer coisa, void, int etc
        Task<Category> Delete(int id);
    }
}
