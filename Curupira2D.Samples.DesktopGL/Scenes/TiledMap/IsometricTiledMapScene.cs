using Curupira2D.Samples.DesktopGL.Systems.TiledMap;
using Curupira2D.Samples.DesktopGL.Common.Scenes;

namespace Curupira2D.Samples.DesktopGL.Scenes.TiledMap
{
    class IsometricTiledMapScene : SceneBase
    {
        public override void LoadContent()
        {
            SetTitle(nameof(IsometricTiledMapScene));

            AddSystem<IsometricBicycleAnimationSystem>();
            AddSystem(new MapSystem("TiledMap/IsometricTiledMap.tmx", "TiledMap/IsometricCity"));

            ShowControlTips("MOVIMENT: Keyboard Arrows", y: 120f);

            base.LoadContent();
        }
    }
}
