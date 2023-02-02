using GameCore.Framework.Asynchronous;

namespace GameCore.Framework.Views
{
    public interface IManageable : IWindow
    {
        IAsyncResult Activate(bool ignoreAnimation);
        IAsyncResult Passivate(bool ignoreAnimation);
        IAsyncResult DoShow(bool ignoreAnimation = false);
        IAsyncResult DoHide(bool ignoreAnimation = false);
        void DoDismiss();
    }
}