using System;
using System.Collections;
using System.Collections.Generic;
using Framework.Services;
using UnityEngine;

namespace Framework.Contexts
{
    public class Context : IDisposable
    {
        private static ApplicationContext context = null;
        private static Dictionary<string, Context> contexts = null;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnInitialize()
        {
            //For compatibility with the "Configurable Enter Play Mode" feature
#if UNITY_2019_3_OR_NEWER && UNITY_EDITOR
            try
            {
                if (context != null)
                    context.Dispose();

                if (contexts != null)
                {
                    foreach (var context in contexts.Values)
                        context.Dispose();
                    contexts.Clear();
                }
            }
            catch (Exception)
            {
            }
#endif
            context = new ApplicationContext();
            contexts = new Dictionary<string, Context>();
        }

        public static ApplicationContext GetApplicationContext()
        {
            return Context.context;
        }

        public static void SetApplicationContext(ApplicationContext context)
        {
            Context.context = context;
        }

        public static Context GetContext(string key)
        {
            Context context = null;
            contexts.TryGetValue(key, out context);
            return context;
        }

        public static T GetContext<T>(string key) where T : Context
        {
            return (T) GetContext(key);
        }

        public static void AddContext(string key, Context context)
        {
            contexts.Add(key, context);
        }

        public static void RemoveContext(string key)
        {
            contexts.Remove(key);
        }

        private bool innerContainer = false;
        private Context contextBase;
        private IServiceContainer container;
        private Dictionary<string, object> attributes;

        public Context() : this(null, null)
        {
        }

        public Context(IServiceContainer container, Context contextBase)
        {
            this.attributes = new Dictionary<string, object>();
            this.contextBase = contextBase;
            this.container = container;
            if (this.container == null)
            {
                this.innerContainer = true;
                this.container = new ServiceContainer();
            }
        }

        #region IDisposable Support

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (this.innerContainer && this.container != null)
                    {
                        IDisposable dis = this.container as IDisposable;
                        if (dis != null)
                            dis.Dispose();
                    }
                }

                disposed = true;
            }
        }

        ~Context()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
