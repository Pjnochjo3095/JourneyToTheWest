using Microsoft.EntityFrameworkCore;
using RoadToWest.Data.Models;
using RoadToWest.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoadToWest.Data.Repositories
{
    public partial interface ISceneRepository : IBaseRepository<Scene,String>
    {
        Scene PrepareCreate(SceneCreateModel model);
        Scene CreateScene(SceneCreateModel model);

        Scene EditScene(Scene entity, SceneUpdateModel model);
        Scene DeleteScene(Scene entity);
        
    }
    public partial class SceneRepository : BaseRepository<Scene, String>, ISceneRepository
    {
        public SceneRepository(DbContext context) : base(context)
        {
        }

        public Scene CreateScene(SceneCreateModel model)
        {
            Scene scene = PrepareCreate(model);
            return Create(scene).Entity;
        }

        public Scene DeleteScene(Scene entity)
        {
            entity.Status = SceneStatus.DELETE;
            return entity;
        }

        public Scene EditScene(Scene entity,SceneUpdateModel model)
        {
            entity.Name = model.Name;
            entity.SnapShot = model.Snapshot;
            entity.Description = model.Description;
            entity.TimeStart = model.TimeStart;
            entity.TimeEnd = model.TimeEnd;
            entity.Location = model.Location;
            return entity;
        }

        public Scene PrepareCreate(SceneCreateModel model)
        {
            Scene scene = new Scene
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                Description = model.Description,
                Location = model.Location,
                TimeStart = model.TimeStart,
                TimeEnd = model.TimeEnd,
                SnapShot = model.Snapshot,
                Status = SceneStatus.NEW,
            };
            return scene;
        }
    }
}
