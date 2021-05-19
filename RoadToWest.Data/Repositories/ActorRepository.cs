using Microsoft.EntityFrameworkCore;
using RoadToWest.Data.Models;
using RoadToWest.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadToWest.Data.Repositories
{
    public interface IActorRepository : IBaseRepository<Actor, string>
    {
        Actor PrepareCreate(ActorCreateModel model, string img);
        Actor CreateActor(ActorCreateModel model, string img);

        Actor EditActor(Actor actor,ActorUpdateModel model, string img);

        Actor GetById(string id);
    }
    public class ActorRepository : BaseRepository<Actor, string>, IActorRepository
    {
        public ActorRepository(DbContext context) : base(context)
        {

        }

        public Actor CreateActor(ActorCreateModel model, string img)
        {
            var actor = PrepareCreate(model,img);
            return Create(actor).Entity;
        }

        public Actor EditActor(Actor actor, ActorUpdateModel model, string img)
        {
            actor.Name = model.Name;
            actor.Description = model.Description;
            actor.Image = img;
            actor.Email = model.Email;
            actor.Phone = model.Phone;
            return actor;
        }

        public Actor GetById(string id)
        {
            return Get().FirstOrDefault(s => s.Id == id);
        }
        public Actor PrepareCreate(ActorCreateModel model, string img)
        {
            Actor actor = new Actor
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Image = img,
                Email = model.Email,
                Phone = model.Phone,
                Status = true,
            };
            
            return actor;
        }
    }



}
