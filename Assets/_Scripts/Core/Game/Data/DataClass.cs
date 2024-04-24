namespace _Scripts.Core.Game.Data
{
    public abstract class DataClass
    {
        public abstract void Load(string json);
        public abstract string SaveToJson();
    }
}