using Curupira2D.ECS;

namespace Curupira2D.Samples.Desktop.BehaviorTree.Leafs
{
    public class PathfindingGoldDepositAction(Scene scene)
        : BasePathfindingStartingNearbyGoldMineAction(scene, bbPathfindKey: "NearbyGoldMinePathToDeposit", pointObjectName: "gold-deposit")
    { }
}
