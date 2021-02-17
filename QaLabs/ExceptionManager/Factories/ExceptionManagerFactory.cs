using ExceptionManager.Implementations;
using ExceptionManager.Interfaces;

namespace ExceptionManager.Factories
{
    class ExceptionManagerFactory
    {
        private IExceptionManager extensionManager = null;
        //private readonly List<Type> _criticalExceptionTypes;
        //private readonly IServerClient _serverClient;
        public void SetManager(IExceptionManager mgr)
        {
            //_criticalExceptionTypes = options?.CriticalExceptionTypes?.Select(Type.GetType).ToList();
            //_serverClient = serverClient;
            extensionManager = mgr;
        }
        public IExceptionManager IExceptionManager Create()
        {
            if (extensionManager != null)
                return extensionManager;
            return new ServerClient();
        }
    }
}
