﻿using Curupira2D.AI.BehaviorTree;
using Curupira2D.AI.BehaviorTree.Leafs;
using Curupira2D.Samples.DesktopGL.Systems.BehaviorTreeAndPathfinder;
using Curupira2D.ECS;
using System;

namespace Curupira2D.Samples.DesktopGL.BehaviorTree.Leafs
{
    public class MineGoldAction(Scene scene) : Leaf
    {
        private readonly MinerControllerSystem _minerControllerSystem = scene.GetSystem<MinerControllerSystem>();
        private readonly GoldMineControllerSystem _goldMineControllerSystem = scene.GetSystem<GoldMineControllerSystem>();
        private TimeSpan _elapsedTime = TimeSpan.Zero;

        public override BehaviorState Update(IBlackboard blackboard)
        {
            if (_minerControllerSystem.MinerState.CurrentMinerAction != MinerState.MinerAction.Mine)
                return Failure();

            _elapsedTime += scene.GameTime.ElapsedGameTime;
            if (_elapsedTime >= TimeSpan.FromSeconds(1))
            {
                _minerControllerSystem.MinerState.Energy = _minerControllerSystem.MinerState.Energy + Random.Shared.Next(1, 5);
                _minerControllerSystem.MinerState.Energy = _minerControllerSystem.MinerState.Energy > MinerState.MaxEnergy ? MinerState.MaxEnergy : _minerControllerSystem.MinerState.Energy;
                _minerControllerSystem.MinerState.InventoryCapacity++;
                _elapsedTime = TimeSpan.Zero;

                _goldMineControllerSystem.DecreaseAvailableGold(blackboard.Get<string>("NearbyGoldMineEntityUniqueId"));
            }

            if (_minerControllerSystem.MinerState.IsInventoryFull)
            {
                _minerControllerSystem.MinerState.CurrentMinerAction = MinerState.MinerAction.GoToDeposit;
                return Success();
            }

            if (_minerControllerSystem.MinerState.IsFatigued)
            {
                _minerControllerSystem.MinerState.CurrentMinerAction = MinerState.MinerAction.GoHome;
                return Success();
            }

            if (!_goldMineControllerSystem.ThereIsGoldAvailable(blackboard.Get<string>("NearbyGoldMineEntityUniqueId")))
            {
                _minerControllerSystem.MinerState.CurrentMinerAction = MinerState.MinerAction.Idle;
                blackboard.Remove("NearbyGoldMinePath");
                return Success();
            }

            return Running();
        }
    }
}
