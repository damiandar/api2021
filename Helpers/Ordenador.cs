using System.Linq;
using System.Reflection;
using System.Text; 
using System; 
using System.Linq.Dynamic.Core;

namespace Clase2DatabaseFirst.Helpers
{
    public class Ordenador<T> : IOrdenador<T>
    {
        public IQueryable<T> Ordenar(IQueryable<T> entities, string orderByQueryString)
        {
            if (!entities.Any())
				return entities;

			if (string.IsNullOrWhiteSpace(orderByQueryString))
			{
				return entities;
			}

			var orderParams = orderByQueryString.Trim().Split(',');
			var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
			var orderQueryBuilder = new StringBuilder();

			foreach (var param in orderParams)
			{
				if (string.IsNullOrWhiteSpace(param))
					continue;

				var propertyFromQueryName = param.Split(" ")[0];
				var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

				if (objectProperty == null)
					continue;

				var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";

				orderQueryBuilder.Append($"{objectProperty.Name} {sortingOrder}, ");
			}

			var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
			return entities.OrderBy(orderQuery);
        }
		/*
		private void SearchByName(ref IQueryable<T> entidades, string ownerName)
		{
    		if (!entidades.Any() || string.IsNullOrWhiteSpace(ownerName))
        		return;
    			entidades = entidades.Where(o => o.Nombre.ToLower().Contains(ownerName.Trim().ToLower()));
		}*/
    }
}