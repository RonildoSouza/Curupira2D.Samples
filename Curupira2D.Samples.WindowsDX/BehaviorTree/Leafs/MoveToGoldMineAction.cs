using Curupira2D.Samples.WindowsDX.Systems.BehaviorTreeAndPathfinder;
using Curupira2D.ECS;
using Curupira2D.Extensions;
using Microsoft.Xna.Framework;

namespace Curupira2D.Samples.WindowsDX.BehaviorTree.Leafs
{
    public class MoveToGoldMineAction(Scene scene) : BaseMoveCharacterWithPathfindAction(scene, entityUniqueId: "miner", bbPathfindKey: "NearbyGoldMinePath")
    {
        private readonly MinerControllerSystem _minerControllerSystem = scene.GetSystem<MinerControllerSystem>();

        protected override void RunningAction(Vector2 currentDirection)
        {
            _minerControllerSystem.MinerState.CurrentDirection = currentDirection.GetSafeNormalize();
            _minerControllerSystem.MinerState.CurrentMinerAction = MinerState.MinerAction.GoToMine;
        }

        protected override void SuccessAction()
        {
            _minerControllerSystem.MinerState.CurrentMinerAction = MinerState.MinerAction.Mine;
        }
    }
}
