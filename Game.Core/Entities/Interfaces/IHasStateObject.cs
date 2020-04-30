namespace Game.Core.Entities.Interfaces
{
    public interface IHasStateObject : IHasID
    {
        IHasID stateObject { get; set; }
    }
}
