using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AppsGenerate.Services
{
    public static class RuntimeInfo
    { 
        static RuntimeInfo()
        {
            _runningProjects = new List<string>();
            _generatedProjects = new List<string>();
        }
        private static List<string> _runningProjects;
        private static List<string> _generatedProjects;

        public static string[] GetRunningProjects()
        {
            return ThreadsFactory.GetThreadsInfo();
        }
        
        public static string[] GetGeneratedProjects()
        {
            var path = "../../../../../Projects/";
            return Directory.EnumerateDirectories(path).Select(x => x.Split("/").Last()).ToArray();
        }
    }
}