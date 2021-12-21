using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Data
{
    //a interface que vai criar o repositorio
    public interface IGenericRepository<T> where T : class //como eu não sei o nome da classe ponho T que uma IEntity
    {
        IQueryable<T> GetAll(); //um método que devolve todas as entidades..Quais? as que o T tiver a usar... neste caso product.. cliente... fornecedores... etc

        Task<T> GetByIdAsync(int id); //posso por id pq é a única coisa que é comum

        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<bool> ExistAsync(int id);
    }
}
