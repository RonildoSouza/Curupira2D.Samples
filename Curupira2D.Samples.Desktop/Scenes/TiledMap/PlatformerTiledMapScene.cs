using Curupira2D.Samples.Desktop.Systems.TiledMap;
using Curupira2D.Samples.Desktop.Common.Scenes;

namespace Curupira2D.Samples.Desktop.Scenes.TiledMap
{
    class PlatformerTiledMapScene : SceneBase
    {
        public override void LoadContent()
        {
            SetTitle(nameof(PlatformerTiledMapScene));

            AddSystem(new MapSystem("TiledMap/PlatformerTiledMap.tmx", "TiledMap/PlatformerTileset"));
            AddSystem<CharacterMovementSystem>();

            ShowControlTips("MOVIMENT: Keyboard Arrows OR WASD", y: 120f);

            base.LoadContent();
        }
    }
}
