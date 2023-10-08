using Server.API.Database;
using Server.API.Models;
using Server.API.Services.Interfaces;
using System.Linq.Expressions;

namespace Server.API.Services.Classes
{
    public class ProviderService : Service, IProviderService
    {
        public ProviderService(IUnitOfWork uow) : base(uow)
        {
        }

        public void Delete(Provider item)
        {
            this.Delete(new List<Provider> { item });
        }

        public void Delete(IList<Provider> items)
        {
            foreach (var item in items)
            {
                this.uow.ProviderRepository.Delete(item);
            }
            this.uow.Save();
        }

        public IList<Provider> Read()
        {
            return this.uow.ProviderRepository.Read().ToList();
        }

        public IList<Provider> Read(Expression<Func<Provider, bool>> where)
        {
            return this.uow.ProviderRepository.Read(where).ToList();
        }

        public Provider Read(object id)
        {
            return this.uow.ProviderRepository.Read(i => i.Id == (int)id).FirstOrDefault();
        }

        public void Save(Provider item)
        {
            this.Save(new List<Provider> { item });
        }

        public void Save(IList<Provider> items)
        {
            foreach (var item in items)
            {
                var exist = this.uow.ProviderRepository.Read(i => i.Id == item.Id).FirstOrDefault();
                if (exist == null)
                {
                    this.uow.ProviderRepository.Create(item);
                }
                else
                {
                    this.uow.ProviderRepository.Update(item);
                }
            }
            this.uow.Save();
        }
    }
}
