using APICatalogo.Context;

namespace APICatalogo.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IProdutoRepository _produtoRepo;

        private ICategoriaRepository _categoriaRepo;

        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IProdutoRepository ProdutoRepository
        {
            get 
            {
                   return _produtoRepo = _produtoRepo?? new ProdutoRepository(_context);
            }
        }

        public ICategoriaRepository CategoriaRepository
        {             get
            {
                return _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_context);
            }
        }

        public void Commit() //método para confirmar todas alteracoes pendentes nos repositorios
        {
           _context.SaveChanges();
        }

        public void Dispose()//método para liberar os recursos do contexto de dados
        {
            _context.Dispose();
        }
    }
}
