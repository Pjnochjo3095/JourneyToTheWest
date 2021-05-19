using Microsoft.EntityFrameworkCore;
using RoadToWest.Data.Models;
using RoadToWest.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoadToWest.Data.Repositories
{
    public partial interface ISceneCharacterRepository : IBaseRepository<SceneCharacter, string>
    {
        SceneCharacter PrepareCreate(CharacterCreateModel model, string characterScript);
        SceneCharacter CreateCharacter(CharacterCreateModel model, string characterScript);
    }

    public class SceneCharacterRepository : BaseRepository<SceneCharacter, string>, ISceneCharacterRepository
    {
        public SceneCharacterRepository(DbContext context) : base(context)
        {
        }

        public SceneCharacter CreateCharacter(CharacterCreateModel model, string characterScript)
        {
            var result = PrepareCreate(model, characterScript);
            return Create(result).Entity;

        }

        public SceneCharacter PrepareCreate(CharacterCreateModel model, string characterScript)
        {
            var newCharacter = new SceneCharacter
            {
                Id = Guid.NewGuid().ToString(),
                ActorId = model.ActorId,
                Character = model.Character,
                Description = model.Description,
                SceneId = model.SceneId,
                ScriptLink = characterScript,
                Status = CharacterStatus.ENABLE
            };
            return newCharacter;

        }
    }

}
