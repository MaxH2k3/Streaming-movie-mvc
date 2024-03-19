using SMovie.Application.IService;
using SMovie.Domain.Entity;
using SMovie.Infrastructure.UnitOfWork;

namespace SMovie.Application.Service;

public class CategoryService : ICategoryService
{
    private readonly UnitOfWork _unitOfWork;

    public CategoryService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public CategoryService()
    {
        _unitOfWork = new UnitOfWork();
	}

    public async Task<IEnumerable<Category>> GetCategories()
    {
        return await _unitOfWork.CategoryRepository.GetAll();
    }
}