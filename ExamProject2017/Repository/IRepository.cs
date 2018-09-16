using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using ExamProject2017.Models;
using ExamProject2017.Data;
using System.Threading.Tasks;

namespace ExamProject2017.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsListAsync();
        T Get(string id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}