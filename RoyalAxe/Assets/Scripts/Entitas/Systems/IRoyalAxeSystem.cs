using Entitas;

namespace RoyalAxe.EntitasSystems
{
    public interface IPauseAbleSystem : IExecuteSystem { }

    //системы необходмые для работы геймплейной сцены
    public interface IGamePlaySceneSystem : ISystem { }

    //Системы которые биндятся в ядре игры и работают с запуска и до конца игры
    //  public interface IRoyalAxeSystem : ISystem { }
}