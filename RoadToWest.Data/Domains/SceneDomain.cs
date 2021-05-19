using Microsoft.EntityFrameworkCore;
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
   public class SceneDomain
    {
        private ISceneRepository _sceneRepo;
        private ISceneCharacterRepository _sceneCharacterRepo;
        
        public SceneDomain(ISceneRepository sceneRepository, ISceneCharacterRepository sceneCharacterRepository)
        {
            this._sceneRepo = sceneRepository;
            this._sceneCharacterRepo = sceneCharacterRepository;
        }
        public Scene Create( SceneCreateModel model)
        {
            return _sceneRepo.CreateScene(model);
        }
        public DbSet<Scene> Get()
        {
            return _sceneRepo.Get();
        }
        public object GetScene(SceneFilter filter, string sort)
        {
            var query = Get();
            return query.GetData(filter,sort);
        }
        public Scene Update(string sceneId, SceneUpdateModel model)
        {
            var scene = _sceneRepo.Get().Where(s => s.Id == sceneId).FirstOrDefault();
            if(scene != null)
            {
                var updateScene = _sceneRepo.EditScene(scene,model);
                return _sceneRepo.Update(updateScene).Entity;
            }
            else
            {
                return null;
            }
        }
        public Scene Delete (string sceneId)
        {
            var scene = _sceneRepo.Get().Where(s => s.Id == sceneId).FirstOrDefault();
            if (scene != null)
            {
                var deleteScene = _sceneRepo.DeleteScene(scene);
                return _sceneRepo.Update(deleteScene).Entity;
            }
            return null;
        }
        public SceneViewModel GetSceneById(string sceneId)
        {
            var scene = _sceneRepo.Get().Where(s => s.Id == sceneId).FirstOrDefault();
            if (scene != null)
            {
                return new SceneViewModel {
                    Id = scene.Id,
                    Name = scene.Name,
                    Description = scene.Description,
                    Snapshot = scene.SnapShot,
                    Location = scene.Location,
                    TimeStart = scene.TimeStart,
                    TimeEnd  = scene.TimeEnd,
                    Status = scene.Status
                };
            }
            return null;
        }
        public bool AddCharacter(CharacterCreateModel model, string characterScript)
        {
            var newCharacter = _sceneCharacterRepo.CreateCharacter(model, characterScript);
            var scene = _sceneRepo.Get().FirstOrDefault(s => s.Id == model.SceneId);
            if( newCharacter != null)
            {
                scene.Status = SceneStatus.PROCESSING;
                _sceneRepo.Update(scene);
                return true;
            }
            return false;
        }
        public string GetScript(string characterId)
        {
            var currentCharacter = _sceneCharacterRepo.Get().FirstOrDefault(s => s.Id == characterId);
            if (currentCharacter != null)
            {
                return currentCharacter.ScriptLink;
            }
            return null;
        }

        public List<CharacterOfSceneModel> GetCharacters(string sceneId)
        {
            List<CharacterOfSceneModel> result = new List<CharacterOfSceneModel>();
            var characters = _sceneCharacterRepo.Get().Where(s => s.SceneId == sceneId && s.Status == CharacterStatus.ENABLE);
            foreach(var character in characters)
            {
                var temp = new CharacterOfSceneModel {
                    Id = character.Id,
                    ActorId = character.ActorId,
                    ActorName= character.Actor.Name,
                    ActorImage =character.Actor.Image,
                    Character = character.Character,
                    Description = character.Description,
                    SceneId  = character.SceneId,
                    SceneName = character.Scene.Name,
                    Status = character.Status,
                    ScriptLink = character.ScriptLink,
                };
                result.Add(temp);
            }
            return result;
        }
        public bool FinishScene(string sceneId)
        {
            var check = false;
            var scene = _sceneRepo.Get().FirstOrDefault(s => s.Id == sceneId);
            if (scene != null)
            {
                scene.Status = SceneStatus.DONE;
                scene.TimeEnd = DateTime.Now;
                _sceneRepo.Update(scene);
                check = true;
            }

            return check;
        }

        public bool DeleteCharacter(string characterId)
        {
            var check = false;
            var character = _sceneCharacterRepo.Get().FirstOrDefault(s => s.Id == characterId);
            if (character != null)
            {
                character.Status = CharacterStatus.DISABLE;
                _sceneCharacterRepo.Update(character);
                check = true;
            }

            return check;
        }

    }
}
