using Server.API.Database;
using Server.API.Models;
using Server.API.Services.Interfaces;
using System.Linq.Expressions;

namespace Server.API.Services.Classes
{
    public class ContractProviderProductService : Service, IContractProviderProductService
    {

        public ContractProviderProductService(IUnitOfWork uow) : base(uow)
        {
        }

        public void Delete(ContractProviderProduct item)
        {
            this.Delete(new List<ContractProviderProduct> { item });
        }

        public void Delete(IList<ContractProviderProduct> items)
        {
            foreach (var item in items)
            {
                this.uow.ContractProviderProductRepository.Delete(item);
            }
            this.uow.Save();
        }

        public IList<ContractProviderProduct> Read()
        {
            return this.uow.ContractProviderProductRepository.Read().ToList();
        }

        public IList<ContractProviderProduct> Read(Expression<Func<ContractProviderProduct, bool>> where)
        {
            return this.uow.ContractProviderProductRepository.Read(where).ToList();
        }

        public ContractProviderProduct Read(object id)
        {
            return this.uow.ContractProviderProductRepository.Read(i => i.Id == (string)id).FirstOrDefault();
        }

        public void Save(ContractProviderProduct item)
        {
            this.Save(new List<ContractProviderProduct> { item });
        }

        public void Save(IList<ContractProviderProduct> items)
        {
            foreach (var item in items)
            {
                var exist = this.uow.ContractProviderProductRepository.Read(i => i.Id == item.Id).FirstOrDefault();
                if (exist == null)
                {
                    this.uow.ContractProviderProductRepository.Create(item);
                }
                else
                {
                    this.uow.ContractProviderProductRepository.Update(item);
                }
            }
            this.uow.Save();
        }

        public void SaveLink(IList<ContractProviderProduct> items, string contractProviderId)
        {
            var olds = this.uow.ContractProviderProductRepository.Read(i => i.ContractProviderId == contractProviderId).ToList();

            foreach (var item in items)
            {
                var old = olds.Where(i => i.Id == item.Id).FirstOrDefault();

                if (old != null)
                    olds.Remove(old);
            }
            this.Save(items);
            this.Delete(olds);
        }
    }


}
