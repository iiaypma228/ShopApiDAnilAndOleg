using System;
using System.Linq;
using System.Linq.Expressions;

namespace Server.API.Database.Interfaces
{
	//CRUD
	public interface IRepository<T> where T : class
	{
		IQueryable<T> Read();
		IQueryable<T> Read(Expression<Func<T, bool>> expressionWhere);
        IQueryable<T> ReadTracking(Expression<Func<T, bool>> expressionWhere);
        void Create(T item);
		void Update(T item);
		void Delete(T item);
    }
}
