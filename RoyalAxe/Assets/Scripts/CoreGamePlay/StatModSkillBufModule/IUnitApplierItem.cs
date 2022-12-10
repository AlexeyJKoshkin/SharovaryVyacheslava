namespace RoyalAxe.Units 
{
    public interface IUnitApplierItem
    {
        void ApplyTo(UnitsEntity owner);
        void RemoveFrom(UnitsEntity owner);
    }
}