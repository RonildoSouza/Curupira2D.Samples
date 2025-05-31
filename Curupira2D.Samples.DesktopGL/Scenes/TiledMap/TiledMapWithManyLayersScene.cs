using Curupira2D.Samples.DesktopGL.Systems.Camera;
using Curupira2D.Samples.DesktopGL.Systems.TiledMap;
using Curupira2D.Samples.DesktopGL.Common.Scenes;

namespace Curupira2D.Samples.DesktopGL.Scenes.TiledMap
{
    class TiledMapWithManyLayersScene : SceneBase
    {
        public override void LoadContent()
        {
            SetTitle(nameof(TiledMapWithManyLayersScene));

            AddSystem(new MapSystem("TiledMap/MapWithManyLayers.tmx"));
            AddSystem(new CameraSystem(moveWithKeyboard: true));

            ShowControlTips("MOVIMENT: WASD"
                            + "\nZOOM: Mouse Wheel"
                            + "\nROTATION: Mouse Left Button"
                            + "\nRESET ROTATION: Mouse Right Button",
                            y: 120f);

            base.LoadContent();
        }
    }
}
