using Curupira2D.AI.BehaviorTree;
using Curupira2D.ECS;
using Curupira2D.ECS.Components;
using Curupira2D.ECS.Components.Drawables;
using Curupira2D.ECS.Systems;
using Curupira2D.ECS.Systems.Attributes;
using Curupira2D.Samples.Desktop.BehaviorTree.Conditions;
using Curupira2D.Samples.Desktop.BehaviorTree.Leafs;
using Curupira2D.Samples.Desktop.Common.Scenes;
using Microsoft.Xna.Framework;
using static System.Formats.Asn1.AsnWriter;

namespace Curupira2D.Samples.Desktop.Systems.BehaviorTreeAndPathfinder
{
    [RequiredComponent(typeof(BehaviorTreeMinerControllerSystem), typeof(DumpComponent))]
    public class BehaviorTreeMinerControllerSystem(IBlackboard blackboard) : ECS.System, ILoadable, IUpdatable
    {
        AI.BehaviorTree.BehaviorTree _behaviorTree;
        TextComponent _textComponent;

        public void LoadContent()
        {
            var minerControllerSystem = Scene.GetSystem<MinerControllerSystem>();

            _behaviorTree = BehaviorTreeBuilder.GetInstance()
                .Selector()
                    .Leaf<PathfindingNearbyGoldMineAction>(Scene)
                    .Sequence()
                        .Conditional(bb => minerControllerSystem.MinerState.IsFatigued || !Scene.ExistsEntities(_ => _.Group == "goldMines" && _.Active))
                        .Leaf<PathfindingHomeAction>(Scene)
                        .Leaf<MoveToHomeAction>(Scene)
                        .Leaf<MinerSleepAction>(Scene)
                    .Close()
                    .Sequence()
                        .Inverter()
                            .Leaf<HasFreeSpaceInInventoryCondition>(Scene)
                        .Leaf<PathfindingGoldDepositAction>(Scene)
                        .Leaf<MoveToGoldDepositAction>(Scene)
                        .Leaf<DepositGoldAction>(Scene)
                    .Close()
                    .Sequence()
                        .Leaf<ThereIsGoldAvailableCondition>(Scene)
                        .Leaf<HasFreeSpaceInInventoryCondition>(Scene)
                        .Leaf<MoveToGoldMineAction>(Scene)
                        .Leaf<MineGoldAction>(Scene)
                    .Close()
                .Close()
            .Build(blackboard);

            _textComponent = ((SceneBase)Scene).ShowText(GetTreeStructure(), x: 660f, y: 480f, color: Color.White, scale: new Vector2(0.35f));
        }

        public void Update()
        {
            _behaviorTree?.Tick();
            _textComponent.Text = GetTreeStructure();
        }

        private string GetTreeStructure()
            => $"BEHAVIOR TREE STATE\n{_behaviorTree.GetTreeStructure(true).Replace("└──", "L").Replace("│", "I").Replace("├──", "I-")}";
    }
}
