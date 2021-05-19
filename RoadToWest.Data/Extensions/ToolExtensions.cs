using RoadToWest.Data.Models;
using RoadToWest.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadToWest.Data.Extensions
{
    public static partial class ToolExtensions
    {
        private static IQueryable<Tool> Filter(this IQueryable<Tool> query, ToolFilter filter)
        {
            if (filter.Ids != null)
            {
                query = query.Where(s => filter.Ids.Contains(s.Id));
            }
            if (filter.Name_Contains != null && filter.Name_Contains.Length > 0)
            {
                query = query.Where(s => s.Name.Contains(filter.Name_Contains));
            }
            return query;
        }
        private static IQueryable<Tool> Sort(this IQueryable<Tool> query, string sort)
        {
            if (sort != null && sort.Length > 0)
            {
                var asc = sort[0] == 'a';
                var fieldName = sort.Split(" ")[1];
                switch (fieldName)
                {
                    case ToolFieldsSort.Name:
                        if (asc)
                        {
                            query = query.OrderBy(s => s.Name);
                        }
                        else
                        {
                            query = query.OrderByDescending(s => s.Name);
                        }
                        break;
                }
            }
            return query;
        }
        public static object GetData(this IQueryable<Tool> query, ToolFilter filter, string sort)
        {
            query = query.Filter(filter);
            query = query.Sort(sort);
            return query.Select(s => new ToolViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Amount = s.Amount,
                Description = s.Description,
                Image = s.Image,
                Status = s.Status
            }).ToList(); ;
        }
    }
}
