using System;
using System.Collections.Generic;
using GameCore.Framework.Views.Transitions;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.Framework.Views
{
    [DisallowMultipleComponent]
    public class WindowManager : MonoBehaviour, IWindowManager
    {
        private bool activated = true;
        public bool Activated
        {
            get=> activated;
            set
            {
                if(activated == value)
                    return;
                activated = value;
            }
        }
        
        private List<IWindow> windows = new List<IWindow>();

        public IWindow Current
        {
            get
            {
                // if()
                return default;
            }
        }

        public int Count { get; }
        public IEnumerator<IWindow> Visibles()
        {
            throw new System.NotImplementedException();
        }

        public IWindow Get(int index)
        {
            throw new System.NotImplementedException();
        }

        public void Add(IWindow window)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(IWindow window)
        {
            throw new System.NotImplementedException();
        }

        public IWindow RemoveAt(int index)
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(IWindow window)
        {
            throw new System.NotImplementedException();
        }

        public int IndexOf(IWindow window)
        {
            throw new System.NotImplementedException();
        }

        public List<IWindow> Find(bool visible)
        {
            throw new System.NotImplementedException();
        }

        public T Find<T>() where T : IWindow
        {
            throw new System.NotImplementedException();
        }

        public T Find<T>(string name) where T : IWindow
        {
            throw new System.NotImplementedException();
        }

        public List<T> FindAll<T>() where T : IWindow
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public ITransition Show(IWindow window)
        {
            throw new System.NotImplementedException();
        }

        public ITransition Hide(IWindow window)
        {
            throw new System.NotImplementedException();
        }

        public ITransition Dismiss(IWindow window)
        {
            throw new System.NotImplementedException();
        }

        class BlockingCoroutineTransitionExecutor
        {
            private Asynchronous.IAsyncResult taskResult;
            private bool running = false;
            private List<Transition> _transitions = new List<Transition>();
            public bool IsRunning
            {
                get => running;
            }
            
            public int Count
            {
                get => _transitions.Count;
            }
            
            public BlockingCoroutineTransitionExecutor()
            {
            }

            public void Execute(Transition transition)
            {
                try
                {
                    if(transition is )
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}