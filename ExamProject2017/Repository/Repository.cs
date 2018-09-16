using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamProject2017.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExamProject2017.Repository 
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {


        private readonly ApplicationDbContext _context;
        private DbSet<T> entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            entities = _context.Set<T>();
        }

        public void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            _context.SaveChanges();
        }

        public T Get(string id)
        {
            return entities.SingleOrDefault(s => s.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public Task<List<T>> GetAllAsListAsync()
        {
            return entities.ToListAsync();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            _context.SaveChanges();
        }
    }
}

    

