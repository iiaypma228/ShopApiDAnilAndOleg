using Server.API.Database;
using Server.API.Models;
using Server.API.Services.Interfaces;
using System.Linq.Expressions;

namespace Server.API.Services.Classes
{
    public class ContractProviderService : Service, IContractProviderService
    {

        public ContractProviderService(IUnitOfWork uow) : base(uow)
        {
        }

        public void Delete(ContractProvider item)
        {
            this.Delete(new List<ContractProvider> { item });
        }

        public void Delete(IList<ContractProvider> items)
        {
            foreach (var item in items)
            {
                this.uow.ContractProviderRepository.Delete(item);
            }
            this.uow.Save();
        }

        public IList<ContractProvider> Read()
        {
            return this.uow.ContractProviderRepository.Read().ToList();
        }

        public IList<ContractProvider> Read(Expression<Func<ContractProvider, bool>> where)
        {
            return this.uow.ContractProviderRepository.Read(where).ToList();
        }

        public ContractProvider Read(object id)
        {
            return this.uow.ContractProviderRepository.Read(i => i.Id == (string)id).FirstOrDefault();
        }

        public void Save(ContractProvider item)
        {
            this.Save(new List<ContractProvider> { item });
        }

        public void Save(IList<ContractProvider> items)
        {
            foreach (var item in items)
            {
                var exist = this.uow.ContractProviderRepository.Read(i => i.Id == item.Id).FirstOrDefault();
                if (exist == null)
                {
                    this.uow.ContractProviderRepository.Create(item);
                }
                else
                {
                    this.uow.ContractProviderRepository.Update(item);
                }
                if (item.ContractProviders != null) new ContractProviderProductService(this.uow).SaveLink(item.ContractProviders, item.Id);
            }
            this.uow.Save();
        }
    }

}
