using Curupira2D.Samples.DesktopGL.Common.Scenes;
using Curupira2D.Samples.DesktopGL.Systems.Physic;

namespace Curupira2D.Samples.DesktopGL.Scenes
{
    class PhysicScene : SceneBase
    {
        public override void LoadContent()
        {

            SetTitle(nameof(PhysicScene));

            AddSystem<BallControllerSystem>();
            AddSystem<SquareControllerSystem>();
            AddSystem<BorderControllerSystem>();

            ShowControlTips("MOVIMENT: Keyboard Arrows OR WASD");

            base.LoadContent();
        }
    }
}
