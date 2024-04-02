using SMovie.Application.IService;
using SMovie.Domain.Entity;
using SMovie.Domain.Repository;

namespace SMovie.Application.Service;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
        return await _unitOfWork.CategoryRepository.GetAll();
    }
}