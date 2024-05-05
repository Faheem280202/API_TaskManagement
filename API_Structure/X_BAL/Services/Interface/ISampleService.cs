using API_Structure.X_BAL.DomainModels;
using API_Structure.X_BAL.DomainModels.Models;

namespace API_Structure.X_BAL.Services.Interface
{
    public interface ISampleService
    {
        public JsonResponse Sample(int ID);
    }
}
