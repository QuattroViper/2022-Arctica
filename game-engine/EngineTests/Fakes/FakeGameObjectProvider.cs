﻿using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Enums;
using Domain.Models;
using Engine.Interfaces;

namespace EngineTests.Fakes
{
    /**
     * Create game objects for testing
     */
    public class FakeGameObjectProvider
    {
        private readonly IWorldStateService worldStateService;

        public FakeGameObjectProvider(IWorldStateService worldStateService)
        {
            this.worldStateService = worldStateService;
        }

/*        public void GenerateWorld(List<BotObject> bot, IEnumerable<ResourceNode> resourceNodes, IEnumerable<ScoutTower> scoutTowers)
        {
            worldStateService.GetWorldState(); 
            worldStateService. = World.Create(bot, resourceNodes, scoutTowers);
        }*/

        public GameState GetFakeWorld()
        {
            worldStateService.Initialize();
            return worldStateService.GetState();
        }


        public ResourceNode GetWoodAt(Position position, int amount)
        {
            var wood = new ResourceNode
            {
                Id = Guid.NewGuid(),
                Type = ResourceType.Wood,
                Position = position,
                Amount = amount
            };
            worldStateService.AddResourceToMap(wood);
            return wood;
        }


        public BotObject GetBaseBotAt()
        {
            return worldStateService.CreateBotObject(Guid.NewGuid());
            ;
        }

        public BotObject GetBotAtDefault()
        {
            var bot = new BotObject
            {
                Id = Guid.NewGuid(),
                Position = new Position(),
            };
            worldStateService.AddBotObject(bot);
            return bot;
        }

        public BotObject GetBotWithActions()
        {
            var id = Guid.NewGuid();
            var bot = new BotObject
            {
                Id = id,
                Position = new Position()
            };
            worldStateService.AddBotObject(bot);
            return bot;
        }

        public ScoutTower GetScoutTowerAt(Position position)
        {
            var id = Guid.NewGuid();
            var scoutTower = new ScoutTower
            {
                Id = id,
                Position = position
            };
            return scoutTower;
        }


        public ScoutTower GetScoutTowerWithResourceNodes(Position postion, List<ResourceNode> resourceNodes)
        {
            ScoutTower scoutTower = GetScoutTowerAt(postion);

            scoutTower.Nodes = resourceNodes.Select(node => node.Id).ToList();

            return scoutTower;
        }

        public PlayerAction GetScoutAction(
            BotObject bot,
            Guid target,
            int numberOfUnits,
            int completionTick
        ) =>
            new PlayerAction
            (
                ActionType.Scout,
                numberOfUnits,
                target
            )
            {
                Bot = bot
            };

        public CommandAction GetSimpleScoutCommandAction() => new CommandAction
        {

            Type = ActionType.Scout,
            Units = 1,
            Id = Guid.Empty

        };
        
        public PlayerAction GetLumberAction(BotObject bot,
            Guid target,
            int numberOfUnits,
            int completionTick
        ) =>
            new PlayerAction
            (
                ActionType.Lumber,
                numberOfUnits,
                target
            )
            {
                Bot = bot
            };

        public PlayerAction GetStartCampfireAction(BotObject bot, Guid id, int numberOfUnits, int completionTick)
            => new (
                ActionType.StartCampfire,
                numberOfUnits,
                id
            )
            {
                Bot = bot
            };
    }
}