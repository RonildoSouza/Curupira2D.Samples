using Curupira2D.Samples.WindowsDX.Common.Systems;
using Curupira2D.ECS.Components.Drawables;
using Curupira2D.ECS.Systems.Attributes;
using Curupira2D.Extensions;
using Microsoft.Xna.Framework;

namespace Curupira2D.Samples.WindowsDX.Systems.SpriteAnimation
{
    [RequiredComponent(typeof(CharacterMovementSystem), typeof(SpriteAnimationComponent))]
    class CharacterMovementSystem : EntityMovementSystemBase
    {
        protected override string EntityUniqueId => "character";

        public override void Update()
        {
            if (_entityToMove.IsCollidedWith(Scene, "explosion", true))
                Scene.SetCleanColor(Color.MonoGameOrange);
            else
                Scene.SetFallbackCleanColor();

            base.Update();
        }
    }
}
