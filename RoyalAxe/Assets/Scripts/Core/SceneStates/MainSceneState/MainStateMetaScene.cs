using FluentBehaviourTree;
using ProjectUI;

namespace Core.Launcher
{
    /// <summary>
    ///     Главный стейт игры.
    /// </summary>
    public class MainStateMetaScene : AbstractMetaSceneState
    {
        public MainStateMetaScene(IMetaSceneUIViewHolder maineSceneViewHolder) : base(maineSceneViewHolder) { }

        public override BehaviourTreeStatus Execute(TimeData time)
        {
            return BehaviourTreeStatus.Running;
        }

        public override void EnterState()
        {
            /*if (!_currencyController.Currency.ChekStatrOneGame) // что-то что надо делать на уровне загрузки профайлера игрока такое ощущение.
            {
                Save.SaveJson(Stats, JsonName.PlayerStats);
                _inventoryEqupController.SavePlayerEquipment();
                _inventoryController.InventorySave();
            }*/

            SetStatPlayer();
            MaineSceneViewHolder.InitMainSceneState();
        }

        public void SetStatPlayer()
        {
            /*Debug.LogError("SetStatPlayer");
            Stats = Load.LoadJson<PlayerStats>(JsonName.PlayerStats);
            Save.SaveJson(Stats, JsonName.LevelStats);*/
        }
    }
}