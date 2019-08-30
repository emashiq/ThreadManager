using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreadManager
{
    public class TMThread
    {
        private Thread Thread { get; set; }
        private object ReturnObject { get; set; } = null;
        public TMThread(Thread thread) => Thread = thread;

        public object GetThreadResult => ReturnObject;
        public void Start() => Thread.Start();
        public void Join(int? miliseconds = null) => Thread.Join(miliseconds == null ? 0 : (int)miliseconds);
    }
}
