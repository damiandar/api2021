using System.Linq;

namespace Clase2DatabaseFirst.Helpers
{
    public interface IOrdenador<T>
	{
        IQueryable<T> Ordenar(IQueryable<T> entidad, string cadenaorden);
	}
}