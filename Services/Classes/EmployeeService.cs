using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using Server.API.Database;
using Server.API.Models;
using Server.API.Services.Interfaces;

namespace Server.API.Services.Classes;

public class EmployeeService : Service, IEmployeeService
{
    public EmployeeService(IUnitOfWork uow) : base(uow)
    {
    }
        

    public void Save(Employee item)
    {
        this.Save(new List<Employee>() {item});
    }

    public void Save(IList<Employee> items)
    {
        foreach (var item in items)
        {
            var exs = this.uow.EmployeeRepository.Read(s => s.Id == item.Id).FirstOrDefault();

            if (exs != null)
            {
                item.Password = exs.Password;
                this.uow.EmployeeRepository.Update(item);
            }
            else
            {
                item.Password = this.GenerateHash(item.Password);
                this.uow.EmployeeRepository.Create(item);
            }
            
        }
        
        this.uow.Save();
    }

    public IList<Employee> Read()
    {
        return this.uow.EmployeeRepository.Read().ToList();
    }

    private string GenerateHash(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            // Хэшируем пароль
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Преобразуем хэш в строку шестнадцатеричных символов
            string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return hashedPassword;
        }
    }
    
    public IList<Employee> Read(Expression<Func<Employee,bool>> where)
    {
        return this.uow.EmployeeRepository.Read(where).ToList();
    }

    public Employee Read(object id)
    {
        return this.uow.EmployeeRepository.Read(i => i.Id == (int)id).FirstOrDefault();
    } 

    public void Delete(Employee item)
    {
        this.Delete(new List<Employee>() {item});
    }

    public void Delete(IList<Employee> items)
    {
        foreach (var item in items)
        {
            this.uow.EmployeeRepository.Delete(item);
        }
        this.uow.Save();
    }

    public Employee AuthUser(string email, string password)
    {
        Employee result = null;
        var user = this.uow.EmployeeRepository.Read(i => i.Email.ToLower() == email.ToLower()).FirstOrDefault();

        if (user != null)
        {
            if (user.Password == this.GenerateHash(password))
                result = user;
        }

        return result;
    }
}