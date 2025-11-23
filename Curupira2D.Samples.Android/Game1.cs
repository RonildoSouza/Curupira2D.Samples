using Curupira2D.Samples.Android.Scenes;

namespace Curupira2D.Samples.Android
{
    public class Game1 : GameCore
    {
        public Game1() : base(debugOptions: new Diagnostics.DebugOptions { DebugActive = false })
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
