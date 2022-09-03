namespace Farm.Interactable
{
    public interface IInteractable
    {
        public IInteractionLogic InteractionSource { get; }
        public InteractionSettings InteractionSettings { get; }
    }
}
