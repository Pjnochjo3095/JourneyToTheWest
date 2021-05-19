using RoadToWest.Data.Models;
using RoadToWest.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadToWest.Data.Extensions
{
    public static partial class SceneExtensions
    {
        private static IQueryable<Scene> Filter(this IQueryable<Scene> query, SceneFilter filter)
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
        private static IQueryable<Scene> Sort(this IQueryable<Scene> query, string sort)
        {
            if (sort != null && sort.Length > 0)
            {
                var asc = sort[0] == 'a';
                var fieldName = sort.Split(" ")[1];
                switch (fieldName)
                {
                    case SceneFieldsSort.Name:
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
        public static object GetData(this IQueryable<Scene> query, SceneFilter filter, string sort)
        {
            query = query.Filter(filter);
            query = query.Sort(sort);
            return query.Where(s=> s.Status == SceneStatus.NEW || s.Status == SceneStatus.PROCESSING).Select(s => new SceneViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Location = s.Location,
                TimeEnd = s.TimeEnd,
                TimeStart = s.TimeStart,
                Snapshot = s.SnapShot,
                Status = s.Status
            }).ToList();
        }
    }
}
    