﻿/**
 * https://github.com/nkast/Aether.Physics2D/blob/main/Samples/HelloWorld/Game1.cs
 */

using Curupira2D.ECS.Components.Drawables;
using Curupira2D.ECS.Components.Physics;
using Curupira2D.Extensions;
using Curupira2D.Samples.DesktopGL.Common.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Curupira2D.Samples.DesktopGL.Scenes
{
    class AetherPhysics2DHelloWorldScene : SceneBase
    {
        readonly float _playerBodyRadius = 1.5f / 2f; // player diameter is 1.5 meters
        readonly Vector2 _groundBodySize = new(8f, 1f); // ground is 8x1 meters
        Vector2 _cameraPosition = new(0, 1.70f); // camera is 1.7 meters above the ground
        BodyComponent _playerBodyComponent;

        public override void LoadContent()
        {
            SetTitle(nameof(AetherPhysics2DHelloWorldScene));

            /* Circle */
            var playerPosition = new Vector2(0, _playerBodyRadius);
            var playerTexture = GameCore.Content.Load<Texture2D>("AetherPhysics2D/CircleSprite");

            _playerBodyComponent = new BodyComponent(_playerBodyRadius, EntityType.Dynamic)
            {
                Restitution = 0.3f,
                Friction = 0.5f,
            };

            CreateEntity("circle", playerPosition)
                .AddComponent(
                    new SpriteComponent(texture: playerTexture, scale: new Vector2(_playerBodyRadius * 2f) / playerTexture.Bounds.Size.ToVector2()),
                    _playerBodyComponent);

            /* Ground */
            var groundPosition = new Vector2(0, _groundBodySize.Y * 3f);
            var groundTexture = GameCore.Content.Load<Texture2D>("AetherPhysics2D/GroundSprite");

            CreateEntity("ground", groundPosition)
                .AddComponent(
                    new SpriteComponent(texture: groundTexture, scale: _groundBodySize / groundTexture.Bounds.Size.ToVector2()),
                    new BodyComponent(_groundBodySize, EntityType.Static, EntityShape.Rectangle)
                    {
                        Restitution = 0.3f,
                        Friction = 0.5f,
                    });

            ShowControlTips("Press A or D to rotate the ball\n" +
                             "Press Space to jump\n" +
                             "Use arrow keys to move the camera");

            base.LoadContent();

            Camera2D.Position = _cameraPosition;
            Camera2D.Zoom = 30f;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardInputManager.Begin();

            // Move camera
            if (KeyboardInputManager.IsKeyDown(Keys.Left))
                _cameraPosition.X += 12f * DeltaTime;

            if (KeyboardInputManager.IsKeyDown(Keys.Right))
                _cameraPosition.X -= 12f * DeltaTime;

            if (KeyboardInputManager.IsKeyDown(Keys.Up))
                _cameraPosition.Y -= 12f * DeltaTime;

            if (KeyboardInputManager.IsKeyDown(Keys.Down))
                _cameraPosition.Y += 12f * DeltaTime;

            // We make it possible to rotate the player body
            if (KeyboardInputManager.IsKeyDown(Keys.A))
                _playerBodyComponent.ApplyTorque(-10);

            if (KeyboardInputManager.IsKeyDown(Keys.D))
                _playerBodyComponent.ApplyTorque(10);

            if (KeyboardInputManager.IsKeyPressed(Keys.Space))
                _playerBodyComponent.ApplyLinearImpulse(new Vector2(0f, -10f));

            Camera2D.Position = _cameraPosition;

            KeyboardInputManager.End();

            base.Update(gameTime);
        }
    }
}
