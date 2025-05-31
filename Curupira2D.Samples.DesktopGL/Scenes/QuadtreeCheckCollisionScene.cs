using Curupira2D.Samples.DesktopGL.Systems.Quadtree;
using Curupira2D.Samples.DesktopGL.Common.Scenes;

namespace Curupira2D.Samples.DesktopGL.Scenes
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
