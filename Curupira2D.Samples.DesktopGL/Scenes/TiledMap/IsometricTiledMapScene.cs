using Curupira2D.Samples.DesktopGL.Common.Scenes;
using Curupira2D.Samples.DesktopGL.Systems.Camera;
using Curupira2D.Samples.DesktopGL.Systems.TiledMap;

namespace Curupira2D.Samples.DesktopGL.Scenes.TiledMap
{
    class IsometricTiledMapScene : SceneBase
    {
        public override void LoadContent()
        {
            SetTitle(nameof(IsometricTiledMapScene));

            AddSystem<IsometricBicycleAnimationSystem>();
            AddSystem(new MapSystem("TiledMap/IsometricTiledMap.tmx", "TiledMap/IsometricCity"));
            AddSystem(new CameraSystem(moveWithKeyboard: true, enabledRotation: false));

            ShowControlTips("MOVIMENT: Keyboard Arrows");

            base.LoadContent();
        }
    }
}
