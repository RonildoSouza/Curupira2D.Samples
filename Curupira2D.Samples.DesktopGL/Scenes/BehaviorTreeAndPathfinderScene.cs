using Curupira2D.AI.BehaviorTree;
using Curupira2D.ECS.Components.Drawables;
using Curupira2D.Samples.DesktopGL.Common.Scenes;
using Curupira2D.Samples.DesktopGL.Systems.BehaviorTreeAndPathfinder;
using Curupira2D.Samples.DesktopGL.Systems.Camera;
using Curupira2D.Samples.DesktopGL.Systems.TiledMap;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Curupira2D.Samples.DesktopGL.Scenes
{
    class BehaviorTreeAndPathfinderScene : SceneBase
    {
        public override void LoadContent()
        {
            SetTitle(nameof(BehaviorTreeAndPathfinderScene));

            var blackboard = new Blackboard();

            AddSystem<MinerControllerSystem>(blackboard);
            AddSystem<BehaviorTreeMinerControllerSystem>(blackboard);
            AddSystem<GoldMineControllerSystem>();
            AddSystem(new MapSystem("AI/BehaviorTreeAndPathfinderTiledMap.tmx", "AI/BehaviorTreeAndPathfinderTileset"));

            ShowControlTips(string.Empty, y: 50f, color: Color.White);

            var boatSpriteAnimationComponent = new SpriteAnimationComponent(
                texture: GameCore.Content.Load<Texture2D>("AI/BoatSpriteSheet"),
                frameRowsCount: 1,
                frameColumnsCount: 4,
                frameTimeMilliseconds: 150,
                animateType: AnimateType.PerRow,
                sourceRectangle: new Rectangle(0, 0, 48, 37),
                isLooping: true,
                isPlaying: true,
                layerDepth: 0.02f);
            CreateEntity("boat", default)
                .AddComponent(boatSpriteAnimationComponent);

            base.LoadContent();
        }
    }
}
