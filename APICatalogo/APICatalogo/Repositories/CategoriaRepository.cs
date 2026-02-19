using APICatalogo.Context;
using APICatalogo.Models;

namespace APICatalogo.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Categoria> GetCategorias()
        {
           return _context.Categorias.ToList();
        }


        public Categoria GetCategoria(int id)
        {
            return _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
        }

     
        public Categoria CreateCategoria(Categoria categoria)
        {
                if(categoria == null) 
                    throw new ArgumentNullException(nameof(categoria));

                _context.Categorias.Add(categoria);
                _context.SaveChanges();

                return categoria;
            }

        public Categoria UpdateCategoria(Categoria categoria)
        {
            if (categoria == null)
                throw new ArgumentNullException(nameof(categoria));

            _context.Categorias.Update(categoria);
            _context.SaveChanges();

            return categoria;

        }

        public Categoria DeleteCategoria(int id)
        {
           var categoria = _context.Categorias.Find(id);

            if (categoria == null)
                throw new ArgumentNullException(nameof(categoria));

            _context.Categorias.Remove(categoria); 
            
            _context.SaveChanges();

            return categoria;
        }

     
      

       
    }
}
