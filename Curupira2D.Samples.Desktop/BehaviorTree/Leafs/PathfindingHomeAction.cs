using Curupira2D.ECS;

namespace Curupira2D.Samples.Desktop.BehaviorTree.Leafs
{
    public class PathfindingHomeAction(Scene scene)
        : BasePathfindingStartingNearbyGoldMineAction(scene, bbPathfindKey: "NearbyGoldMinePathToHome", pointObjectName: "home")
    { }
}
