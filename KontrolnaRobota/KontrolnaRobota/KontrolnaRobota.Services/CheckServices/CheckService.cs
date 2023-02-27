

using KontrolnaRobota.Database.Entities;
using KontrolnaRobota.Database.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace EFCoreProject.Services.UserServices
{
    public class CheckService : ICheckService
    {
        private readonly IGenericRepository<CheckEntity> _genericRepository; 
        public CheckService(IGenericRepository<CheckEntity> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public void Create(CheckEntity checkEntity)
        {
            _genericRepository.Create(checkEntity);
        }

        public CheckEntity GetById(long id)
        {
            CheckEntity dbRecord = _genericRepository.Table
                .Include(check => check.Products)
                .FirstOrDefault(check => check.Id == id);
            if(dbRecord == null)
            {
                return null; 
            }
            return dbRecord;
        }

        public bool Update(CheckEntity checkEntity)
        {
            try
            {
                _genericRepository.Table.Update(checkEntity);
                _genericRepository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        
        public CheckEntity GetByBuyerFK(long buyerFK)
        {
            CheckEntity dbRecord = _genericRepository.Table
         .FirstOrDefault(check => check.BuyerFK == buyerFK);
            if (dbRecord == null)
            {
                return null;
            }
            return dbRecord;
        }

    }
}
