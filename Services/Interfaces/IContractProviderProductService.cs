using Server.API.Models;

namespace Server.API.Services.Interfaces
{
    public interface IContractProviderProductService : ICRUDService<ContractProviderProduct>
    {

        void SaveLink(IList<ContractProviderProduct> contractProviderProducts, string contractProviderId);

    }
}
