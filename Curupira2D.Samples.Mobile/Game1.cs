using Curupira2D.Samples.Mobile.Scenes;

namespace Curupira2D.Samples.Mobile
{
    public class Game1 : GameCore
    {
        public Game1() : base()
        {
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            SetScene<MenuScene>();

            base.LoadContent();
        }
    }
}
