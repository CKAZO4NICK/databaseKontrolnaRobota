

using KontrolnaRobota.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreProject.Services.UserServices
{
    public interface IProductService
    {
        void Create(ProductEntity productEntity);
        bool Update(ProductEntity productEntity);
        ProductEntity GetById(long id);
        ProductEntity GetByName(string name);
        List<ProductEntity> GetNotBought();
    }
}
