namespace APICatalogo.Repositories
{
    public interface IRepository<T> //T é um tipo genérico, ou seja, pode ser qualquer tipo de classe
    {
        //CRUD BASICO
        IEnumerable<T> GetAll(); //colecao de todas entidades do repositorio
        T GetById(int id);
        T Create(T entity);
        T Update(T entity);
        T Delete(int id);
       
    }
}
