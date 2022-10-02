namespace RoyalAxe
{
    public interface IRoyalAxePool<T> { 
        T Get();
        void Return(T chunk);
    }
}