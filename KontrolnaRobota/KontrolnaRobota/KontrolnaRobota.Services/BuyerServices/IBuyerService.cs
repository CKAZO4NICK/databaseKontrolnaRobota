

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
    }
}
