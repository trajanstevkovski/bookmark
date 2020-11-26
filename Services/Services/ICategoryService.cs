using System.Collections.Generic;
using ReadLater.Entities;
using ReadLater.Services.Models.Categories;

namespace ReadLater.Services
{
    public interface ICategoryService
    {
        Category CreateCategory(Category category);
        GetCategoriesResponse GetCategoriesJson();
        CategoryDto CreateCategory(string name);
        List<Category> GetCategories();
        Category GetCategory(int Id);
        Category GetCategory(string Name);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
