using AutoMapper;
using BlazorBootcamp_Business.Repository.IRepository;
using BlazorBootcamp_DataAccess;
using BlazorBootcamp_DataAccess.Data;
using BlazorBootcamp_Models;

namespace BlazorBootcamp_Business.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public readonly ApplicationDbContext _db;
        public readonly IMapper _mapper;

        public CategoryRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public CategoryDTO Create(CategoryDTO objDTO)
        {
            var obj = _mapper.Map<CategoryDTO, Category>(objDTO);
            obj.CreatedDate = DateTime.Now;
            var addedObj = _db.Categories.Add(obj);
            _db.SaveChanges();

            return _mapper.Map<Category, CategoryDTO>(addedObj.Entity);
        }

        public int Delete(int id)
        {
            var obj = _db.Categories.FirstOrDefault(c => c.Id == id);
            if(obj != null)
            {
                _db.Categories.Remove(obj);
                return _db.SaveChanges();
            }
            return 0;
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_db.Categories);
        }

        public CategoryDTO GetById(int id)
        {
            var obj = _db.Categories.FirstOrDefault(c => c.Id == id);
            if(obj != null)
            {
                return _mapper.Map<Category, CategoryDTO>(obj);
            }
            return new CategoryDTO();
        }

        public CategoryDTO Update(CategoryDTO objDTO)
        {
            var objFromDb = _db.Categories.FirstOrDefault(c => c.Id == objDTO.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = objDTO.Name;
                _db.Categories.Update(objFromDb);
                _db.SaveChanges();

                return _mapper.Map<Category, CategoryDTO>(objFromDb);
            }

            return objDTO;
        }
    }
}
