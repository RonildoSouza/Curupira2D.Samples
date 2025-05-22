using Curupira2D.AI.BehaviorTree;
using Curupira2D.AI.BehaviorTree.Leafs;
using Curupira2D.Samples.Desktop.Systems.BehaviorTreeAndPathfinder;
using Curupira2D.ECS;

namespace Curupira2D.Samples.Desktop.BehaviorTree.Conditions
{
    public class HasFreeSpaceInInventoryCondition(Scene scene) : Leaf
    {
        private readonly MinerControllerSystem _minerControllerSystem = scene.GetSystem<MinerControllerSystem>();

        public override BehaviorState Update(IBlackboard blackboard)
            => _minerControllerSystem?.MinerState.IsInventoryFull ?? true ? Failure() : Success();
    }
}
