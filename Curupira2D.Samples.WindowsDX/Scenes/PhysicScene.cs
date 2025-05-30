using Curupira2D.Samples.WindowsDX.Systems.Physic;
using Curupira2D.Samples.WindowsDX.Common.Scenes;

namespace Curupira2D.Samples.WindowsDX.Scenes
{
    class PhysicScene : SceneBase
    {
        public override void LoadContent()
        {

            SetTitle(nameof(PhysicScene));

            AddSystem<BallControllerSystem>();
            AddSystem<SquareControllerSystem>();
            AddSystem<BorderControllerSystem>();

            ShowControlTips("MOVIMENT: Keyboard Arrows OR WASD", x: 200f, y: 120f);

            base.LoadContent();
        }
    }
}
