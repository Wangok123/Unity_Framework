using System;

namespace GameCore.Framework.Views
{
    public interface ITransition
    {
        bool IsDone { get; }

        object WaitForDone();
        
        ITransition DisableAnimation(bool disabled);
        
        ITransition AtLayer(int layer);
        
        ITransition Overlay(Func<IWindow, IWindow, ActionType> policy);
        
        ITransition OnStart(Action callback);
        
        ITransition OnStateChanged(Action<IWindow, WindowState> callback);
        
        ITransition OnFinish(Action callback);
    }
    
    public enum ActionType
    {
        None,
        Hide,
        Dismiss
    }
}