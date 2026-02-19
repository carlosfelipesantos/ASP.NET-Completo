using APICatalogo.Models;

namespace APICatalogo.Repositories
{
    public interface ICategoriaRepository
    {

        IEnumerable <Categoria> GetCategorias(); //Lista de objetos categorias
        Categoria GetCategoria(int id);
            Categoria CreateCategoria(Categoria categoria);
            Categoria UpdateCategoria(Categoria categoria);
            Categoria DeleteCategoria(int  id);
           

    }
}
