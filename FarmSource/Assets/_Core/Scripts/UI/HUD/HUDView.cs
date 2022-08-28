namespace Farm.UI.HUD
{
    public class HUDView : View
    {
        private PointerOverTracker _mouseOverTracker;

        public bool IsFocused { get; private set; }

        private void Awake()
        {
            _mouseOverTracker = GetComponentInChildren<PointerOverTracker>();
        }

        private void OnEnable()
        {
            RegisterEventHandlers();
        }

        private void OnDisable()
        {
            UnregisterEventHandlers();
        }

        private void RegisterEventHandlers()
        {
            _mouseOverTracker.PointerEnter += OnPointerEnterHandler;
            _mouseOverTracker.PointerExit += OnPointerExitHandler;
        }

        private void UnregisterEventHandlers()
        {

            _mouseOverTracker.PointerEnter -= OnPointerEnterHandler;
            _mouseOverTracker.PointerExit -= OnPointerExitHandler;
        }

        private void OnPointerEnterHandler()
        {
            IsFocused = true;
        }

        private void OnPointerExitHandler()
        {
            IsFocused = false;
        }
    }
}