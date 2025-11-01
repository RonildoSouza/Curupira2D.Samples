using Curupira2D.ECS.Components.Drawables;
using Curupira2D.Extensions;
using Curupira2D.Samples.DesktopGL.Common.Scenes;
using Curupira2D.Samples.DesktopGL.Systems.Camera;
using Microsoft.Xna.Framework;

namespace Curupira2D.Samples.DesktopGL.Scenes
{
    class CameraScene : SceneBase
    {
        public override void LoadContent()
        {
            SetTitle(nameof(CameraScene));

            var blockTexture = GameCore.GraphicsDevice.CreateTextureRectangle(100, Color.Red * 0.8f);

            CreateEntity("block", ScreenCenter)
                .AddComponent(new SpriteComponent(blockTexture));

            AddSystem(new CameraSystem(moveWithKeyboard: false));

            ShowControlTips("MOVIMENT: Mouse Cursor"
                            + "\nZOOM: Mouse Wheel"
                            + "\nROTATION: Mouse Left Button"
                            + "\nRESET ROTATION AND ZOOM: Mouse Right Button");

            base.LoadContent();
        }
    }
}
