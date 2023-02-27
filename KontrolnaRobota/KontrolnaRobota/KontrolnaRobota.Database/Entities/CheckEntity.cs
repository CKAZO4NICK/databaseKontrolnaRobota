

using Microsoft.EntityFrameworkCore.Query.Internal;

namespace KontrolnaRobota.Database.Entities
{
    public class CheckEntity
    {
        public CheckEntity()
        {
            Products = new List<ProductEntity>();
        }
        public long Id { get; set; }
        public long BuyerFK { get; set; }
        public BuyerEntity Buyer { get; set; }
        public DateTime DateOfBuying { get; set; }
        public ICollection<ProductEntity> Products { get; set; }
    }
}
