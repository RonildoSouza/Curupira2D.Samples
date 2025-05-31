using Curupira2D.Samples.DesktopGL.Systems.SceneGraph;
using Curupira2D.Samples.DesktopGL.Common.Scenes;

namespace Curupira2D.Samples.DesktopGL.Scenes
{
    class SceneGraphScene : SceneBase
    {
        public override void LoadContent()
        {
            SetTitle(nameof(SceneGraphScene));

            AddSystem<CharacterMovementSystem>();
            AddSystem<EquipmentMovimentSystem>();

            ShowControlTips("MOVIMENT: Keyboard Arrows OR WASD\nEQUIPMENTS: Key 1, 2");

            base.LoadContent();
        }
    }
}
