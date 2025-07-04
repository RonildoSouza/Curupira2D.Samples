﻿using Curupira2D.ECS;
using Curupira2D.ECS.Components.Drawables;
using Curupira2D.Extensions;
using Curupira2D.Samples.Android.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Linq;

namespace Curupira2D.Samples.Android.Common.Scenes
{
    public abstract class SceneBase(bool activeReturnButton = true) : Scene
    {
        Entity _returnButtonEntity;

        public TouchLocation TouchLocation { get; set; }
        public Rectangle TouchLocationRectangle { get; set; }

        public override void LoadContent()
        {
            var returnButtonTexture = GameCore.Content.Load<Texture2D>("Common/ReturnButton");
            _returnButtonEntity = CreateEntity(
                "returnButton",
                ScreenWidth - returnButtonTexture.Width,
                returnButtonTexture.Height + 10f)
                .AddComponent(new SpriteComponent(returnButtonTexture))
                .SetActive(activeReturnButton);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            TouchLocationRectangle = Rectangle.Empty;

            var touchCollections = TouchPanel.GetState();

            if (touchCollections.Count == 0)
                return;

            TouchLocation = touchCollections.FirstOrDefault();
            TouchLocationRectangle = new Rectangle(
                (int)TouchLocation.Position.X, (int)TouchLocation.Position.Y, 0, 0);

            if (activeReturnButton
                && TouchLocation.State == TouchLocationState.Released
                && TouchLocationRectangle.Intersects(_returnButtonEntity.GetHitBox()))
                GameCore.SetScene<MenuScene>();
        }
    }
}

