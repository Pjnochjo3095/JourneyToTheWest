using RoadToWest.Data.Extensions;
using RoadToWest.Data.Models;
using RoadToWest.Data.Repositories;
using RoadToWest.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadToWest.Data.Domains
{
    public class ActorDomain
    {
        private IActorRepository _actorRepo;
        private IUserRepository _userRepo;
        private ISceneCharacterRepository _characterRepo;
        public ActorDomain(IActorRepository actorRepository, IUserRepository userRepository, ISceneCharacterRepository sceneCharacterRepository)
        {
            _actorRepo = actorRepository;
            _userRepo = userRepository;
            _characterRepo = sceneCharacterRepository;
        }

        public Actor Create(ActorCreateModel model, string img)
        {
            var user = _userRepo.Get().FirstOrDefault(s => s.Id == model.Id);
            user.IsActor = true;
            _userRepo.Update(user);
            return _actorRepo.CreateActor(model, img);
        }

        public ActorViewModel GetById(string id)
        {
            var result = _actorRepo.GetById(id);
            if (result != null)
            {
                var viewModel = new ActorViewModel
                {
                    Id = result.Id,
                    Name = result.Name,
                    Email = result.Email,
                    Description = result.Description,
                    Phone = result.Phone,
                    Image = result.Image,
                    Status = result.Status
                };
                return viewModel;
            }
            return null;
        }
        public Actor EnableActor(string id)
        {
            var result = _actorRepo.GetById(id);
            var updateUser = _userRepo.FindById(id);
            if(result.Status == false)
            {
                result.Status = true;
                updateUser.Status = UserStatus.ENABLE;
            }
            else
            {
                updateUser.Status = UserStatus.DISABLE;
                result.Status = false;
            }
            _userRepo.Update(updateUser);
            return _actorRepo.Update(result).Entity;
        }

        public string EditActor(string id, ActorUpdateModel model, string img)
        {
            var actor = _actorRepo.Get().Where(s => s.Id == id).FirstOrDefault();
            if (actor != null)
            {
                var updateActor = _actorRepo.EditActor(actor, model, img);
                return _actorRepo.Update(updateActor).Entity.Id;
            }
            else
            {
                return null;
            }
        }
        public object GetActor(ActorFilter filter, string sort, int page, int limit)
        {
            var query = _actorRepo.Get();
            int totalPage = 0;
            if (limit > -1)
            {
                totalPage = query.Count() / limit;
            }
            return query.GetData(filter, sort, page, limit, totalPage);
        }
        public List<string> GetRoles() {
            List<string> result = new List<string>();
            var actors = _actorRepo.Get().ToList();
            foreach (Actor actor in actors) 
            {
                var user = _userRepo.Get().Where(s => s.Id == actor.Id).FirstOrDefault();
                List<UserRole> roles = user.UserRole.ToList();
                var str = "";
                foreach (UserRole role in roles)
                {
                    str += role.Role.Name + ": ";
                }
                result.Add(str);
                str = "";
            }
            return result;
        }
        public List<CharacterViewModel> GetCharactorOfActor(string actorId)
        {
            var characters = _characterRepo.Get().Where(s => s.ActorId == actorId && s.Scene.Status != SceneStatus.DONE && s.Scene.Status != SceneStatus.DELETE)
                .Select(s => new CharacterViewModel
                {
                  Character = s.Character,
                  SceneId = s.SceneId,
                  SceneName = s.Scene.Name,
                  script_link = s.ScriptLink,
                });

            if (characters != null)
            {
                return characters.ToList();
            }
            else
            {
                return null;
            }
        }
        public List<SceneViewModel> GetScenes(string id)
        {
            List<SceneViewModel> lists = new List<SceneViewModel>();
            lists = _characterRepo.Get().Where(s => s.ActorId == id && s.Scene.Status != SceneStatus.DONE && s.Scene.Status != SceneStatus.DELETE)
                        .Select(s => new SceneViewModel
                        {
                           Id = s.Scene.Id,
                           Name= s.Scene.Name,
                           Description = s.Scene.Description,
                           Location =s.Scene.Location,
                           Status = s.Scene.Status,
                           Snapshot = s.Scene.SnapShot,
                           TimeStart = s.Scene.TimeStart,
                           TimeEnd = s.Scene.TimeEnd,
                           Character = s.Character,
                        }).ToList();
            return lists;
        }
        public List<SceneViewModel> GetScenesIsFinished(string id)
        {
            List<SceneViewModel> lists = new List<SceneViewModel>();
            lists = _characterRepo.Get().Where(s => s.ActorId == id && s.Scene.Status == SceneStatus.DONE)
                        .Select(s => new SceneViewModel
                        {
                            Id = s.Scene.Id,
                            Name = s.Scene.Name,
                            Description = s.Scene.Description,
                            Location = s.Scene.Location,
                            Status = s.Scene.Status,
                            Snapshot = s.Scene.SnapShot,
                            TimeStart = s.Scene.TimeStart,
                            TimeEnd = s.Scene.TimeEnd,
                            Character = s.Character
                        }).ToList();
            return lists;
        }

    }
}
