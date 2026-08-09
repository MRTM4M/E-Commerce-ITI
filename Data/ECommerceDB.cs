using Microsoft.EntityFrameworkCore;

namespace E_commerce_iti.Data
{
    public class ECommerceDBcontext : DbContext
    {
        public ECommerceDBcontext(DbContextOptions<ECommerceDBcontext> options)
            : base(options)
        {
        }
    }
}
