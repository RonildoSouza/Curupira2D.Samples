﻿using Curupira2D.ECS.Components;
using Microsoft.Xna.Framework;

namespace Curupira2D.Samples.WindowsDX.Components.SceneGraph
{
    class EquipmentComponent : IComponent
    {
        public EquipmentComponent(float offsetPosX, float offsetPosY)
        {
            OffsetPosition = new Vector2(offsetPosX, offsetPosY);
        }

        public Vector2 OffsetPosition { get; }
    }
}
