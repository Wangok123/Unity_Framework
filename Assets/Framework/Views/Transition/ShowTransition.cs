namespace GameCore.Framework.Views.Transitions
{
    public class ShowTransition : Transition
    {
        private IWindowManager _manager;

        public ShowTransition(IWindowManager manager, IManageable window) : base(window)
        {
            _manager = manager;
        }
    }
}