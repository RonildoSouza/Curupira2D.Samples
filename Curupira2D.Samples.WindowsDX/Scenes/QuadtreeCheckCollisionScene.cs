using Curupira2D.Samples.WindowsDX.Systems.Quadtree;
using Curupira2D.Samples.WindowsDX.Common.Scenes;

namespace Curupira2D.Samples.WindowsDX.Scenes
{
    class QuadtreeCheckCollisionScene : SceneBase
    {
        public override void LoadContent()
        {
            SetTitle(nameof(QuadtreeCheckCollisionScene));

            AddSystem<QuadtreeCheckCollisionSystem>();

            ShowControlTips("MOVIMENT: Keyboard Arrows OR WASD");

            base.LoadContent();
        }
    }
}
