using BlazorBootcamp_Models;

namespace BlazorBootcamp_Business.Repository.IRepository
{
    public interface ICategoryRepository
    {
        public CategoryDTO Create(CategoryDTO objDTO);
        public CategoryDTO Update(CategoryDTO objDTO);
        public int Delete(int id);
        public CategoryDTO GetById(int id);
        public IEnumerable<CategoryDTO> GetAll();
    }
}
