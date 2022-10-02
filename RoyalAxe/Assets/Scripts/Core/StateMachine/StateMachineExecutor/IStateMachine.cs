namespace Core
{
    /// <summary>
    ///     Базовая концепция стейт машина - черный ящик, который работает сам по себе
    /// </summary>
    public interface IStateMachine
    {
        /// <summary>
        ///     Начало работы
        /// </summary>
        void Start();

        void Stop();
    }
}