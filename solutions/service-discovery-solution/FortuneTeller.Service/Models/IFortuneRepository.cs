using System.Collections.Generic;
using System.Threading.Tasks;

namespace FortuneTeller.Service.Models
{
    public interface IFortuneRepository
    {
        Task<List<FortuneEntity>> GetAllAsync();

        Task<FortuneEntity> RandomFortuneAsync();
    }
}
