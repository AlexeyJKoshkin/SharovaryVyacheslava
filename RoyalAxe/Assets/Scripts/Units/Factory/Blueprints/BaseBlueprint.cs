namespace RoyalAxe.GameEntitas {
    public abstract class BaseBlueprint
    {
        public string Id;
        public int Level;

        public BaseBlueprint()
        {
            
        }

        protected BaseBlueprint(string id, int level)
        {
            Id    = id;
            Level = level;
        }
    }
}