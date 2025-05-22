using Curupira2D.Samples.Desktop.Systems.Camera;
using Curupira2D.Samples.Desktop.Systems.TiledMap;
using Curupira2D.Samples.Desktop.Common.Scenes;

namespace Curupira2D.Samples.Desktop.Scenes.TiledMap
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
