using System;
using GameCore.Framework.Execution;
using Log;
using UnityEngine.UI;

namespace GameCore.Framework.Views.Transitions
{
    public class Transition: ITransition
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Selectable.Transition));

        private IManageable window;
        private bool done = false;
        
        public bool IsDone { get; }
        public object WaitForDone()
        {
            return Executors.WaitWhile(() => !IsDone);
        }

        public ITransition DisableAnimation(bool disabled)
        {
            throw new NotImplementedException();
        }

        public ITransition AtLayer(int layer)
        {
            throw new NotImplementedException();
        }

        public ITransition Overlay(Func<IWindow, IWindow, ActionType> policy)
        {
            throw new NotImplementedException();
        }

        public ITransition OnStart(Action callback)
        {
            throw new NotImplementedException();
        }

        public ITransition OnStateChanged(Action<IWindow, WindowState> callback)
        {
            throw new NotImplementedException();
        }

        public ITransition OnFinish(Action callback)
        {
            throw new NotImplementedException();
        }
    }
}