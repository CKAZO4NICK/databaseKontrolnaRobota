

using KontrolnaRobota.Database.Entities;
using KontrolnaRobota.Database.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace EFCoreProject.Services.UserServices
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<ProductEntity> _genericRepository; 
        public ProductService(IGenericRepository<ProductEntity> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public void Create(ProductEntity productEntity)
        {
            _genericRepository.Create(productEntity);
        }


        public ProductEntity GetById(long id)
        {
            ProductEntity dbRecord = _genericRepository.Table
                .FirstOrDefault(product => product.Id == id);
            if(dbRecord == null)
            {
                return null; 
            }
            return dbRecord;
        }

        public ProductEntity GetByName(string name)
        {
            ProductEntity dbRecord = _genericRepository.Table
                .Where(product => product.Name == name && !product.CheckFK.HasValue)
                .FirstOrDefault();
            if (dbRecord == null)
            {
                return null;
            }
            return dbRecord;
        }

        public List<ProductEntity> GetNotBought()
        {
            List<ProductEntity> dbRecord = _genericRepository.Table
                 .Where(products => !products.CheckFK.HasValue)
                 .ToList()!;
            return dbRecord;
        }

        public bool Update(ProductEntity productEntity)
        {
            try
            {
                _genericRepository.Table.Update(productEntity);
                _genericRepository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
