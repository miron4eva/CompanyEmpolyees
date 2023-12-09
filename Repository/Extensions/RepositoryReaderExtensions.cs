using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Reflection;
using Repository.Extensions.Utility;

namespace Repository.Extensions
{
    public static class RepositoryReaderExtensions
    {
        public static IQueryable<Reader> Search(this IQueryable<Reader> readers, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return readers;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return readers.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }
        public static IQueryable<Reader> Sort(this IQueryable<Reader> readers, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return readers.OrderBy(e => e.Name);
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Reader>(orderByQueryString);
            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Reader).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();
            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;
                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
                if (objectProperty == null)
                    continue;
                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction}, ");
            }
            if (string.IsNullOrWhiteSpace(orderQuery))
                return readers.OrderBy(e => e.Name);
            return readers.OrderBy(orderQuery);
        }
    }
}
