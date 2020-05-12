using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AppsGenerate.Services
{
    public static class ThreadsFactory
    {
        static ThreadsFactory()
        {
            _threads = new Dictionary<string, Process>();
        }
        
        private static Dictionary<string, Process> _threads;

        public static Process CreateNewThread(string key)
        {
            var thread = new Process();
            _threads.Add(key,thread);
            return thread;
        }

        public static void AbortThread(string key)
        {
            var thread = _threads[key];
            thread.StandardInput.Write("^C");
            _threads.Remove(key);
        }

        public static string[] GetThreadsInfo()
        {
            return _threads.Select(x => x.Key).ToArray();
        }

    }
}