using RoadToWest.Data.Models;
using RoadToWest.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadToWest.Data.Extensions
{
    public static class ActorExtension
    {
        private static IQueryable<Actor> Filter(this IQueryable<Actor> query, ActorFilter filter)
        {

            if (filter.Name_Contains != null && filter.Name_Contains.Length > 0)
            {
                query = query.Where(s => s.Name.Contains(filter.Name_Contains));
            }
            if (filter.Email != null && filter.Email.Length > 0)
            {
                query = query.Where(s => s.Email.Contains(filter.Email));
            }

            return query;
        }

        private static IQueryable<Actor> Sort(this IQueryable<Actor> query, string sort)
        {
            if (sort != null && sort.Length > 0)
            {
                var asc = sort[0] == 'a';
                var fieldName = sort.Split(" ")[1];
                switch (fieldName)
                {
                    case ActorSort.Name:
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

        private static IQueryable<Actor> Pagination(this IQueryable<Actor> query, int page, int limit)
        {
            if (limit > -1 && page >= 0)
            {
                query = query.Skip(page * limit).Take(limit);
            }
            return query;
        }


        public static object GetData(this IQueryable<Actor> query, ActorFilter filter, string sort, int page, int limit, int total)
        {
            query = query.Filter(filter);
            query = query.Pagination(page, limit);
            query = query.Sort(sort);
            return query.Select(s => new ActorViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Phone = s.Phone,
                Description = s.Description,
                Image = s.Image,
                Status = s.Status,
            }).ToList();
        }


    }
}
