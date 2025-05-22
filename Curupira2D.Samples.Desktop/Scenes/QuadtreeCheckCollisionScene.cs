using Curupira2D.Samples.Desktop.Systems.Quadtree;
using Curupira2D.Samples.Desktop.Common.Scenes;

namespace Curupira2D.Samples.Desktop.Scenes
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
