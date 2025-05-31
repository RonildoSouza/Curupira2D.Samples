﻿using Curupira2D.Samples.DesktopGL.Common.Systems;
using Curupira2D.ECS.Components.Drawables;
using Curupira2D.ECS.Systems.Attributes;
using Microsoft.Xna.Framework.Graphics;

namespace Curupira2D.Samples.DesktopGL.Systems.SceneGraph
{
    [RequiredComponent(typeof(CharacterMovementSystem), typeof(SpriteComponent))]
    class CharacterMovementSystem : EntityMovementSystemBase
    {
        protected override string EntityUniqueId => "character";

        public override void LoadContent()
        {
            var characterTexture = Scene.GameCore.Content.Load<Texture2D>("SceneGraph/character");

            Scene.CreateEntity(EntityUniqueId, Scene.ScreenCenter)
                .AddComponent(new SpriteComponent(characterTexture));

            base.LoadContent();
        }
    }
}
