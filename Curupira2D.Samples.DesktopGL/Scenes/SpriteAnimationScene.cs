﻿using Curupira2D.ECS.Components.Drawables;
using Curupira2D.Samples.DesktopGL.Common.Scenes;
using Curupira2D.Samples.DesktopGL.Systems.SpriteAnimation;
using Microsoft.Xna.Framework.Graphics;

namespace Curupira2D.Samples.DesktopGL.Scenes
{
    class SpriteAnimationScene : SceneBase
    {
        public override void LoadContent()
        {
            SetTitle(nameof(SpriteAnimationScene));

            AddSystem<CharacterAnimationSystem>();
            AddSystem<CharacterMovementSystem>();

            // Create entity explosion in scene
            var explosionTexture = GameCore.Content.Load<Texture2D>("SpriteAnimation/explosion");

            CreateEntity("explosion", ScreenCenter)
                .AddComponent(new SpriteAnimationComponent(explosionTexture, 5, 5, 150, AnimateType.All, default, true, true));

            ShowControlTips("MOVIMENT: Keyboard Arrows OR WASD");

            base.LoadContent();
        }
    }
}
