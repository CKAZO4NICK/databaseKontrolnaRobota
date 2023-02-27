

using KontrolnaRobota.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreProject.Services.UserServices
{
    public interface IBuyerService
    {
        void Create(BuyerEntity buyerEntity);
        bool Update(BuyerEntity buyerEntity);
        BuyerEntity GetById(long id);
        BuyerEntity GetByNameAndSurname(string name, string surname);
        List<ProductEntity> GetProducts(string name, string surname, int checkNumber);
        CheckEntity GetCheck(string name, string surname, int checkNumber);
        List<BuyerEntity> GetAllBuyers();
        int GetChecksCount(string name, string surname);
    }
}
