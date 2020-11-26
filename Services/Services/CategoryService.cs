using System;
using System.Collections.Generic;
using System.Linq;
using ReadLater.Entities;
using ReadLater.Repository;
using ReadLater.Services.Models.Categories;

namespace ReadLater.Services
{
    public class CategoryService : ICategoryService
    {
        protected IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Category CreateCategory(Category category)
        {
            _unitOfWork.Repository<Category>().Insert(category);
            _unitOfWork.Save();
            return category;
        }

        public void UpdateCategory(Category category)
        {
            _unitOfWork.Repository<Category>().Update(category);
            _unitOfWork.Save();
        }

        public List<Category> GetCategories()
        {
            return _unitOfWork.Repository<Category>().Query().Get().ToList();
        }

        public Category GetCategory(int Id)
        {
            return _unitOfWork.Repository<Category>().Query()
                                                    .Filter(c => c.ID == Id)
                                                    .Get()
                                                    .FirstOrDefault();
        }

        public Category GetCategory(string Name)
        {
            return _unitOfWork.Repository<Category>().Query()
                                        .Filter(c => c.Name == Name)
                                        .Get()
                                        .FirstOrDefault();
        }

        public void DeleteCategory(Category category)
        {
            _unitOfWork.Repository<Category>().Delete(category);
            _unitOfWork.Save();
        }

        public GetCategoriesResponse GetCategoriesJson()
        {
            var categories = GetCategories().Select(x => new CategoryDto { ID = x.ID, Name = x.Name }).ToList();
            return new GetCategoriesResponse { Categories = categories };
        }

        public CategoryDto CreateCategory(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Not valid name");
            }

            var category = new Category
            {
                Name = name
            };

            _unitOfWork.Repository<Category>().Insert(category);
            _unitOfWork.Save();
            return new CategoryDto
            {
                ID = category.ID,
                Name = category.Name
            };
        }
    }
}
