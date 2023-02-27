

using KontrolnaRobota.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreProject.Services.UserServices
{
    public interface ICheckService
    {
        void Create(CheckEntity checkEntity);
        bool Update(CheckEntity checkEntity);
        CheckEntity GetById(long id);
        CheckEntity GetByBuyerFK(long buyerFK);
    }
}
