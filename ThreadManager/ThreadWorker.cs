using System;
using System.Collections.Generic;
using System.Text;

namespace ThreadManager
{
    public static class Extension{
        public static void ForEach<TKey, TValue>(this Dictionary<TKey, TValue> dict, Action<TKey, TValue> action)
        {
            if (dict == null) throw new ArgumentNullException("dict");
            if (action == null) throw new ArgumentNullException("action");
            foreach (KeyValuePair<TKey, TValue> item in dict)
                action(item.Key, item.Value);
        }
        public static List<TValue> ToValueList<TKey, TValue>(this Dictionary<TKey, TValue> dict)
        {
            List<TValue> values = new List<TValue>();
            foreach (var item in dict)
                values.Add(item.Value);
            return values;
        }

        public static List<TValue> ArrayToList<TValue>(this TValue[] valueArray)
        {
            List<TValue> values = new List<TValue>();
            foreach (var item in valueArray)
                values.Add(item);
            return values;
        }
    }
    public class ThreadWorker : IThreadWorker
    {
        public Dictionary<string, TMThread> threads = new Dictionary<string, TMThread>();

        public List<TMThread> GetAllThreads => threads.ToValueList();

        public void AddThread(TMThread thread, string threadName) => threads.Add(threadName+Guid.NewGuid().ToString(), thread);
        public void AddThread(List<TMThread> threads) => threads.AddRange(threads);
        public void AddThread(Dictionary<string, TMThread> thread,bool CanPerformSameWorkMultipletime)
        {
            foreach (var item in thread)
                if (threads.ContainsKey(item.Key) && CanPerformSameWorkMultipletime)
                    threads.Add(item.Key+Guid.NewGuid().ToString(), item.Value);
                else threads.Add(item.Key, item.Value);
        }
        private void ThreadStart(TMThread thread) => thread.Start();
        private void ThreadJoin(TMThread thread) => thread.Join();

        private void JoinThreads() => threads.ForEach((x, y) => y.Join());
        private void StartThreads() => threads.ForEach((x, y) => y.Start());
        private void ThreadStartAndJoin(TMThread thread)
        {
            ThreadStart(thread);
            ThreadJoin(thread);
        }
        public void ExecuteByCriteria(ThreadOperationMode threadOperationMode, CustomThreadRunner customThreadRunner = null, params object[] parameters)
        {
            switch (threadOperationMode)
            {
                case ThreadOperationMode.StartAndJoin:
                    threads.ForEach((x,y) => ThreadStartAndJoin(y));
                    break;
                case ThreadOperationMode.AllStartThenJoin:
                    StartThreads();
                    JoinThreads();
                    break;
                case ThreadOperationMode.JoinOnMainThreadEnd:
                    threads.ForEach((x, y) => ThreadStart(y));
                    break;
                case ThreadOperationMode.CustomThreadSequence:
                    customThreadRunner(parameters);
                    break;
            }
        }

        public void RemoveThread(params string[] threadNames)
        {
            threadNames.ArrayToList<string>().ForEach(x =>
            {
                if (threads.ContainsKey(x))
                    threads.Remove(x);
            });
          
        }
    }
}