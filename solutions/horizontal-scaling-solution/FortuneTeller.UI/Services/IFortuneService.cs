using System.Collections.Generic;
using System.Threading.Tasks;

namespace FortuneTeller.UI.Services
{
    public interface IFortuneService
    {
        Task<List<Fortune>> AllFortunesAsync();

        Task<Fortune> RandomFortuneAsync();
    }
}
