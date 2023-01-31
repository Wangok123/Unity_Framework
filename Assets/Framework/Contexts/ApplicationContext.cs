using Framework.Execution;
using Framework.Services;

namespace Framework.Contexts
{
    public class ApplicationContext : Context
    {
        private IMainLoopExecutor mainLoopExecutor;

        public ApplicationContext() : this(null, null)
        {
        }

        public ApplicationContext(IServiceContainer container, IMainLoopExecutor mainLoopExecutor): base(container, null)
        {
            this.mainLoopExecutor = mainLoopExecutor;
            if (this.mainLoopExecutor == null)
                this.mainLoopExecutor = new MainLoopExecutor();
        }

        public virtual IMainLoopExecutor GetMainLoopExcutor()
        {
            return mainLoopExecutor;
        }
    }
}
