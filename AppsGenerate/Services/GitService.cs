using System;
using System.Diagnostics;
using AppsGenerate.CodeGenerate.Parse.Meta;
using LibGit2Sharp;

namespace AppsGenerate.Services
{
    public class GitService
    {
        private readonly string _path;

        private readonly string _id;

        private readonly string _name;

        public GitService(string path)
        {
            _path = path;
            var splited = path.Split("/");
            _id = splited[splited.Length - 2];
            _name = splited[splited.Length - 1];
        }
        public string Init()
        {
            return Repository.Init(_path);
        }

        public void AddAll()
        {
            using (var repo = new Repository(_path))
                Commands.Stage(repo, "*");
        }

        public void Commit()
        {
            using (var repo = new Repository(_path))
            {
                // Create the committer's signature and commit
                Signature author = new Signature("Generator", "@", DateTime.Now);
                Signature committer = author;
                // Commit to the repository
                repo.Commit("Generator commit", author, committer);
            }
        }

        public void PushRemote(string link, string userName, string password)
        {
            
        }

        public void PushOwnRepository()
        {
            var process = new Process();
            process.StartInfo.FileName = "/bin/bash";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.Arguments = $"-c \" ../../../ShellScripts/./HubCreate.sh {_id} {_name} \"";
            process.StartInfo.UseShellExecute = false;
            process.Start();
            while(process.StandardOutput.ReadLine() != null){}
            
            process = new Process();
            process.StartInfo.FileName = "/bin/bash";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.Arguments = $"-c \" ../../../ShellScripts/./GitPush.sh {_id} {_name} \"";
            process.StartInfo.UseShellExecute = false;
            process.Start();
            while(process.StandardOutput.ReadLine() != null){}
        }
        
        
    }
}