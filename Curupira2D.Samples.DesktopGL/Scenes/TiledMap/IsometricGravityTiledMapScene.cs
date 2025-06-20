using Curupira2D.Samples.DesktopGL.Common.Scenes;
using Curupira2D.Samples.DesktopGL.Systems.Camera;
using Curupira2D.Samples.DesktopGL.Systems.TiledMap;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Curupira2D.Samples.DesktopGL.Scenes.TiledMap
{
    class IsometricGravityTiledMapScene : SceneBase
    {
        public override void LoadContent()
        {
            SetTitle(nameof(IsometricGravityTiledMapScene));

            SpriteSortMode = SpriteSortMode.Immediate;

            // 30 degrees for isometric (2:1 Projection) and 45 degrees (diamond-shaped)
            var angle = MathHelper.ToRadians(30f);

            var isoGravityX = Gravity.X * MathF.Sin(angle) + Gravity.Y * MathF.Cos(angle);
            var isoGravityY = Gravity.X * MathF.Cos(angle) + Gravity.Y * MathF.Sin(angle);

            Gravity = new Vector2(isoGravityX, isoGravityY);

            AddSystem<IsometricGravityBallSystem>();
            AddSystem(new MapSystem("TiledMap/IsometricGravityTiledMap.tmx", "TiledMap/isometric-sandbox-sheet"));
            AddSystem(new CameraSystem(moveWithKeyboard: true, enabledRotation: false));

            ShowControlTips("MOVIMENT: WASD"
                            + "\nZOOM: Mouse Wheel"
                            + $"\n\nGRAVITY: {Gravity}");

            base.LoadContent();
        }
    }
}
