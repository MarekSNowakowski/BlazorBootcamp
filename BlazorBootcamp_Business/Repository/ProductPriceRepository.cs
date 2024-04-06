using AutoMapper;
using BlazorBootcamp_DataAccess.Data;
using BlazorBootcamp_DataAccess;
using BlazorBootcamp_Models;
using BlazorBootcamp_Business.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BlazorBootcamp_Business.Repository
{
    public class ProductPriceRepositrory : IProductPriceRepository
    {
        public readonly ApplicationDbContext _db;
        public readonly IMapper _mapper;

        public ProductPriceRepositrory(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductPriceDTO> Create(ProductPriceDTO objDTO)
        {
            var obj = _mapper.Map<ProductPriceDTO, ProductPrice>(objDTO);
            var addedObj = _db.ProductPrices.Add(obj);
            await _db.SaveChangesAsync();

            return _mapper.Map<ProductPrice, ProductPriceDTO>(addedObj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = await _db.ProductPrices.FirstOrDefaultAsync(c => c.Id == id);
            if (obj != null)
            {
                _db.ProductPrices.Remove(obj);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<ProductPriceDTO>> GetAll(int? id = null)
        {
            if(id!=null && id > 0)
            {
                return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDTO>>
                    (_db.ProductPrices.Where(u=>u.ProductId == id));
            }
            else
            {
                return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDTO>>(_db.ProductPrices);
            }
        }

        public async Task<ProductPriceDTO> GetById(int id)
        {
            var obj = await _db.ProductPrices.FirstOrDefaultAsync(c => c.Id == id);
            if (obj != null)
            {
                return _mapper.Map<ProductPrice, ProductPriceDTO>(obj);
            }
            return new ProductPriceDTO();
        }

        public async Task<ProductPriceDTO> Update(ProductPriceDTO objDTO)
        {
            var objFromDb = await _db.ProductPrices.FirstOrDefaultAsync(c => c.Id == objDTO.Id);
            if (objFromDb != null)
            {
                objFromDb.Price = objDTO.Price;
                objFromDb.Size = objDTO.Size;
                objFromDb.ProductId = objDTO.ProductId;
                _db.ProductPrices.Update(objFromDb);
                await _db.SaveChangesAsync();

                return _mapper.Map<ProductPrice, ProductPriceDTO>(objFromDb);
            }

            return objDTO;
        }
    }
}
