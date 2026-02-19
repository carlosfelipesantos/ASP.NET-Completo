using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Produto> GetProdutos()
        {
           return _context.Produtos;
        }

        public Produto GetProduto(int id)
        {
            return _context.Produtos.Include(p => p.Categoria).FirstOrDefault(p => p.ProdutoId == id);

        }

        public Produto Create(Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return produto;
        }

            public bool Update(Produto produto)
        {
            _context.Produtos.Update(produto);
            _context.SaveChanges();
            return true;
        }
        
        public bool Delete(int id)
        {
            _context.Produtos.Remove(GetProduto(id));
            _context?.SaveChanges();
            return true;
        }

        
    }
}
