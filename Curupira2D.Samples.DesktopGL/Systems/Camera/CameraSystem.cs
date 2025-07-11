﻿using Curupira2D.ECS;
using Curupira2D.ECS.Components.Drawables;
using Curupira2D.ECS.Systems;
using Curupira2D.ECS.Systems.Attributes;
using Curupira2D.Extensions;
using Curupira2D.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Curupira2D.Samples.DesktopGL.Systems.Camera
{
    [RequiredComponent(typeof(CameraSystem), typeof(SpriteComponent))]
    class CameraSystem(bool moveWithKeyboard, bool enabledRotation = true) : ECS.System, ILoadable, IUpdatable
    {
        Vector2 _cameraPosition;

        public void LoadContent()
        {
            Scene.GameCore.IsMouseVisible = true;
            Scene.Camera2D.Position = new Vector2(0f, Scene.ScreenCenter.Y);
        }

        public void Update()
        {
            if (moveWithKeyboard)
            {
                _cameraPosition = Scene.Camera2D.Position;
                var direction = Vector2.Zero;

                if (Scene.KeyboardInputManager.IsKeyDown(Keys.A))
                    direction.X -= 1;

                if (Scene.KeyboardInputManager.IsKeyDown(Keys.W))
                    direction.Y -= 1;

                if (Scene.KeyboardInputManager.IsKeyDown(Keys.D))
                    direction.X += 1;

                if (Scene.KeyboardInputManager.IsKeyDown(Keys.S))
                    direction.Y += 1;

                _cameraPosition += (float)(500f * Scene.DeltaTime) * direction.GetSafeNormalize();
            }
            else
                _cameraPosition = Scene.MouseInputManager.GetPosition().ToVector2();

            Scene.Camera2D.Position = _cameraPosition;

            if (Scene.MouseInputManager.GetScrollWheelChange() != 0)
            {
                Scene.Camera2D.Zoom = Scene.MouseInputManager.GetScrollWheelChange() > 0
                    ? Scene.Camera2D.Zoom += 0.1f
                    : Scene.Camera2D.Zoom -= 0.1f;
            }

            if (enabledRotation && Scene.MouseInputManager.IsMouseButtonDown(MouseButton.Left))
                Scene.Camera2D.Rotation += 0.01f;

            if (enabledRotation && Scene.MouseInputManager.IsMouseButtonPressed(MouseButton.Right))
            {
                Scene.Camera2D.Rotation = 0f;
                Scene.Camera2D.Zoom = 1f;
            }
        }
    }
}
