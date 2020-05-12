using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using AppsGenerate.CodeGenerate.Generators.Publication;
using AppsGenerate.CodeGenerate.Parse;
using AppsGenerate.CodeGenerate.Parse.Meta;
using MoreLinq;

namespace AppsGenerate.Services
{
    public class Cli
    {
        public Cli()
        {
            Console.WriteLine(@"Добро пожаловать в AppsGenerate!
Справка: 
generate project {id проекта} {порт} - сгенерировать проект из конфигурации метафайла
run project {id проекта} {порт} - запустить проект из конфигурации метафайла
list generated - вывести список сгенерированных в данный момент проектов
list running - вывести список запущенных в данный момент проектов
");
        }

        public void Start()
        {
            string cmd = String.Empty;
            while (cmd != "exit")
            {
                Console.WriteLine("Введите команду");
                cmd = Console.ReadLine();
                cmd = cmd.Trim();
                
                if (cmd.Contains("generate project"))
                {
                    var id = cmd.Split(" ")[2];
                    var port = cmd.Split(" ")[3];
                    using (var fs = File.Open($"../../../../../Meta/{id}.txt", FileMode.Open))
                        using (var reader = new StreamReader(fs))
                        {
                            var json = reader.ReadToEnd();
                            var parser = new MetaParser();
                            var projectMeta = parser.Parse(json);
                            var builder = new Builder();
                            builder.Build(projectMeta, port);
                        }
                }
                
                else if (cmd.Contains("run project"))
                {
                    var id = cmd.Split(" ")[2];
                    var port = cmd.Split(" ")[3];

                    ProjectMeta projectMeta = null;
                    using (var fs = File.Open($"../../../../../Meta/{id}.txt", FileMode.Open))
                    using (var reader = new StreamReader(fs))
                    {
                        var json = reader.ReadToEnd();
                        var parser = new MetaParser();
                        projectMeta = parser.Parse(json);
                        
                        var thread = new Thread(()=>
                        {
                            var process = ThreadsFactory.CreateNewThread(projectMeta.Id.ToString());
                            process.StartInfo.FileName = "/bin/bash";
                            process.StartInfo.RedirectStandardOutput = true;
                            process.StartInfo.Arguments = $"-c \" ../../../ShellScripts/./DotnetRun.sh {projectMeta.Id} {projectMeta.Name} {port}\"";
                            process.StartInfo.UseShellExecute = false;
                            process.Start();
                            Console.WriteLine($"Проект {projectMeta.Id}/{projectMeta.Name} запущен на порте:" + port);
                            process.WaitForExit();
                        });
            
                        thread.Start();
                    }
                }
                
                else if (cmd.Contains("list running"))
                {
                    RuntimeInfo.GetRunningProjects().ForEach(x => Console.WriteLine($"Проект {x} запущен"));
                }
                
                else if (cmd.Contains("list generated"))
                {
                    RuntimeInfo.GetGeneratedProjects().ForEach(x => Console.WriteLine($"Проект {x} сгенерирован"));
                }
                else
                {
                    Console.WriteLine("Команда введена неверно");
                }
            }
        }
    }
}