using System;
using System.Collections;
using System.Collections.Generic;
using GameCore.Framework.Execution;
using Log;
using UnityEngine;

namespace GameCore.Runtime.Execution
{
    public class MainThreadExecutor : MonoBehaviour
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MainThreadExecutor));
        
        public bool useFixedUpdate = false;
        private List<object> pendingQueue = new List<object>();
        private List<object> stopingQueue = new List<object>();

        private List<object> runningQueue = new List<object>();
        private List<object> stopingTempQueue = new List<object>();
        
        void OnApplicationQuit()
        {
            this.StopAllCoroutines();
            Executors.Destroy();
            if (this.gameObject != null)
            {
                Destroy(this.gameObject);
            }
        }
        
        void Update()
        {
            if (useFixedUpdate)
                return;

            if (pendingQueue.Count <= 0 && stopingQueue.Count <= 0)
                return;

            this.DoStopingQueue();

            this.DoPendingQueue();
        }
        
        void FixedUpdate()
        {
            if (!useFixedUpdate)
                return;

            if (pendingQueue.Count <= 0 && stopingQueue.Count <= 0)
                return;

            this.DoStopingQueue();

            this.DoPendingQueue();
        }
        
        protected void DoStopingQueue()
        {
            lock (stopingQueue)
            {
                if (stopingQueue.Count <= 0)
                    return;

                stopingTempQueue.Clear();
                stopingTempQueue.AddRange(stopingQueue);
                stopingQueue.Clear();
            }

            for (int i = 0; i < stopingTempQueue.Count; i++)
            {
                try
                {
                    object task = stopingTempQueue[i];
                    if (task is IEnumerator)
                    {
                        this.StopCoroutine((IEnumerator)task);
                        continue;
                    }

                    if (task is Coroutine)
                    {
                        this.StopCoroutine((Coroutine)task);
                        continue;
                    }
                }
                catch (Exception e)
                {
                    if (log.IsWarnEnabled)
                        log.WarnFormat("Task stop exception! error:{0}", e);
                }
            }
            stopingTempQueue.Clear();
        }
        
        protected void DoPendingQueue()
        {
            lock (pendingQueue)
            {
                if (pendingQueue.Count <= 0)
                    return;

                runningQueue.Clear();
                runningQueue.AddRange(pendingQueue);
                pendingQueue.Clear();
            }

            float startTime = Time.realtimeSinceStartup;
            for (int i = 0; i < runningQueue.Count; i++)
            {
                try
                {
                    object task = runningQueue[i];
                    if (task is Action)
                    {
                        ((Action)task)();
                        continue;
                    }

                    if (task is IEnumerator)
                    {
                        this.StartCoroutine((IEnumerator)task);
                        continue;
                    }
                }
                catch (Exception e)
                {
                    if (log.IsWarnEnabled)
                        log.WarnFormat("Task execution exception! error:{0}", e);
                }
            }
            runningQueue.Clear();

            float time = Time.realtimeSinceStartup - startTime;
            if (time > 0.15f)
                log.DebugFormat("The running time of tasks in the main thread executor is too long.these tasks take {0} milliseconds.", (int)(time * 1000));
        }
    }
}