﻿using Curupira2D.AI.BehaviorTree;
using Curupira2D.ECS;
using Curupira2D.ECS.Components.Drawables;
using Curupira2D.ECS.Systems;
using Curupira2D.ECS.Systems.Attributes;
using Curupira2D.Extensions;
using Curupira2D.Samples.DesktopGL.Common.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Curupira2D.Samples.DesktopGL.Systems.BehaviorTreeAndPathfinder
{
    [RequiredComponent(typeof(MinerControllerSystem), typeof(SpriteAnimationComponent))]
    public class MinerControllerSystem(IBlackboard blackboard) : ECS.System, ILoadable, IUpdatable, IRenderable
    {
        Texture2D _pixelTexture;
        Entity _miner;

        SpriteAnimationComponent _movementSpriteAnimationComponent, _mineSpriteAnimationComponent;
        TextComponent _textComponent;

        public MinerState MinerState { get; private set; } = new();

        public void LoadContent()
        {
            _pixelTexture = Scene.GameCore.GraphicsDevice.CreateTextureCircle(4, Color.Red * 0.5f);

            _movementSpriteAnimationComponent = new SpriteAnimationComponent(
                texture: Scene.GameCore.Content.Load<Texture2D>("AI/GoblinSpritesheetWalking"),
                frameRowsCount: 1,
                frameColumnsCount: 8,
                frameTimeMilliseconds: 100,
                animateType: AnimateType.PerRow,
                sourceRectangle: new Rectangle(0, 0, 24, 24),
                isLooping: true,
                layerDepth: 0.02f);

            _mineSpriteAnimationComponent = new SpriteAnimationComponent(
                texture: Scene.GameCore.Content.Load<Texture2D>("AI/GoblinSpritesheetMining"),
                frameRowsCount: 1,
                frameColumnsCount: 10,
                frameTimeMilliseconds: 100,
                animateType: AnimateType.PerRow,
                sourceRectangle: new Rectangle(0, 0, 64, 64),
                isLooping: true,
                layerDepth: 0.02f);

            _miner = Scene.CreateEntity("miner", default)
                .AddComponent(_movementSpriteAnimationComponent);

            _textComponent = ((SceneBase)Scene).ShowText($"Energy: {MinerState.Energy.ToString().PadLeft(3, '0')}" +
                $"\nInventory Capacity: {MinerState.InventoryCapacity.ToString().PadLeft(2, '0')}" +
                $"\nMiner Action: {MinerState.CurrentMinerAction}",
                x: 100f,
                y: 50f,
                color: Color.White);
        }

        public void Update()
        {
            StopAnimation();
            PlayMovementAnimation();
            PlayMineAnimation();
            PlaySleepAnimation();
            HorizontalFlipMinerAnimation();

            _textComponent.Text = $"Energy: {MinerState.Energy.ToString().PadLeft(3, '0')}" +
                $"\nInventory Capacity: {MinerState.InventoryCapacity.ToString().PadLeft(2, '0')}" +
                $"\nMiner Action: {MinerState.CurrentMinerAction}";
        }

        public void Draw(ref IReadOnlyCollection<Entity> entities)
        {
            // DRAW EDGES FOR DEBUG
            var path = MinerState.CurrentMinerAction switch
            {
                MinerState.MinerAction.GoToMine => blackboard.Get<IEnumerable<Vector2>>("NearbyGoldMinePath"),
                MinerState.MinerAction.GoToDeposit => blackboard.Get<IEnumerable<Vector2>>("NearbyGoldMinePathToDeposit"),
                MinerState.MinerAction.GoHome => blackboard.Get<IEnumerable<Vector2>>("NearbyGoldMinePathToHome"),
                _ => null
            };

            for (int i = 0; i < path?.Count(); i++)
                Scene.SpriteBatch.Draw(_pixelTexture, path.ElementAt(i), null, Color.White, 0f, new Vector2(_pixelTexture.Width * 0.5f), 1f, SpriteEffects.None, 0.02f);
        }

        private void PlayMovementAnimation()
        {
            if (!_movementSpriteAnimationComponent.IsPlaying
                && (MinerState.CurrentMinerAction == MinerState.MinerAction.GoToMine
                    || MinerState.CurrentMinerAction == MinerState.MinerAction.GoHome
                    || MinerState.CurrentMinerAction == MinerState.MinerAction.GoToDeposit))
            {
                _movementSpriteAnimationComponent.IsPlaying = true;
                _mineSpriteAnimationComponent.IsPlaying = false;

                _miner.RemoveComponent<SpriteAnimationComponent>();
                _miner.AddComponent(_movementSpriteAnimationComponent);
            }
        }

        private void StopAnimation()
        {
            if ((_movementSpriteAnimationComponent.IsPlaying || _mineSpriteAnimationComponent.IsPlaying)
                && (MinerState.CurrentMinerAction == MinerState.MinerAction.Idle
                    || MinerState.CurrentMinerAction == MinerState.MinerAction.Sleep))
            {
                _movementSpriteAnimationComponent.IsPlaying = false;
                _mineSpriteAnimationComponent.IsPlaying = false;

                _miner.RemoveComponent<SpriteAnimationComponent>();
                _miner.AddComponent(_movementSpriteAnimationComponent);
            }
        }

        private void PlayMineAnimation()
        {
            if (!_mineSpriteAnimationComponent.IsPlaying && MinerState.CurrentMinerAction == MinerState.MinerAction.Mine)
            {
                _movementSpriteAnimationComponent.IsPlaying = false;
                _mineSpriteAnimationComponent.IsPlaying = true;

                _miner.RemoveComponent<SpriteAnimationComponent>();
                _miner.AddComponent(_mineSpriteAnimationComponent);
            }
        }

        private void PlaySleepAnimation()
        {
            if (MinerState.CurrentMinerAction == MinerState.MinerAction.Sleep)
            {
                _movementSpriteAnimationComponent.CurrentFrameColumn = 0;
                _miner.SetActive(false);
            }
            else
                _miner.SetActive(true);
        }

        private void HorizontalFlipMinerAnimation()
        {
            if ((
                    (MinerState.CurrentDirection.X > 0 && MinerState.CurrentDirection.Y < 0)        // UP AND RIGHT
                    || (MinerState.CurrentDirection.X > 0 && MinerState.CurrentDirection.Y > 0)     // DOWN AND RIGHT
                    || (MinerState.CurrentDirection.X == 1 && MinerState.CurrentDirection.Y == 0)   // RIGHT
                ) && _movementSpriteAnimationComponent.SpriteEffect == SpriteEffects.FlipHorizontally)
            {
                _movementSpriteAnimationComponent.SpriteEffect = SpriteEffects.None;
                _mineSpriteAnimationComponent.SpriteEffect = SpriteEffects.None;
            }

            if ((
                    (MinerState.CurrentDirection.X < 0 && MinerState.CurrentDirection.Y < 0)        // UP AND LEFT
                    || (MinerState.CurrentDirection.X < 0 && MinerState.CurrentDirection.Y > 0)     // DOWN AND LEFT
                    || (MinerState.CurrentDirection.X == -1 && MinerState.CurrentDirection.Y == 0)  // LEFT
                ) && _movementSpriteAnimationComponent.SpriteEffect == SpriteEffects.None)
            {
                _movementSpriteAnimationComponent.SpriteEffect = SpriteEffects.FlipHorizontally;
                _mineSpriteAnimationComponent.SpriteEffect = SpriteEffects.FlipHorizontally;
            }
        }
    }

    public class MinerState
    {
        public enum MinerAction
        {
            Idle,
            GoToMine,
            Mine,
            GoToDeposit,
            GoHome,
            Sleep
        }

        public static int MaxEnergy => 100;
        public static int MaxInventoryCapacity => 3;
        public static float MaxSpeed => 60f;

        public int Energy;
        public int InventoryCapacity;
        public MinerAction CurrentMinerAction = MinerAction.Idle;
        public Vector2 CurrentDirection = Vector2.Zero;

        public bool IsFatigued => Energy >= MaxEnergy;
        public bool IsInventoryFull => InventoryCapacity >= MaxInventoryCapacity;
    }
}

