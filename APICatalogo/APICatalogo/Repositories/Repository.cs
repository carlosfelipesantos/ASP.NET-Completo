using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories
{
    public class Repository<T> : IRepository<T> where T : class //pedindo que T seja uma classe
    {
        protected readonly AppDbContext _context; //protected é que a clase pode ser acessada por classes que herdam dela

        public Repository(AppDbContext context)
        {
            _context = context;
        }


        public IEnumerable<T> GetAll()
        {
          return _context.Set<T>().AsNoTracking().ToList(); //set serve para acessar uma colecao ou uma tabela do banco de dados, e tolist para converter o resultado em uma lista
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id); //find é um método que busca um registro pelo id
        }


        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
           // _context.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
           _context.Set<T>().Update(entity);
          //  _context.SaveChanges();
            return entity;
        }


        public T Delete(int id)
        {
           _context.Set<T>().Remove(GetById(id));
          //  _context.SaveChanges();
            return GetById(id);
        }




    }
}
