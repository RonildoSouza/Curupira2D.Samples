using Curupira2D.AI.BehaviorTree;
using Curupira2D.AI.BehaviorTree.Leafs;
using Curupira2D.ECS;
using Curupira2D.Samples.WindowsDX.Systems.BehaviorTreeAndPathfinder;
using System;

namespace Curupira2D.Samples.WindowsDX.BehaviorTree.Leafs
{
    public class MinerSleepAction(Scene scene) : Leaf
    {
        private readonly MinerControllerSystem _minerControllerSystem = scene.GetSystem<MinerControllerSystem>();
        private TimeSpan _elapsedTime = TimeSpan.Zero;

        public override BehaviorState Update(IBlackboard blackboard)
        {
            if (!scene.ExistsEntities(_ => _.Group == "goldMines" && _.Active))
            {
                _minerControllerSystem.MinerState.Energy = 0;
                _minerControllerSystem.MinerState.InventoryCapacity = 0;
                _minerControllerSystem.MinerState.CurrentMinerAction = MinerState.MinerAction.Sleep;
                return Running();
            }

            if (!_minerControllerSystem.MinerState.IsFatigued)
                return Failure();

            _elapsedTime += scene.GameTime.ElapsedGameTime;
            if (_minerControllerSystem.MinerState.CurrentMinerAction == MinerState.MinerAction.Idle && _minerControllerSystem.MinerState.IsFatigued)
            {
                _minerControllerSystem.MinerState.CurrentMinerAction = MinerState.MinerAction.Sleep;
                return Running();
            }

            if (_elapsedTime < TimeSpan.FromSeconds(10))
                return Running();

            _minerControllerSystem.MinerState.Energy = 0;
            _minerControllerSystem.MinerState.InventoryCapacity = 0;
            blackboard.Remove("NearbyGoldMine", false);
            _minerControllerSystem.MinerState.CurrentMinerAction = MinerState.MinerAction.Idle;
            _elapsedTime = TimeSpan.Zero;

            return Success();
        }
    }
}
