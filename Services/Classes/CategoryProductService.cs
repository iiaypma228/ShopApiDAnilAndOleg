using Server.API.Database;
using Server.API.Models;
using Server.API.Services.Interfaces;
using System.Linq.Expressions;

namespace Server.API.Services.Classes
{
    public class CategoryProductService : Service,ICategoryProductService
    {
        public CategoryProductService(IUnitOfWork uow) : base(uow)
        {
        }

        public void Delete(CategoryProduct item)
        {
            this.Delete(new List<CategoryProduct> { item });
        }

        public void Delete(IList<CategoryProduct> items)
        {
            foreach (var item in items)
            {
                this.uow.CategoryProductRepository.Delete(item);
            }
            this.uow.Save();
        }

        public IList<CategoryProduct> Read()
        {
            return this.uow.CategoryProductRepository.Read().ToList();
        }

        public IList<CategoryProduct> Read(Expression<Func<CategoryProduct, bool>> where)
        {
            return this.uow.CategoryProductRepository.Read(where).ToList();
        }

        public CategoryProduct Read(object id)
        {
            return this.uow.CategoryProductRepository.Read(i => i.Id == (int)id).FirstOrDefault();
        }

        public void Save(CategoryProduct item)
        {
            this.Save(new List<CategoryProduct> { item });
        }

        public void Save(IList<CategoryProduct> items)
        {
            foreach (var item in items)
            {
                var exist = this.uow.CategoryProductRepository.Read(i => i.Id == item.Id).FirstOrDefault();
                if (exist == null)
                {
                    this.uow.CategoryProductRepository.Create(item);
                }
                else
                {
                    this.uow.CategoryProductRepository.Update(item);
                }
            }
            this.uow.Save();
        }
    }
}
