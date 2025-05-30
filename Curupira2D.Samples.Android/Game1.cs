using Curupira2D.Samples.Android.Scenes;

namespace Curupira2D.Samples.Android
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
