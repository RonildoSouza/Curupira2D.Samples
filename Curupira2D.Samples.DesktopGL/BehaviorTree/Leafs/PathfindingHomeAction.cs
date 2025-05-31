using Curupira2D.ECS;

namespace Curupira2D.Samples.DesktopGL.BehaviorTree.Leafs
{
    public class PathfindingHomeAction(Scene scene)
        : BasePathfindingStartingNearbyGoldMineAction(scene, bbPathfindKey: "NearbyGoldMinePathToHome", pointObjectName: "home")
    { }
}
