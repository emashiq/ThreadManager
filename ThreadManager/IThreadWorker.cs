using System.Collections.Generic;

namespace ThreadManager
{
    
    public interface IThreadWorker
    {
        List<TMThread> GetAllThreads { get; }
        void RemoveThread(params string[] threadName);
        void AddThread(TMThread thread, string threadName="");
        void AddThread(List<TMThread> threads);
        void AddThread(Dictionary<string, TMThread> thread, bool CanPerformSameWorkMultipletime);
        void ExecuteByCriteria(ThreadOperationMode threadOperationMode, CustomThreadRunner customThreadRunner = null, params object[] parameters);
    }
}
