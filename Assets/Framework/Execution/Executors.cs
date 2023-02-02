using System;
using System.Collections.Generic;
using System.Threading;
using GameCore.Runtime.Execution;
using Log;
using UnityEngine;

namespace GameCore.Framework.Execution
{
    public class Executors
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Executors));
        private static readonly object syncLock = new object();
        private static bool disposed = false;
        private static MainThreadExecutor executor;
        private static SynchronizationContext context;
#if NETFX_CORE || !NET_LEGACY
        private static int mainThreadId;
#else
        private static Thread mainThread;
#endif

#if UNITY_EDITOR
        private static Dictionary<int, Thread> threads = new Dictionary<int, Thread>();
#endif

        static void Destroy()
        {
            disposed = true;
#if UNITY_EDITOR
            lock (threads)
            {
                foreach (Thread thread in threads.Values)
                {
                    try
                    {
                        thread.Abort();
                    }
                    catch (Exception)
                    {
                    }
                }

                threads.Clear();
            }
#endif
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnRuntimeCreate()
        {
#if UNITY_2019_3_OR_NEWER && UNITY_EDITOR
            disposed = false;
            executor = null;
            context = null;
#endif
            Create();
        }
        
        public static void Create(bool dontDestroy = true, bool useFixedUpdate = false)
        {
            lock (syncLock)
            {
                try
                {
                    if (executor != null)
                        return;
#if NETFX_CORE || !NET_LEGACY
                    mainThreadId = Environment.CurrentManagedThreadId;
#else
                    Thread currentThread = Thread.CurrentThread;
                    if (currentThread.ManagedThreadId > 1 || currentThread.IsThreadPoolThread)
                        throw new Exception("Initialize the class on the main thread, please!");

                    mainThread = currentThread;
#endif
                    executor = CreateMainThreadExecutor(dontDestroy, useFixedUpdate);
                    context = SynchronizationContext.Current;
                }
                catch (Exception e)
                {
                    if (log.IsErrorEnabled)
                        log.ErrorFormat("Start Executors failure.Exception:{0}", e);
                }
            }
        }
        
        private static MainThreadExecutor CreateMainThreadExecutor(bool dontDestroy, bool useFixedUpdate)
        {
            GameObject go = new GameObject("MainThreadExecutor");
            var executor = go.AddComponent<MainThreadExecutor>();
            go.hideFlags = HideFlags.HideAndDontSave;
            if (dontDestroy)
                GameObject.DontDestroyOnLoad(go);

            executor.useFixedUpdate = useFixedUpdate;
            return executor;
        }
    }
}