using Microsoft.EntityFrameworkCore;

namespace FortuneTeller.Service.Models
{
    public class FortuneContext : DbContext
    {
        public FortuneContext(DbContextOptions<FortuneContext> options) :
            base(options)
        {

        }
        public DbSet<FortuneEntity> Fortunes { get; set; }
    }
}
