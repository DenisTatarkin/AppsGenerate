using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using AppsGenerate.CodeGenerate.Parse.Meta;
using AppsGenerate.Services;
using AppsGenerate.Structures;
using AppsGenerate.Structures.Impl;

namespace AppsGenerate.CodeGenerate.Generators.Publication
{
    public class Builder
    {
        public void Build(ProjectMeta meta, string port)
        {
            var path = $"../../../../../Projects/{meta.Id}/{meta.Name}";
            
            CreateProject(meta,path);
            Package(meta, path);
            Console.WriteLine($"Сгенерированы файлы для проекта {meta.Id}/{meta.Name}");
            BuildProject(meta, port);
            GitProject(meta, path);
            Console.WriteLine($"Сгенерированый проект {meta.Id}/{meta.Name} опубликован системой git на удаленном репозитории");
        }

        private void GitProject(ProjectMeta meta, string path)
        {
            var gitService = new GitService(path);
            gitService.Init();
            gitService.AddAll();
            gitService.Commit();
            gitService.PushOwnRepository();
        }

        private void BuildProject(ProjectMeta meta, string port)
        {
            var thread = new Thread(()=>
            {
                var process = ThreadsFactory.CreateNewThread(meta.Id.ToString());
                process.StartInfo.FileName = "/bin/bash";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.Arguments = $"-c \" ../../../ShellScripts/./DotnetRun.sh {meta.Id} {meta.Name} {port}\"";
                process.StartInfo.UseShellExecute = false;
                process.Start();
                Console.WriteLine($"Проект {meta.Id}/{meta.Name} запущен на порте:" + port);
                process.WaitForExit();
            });
            
            thread.Start();
        }

        private void CreateProject(ProjectMeta meta, string path)
        {
            Directory.CreateDirectory($"{path}");
            Directory.CreateDirectory($"{path}/Controllers");
            Directory.CreateDirectory($"{path}/Data");
            Directory.CreateDirectory($"{path}/Migrations");
            Directory.CreateDirectory($"{path}/Models");
            Directory.CreateDirectory($"{path}/wwwroot/ClientApp");
            Directory.CreateDirectory($"{path}/wwwroot/ClientApp/app/model");
            Directory.CreateDirectory($"{path}/wwwroot/ClientApp/app/store");
            Directory.CreateDirectory($"{path}/wwwroot/ClientApp/app/view");
            Directory.CreateDirectory($"{path}/wwwroot/ClientApp/app/controller");
            
            var process = new Process();
            process.StartInfo.FileName = "/bin/bash";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.Arguments = $"-c \" ../../../ShellScripts/./DotnetNew.sh {meta.Id} {meta.Name} \"";
            process.StartInfo.UseShellExecute = false;
            process.Start();
            while(process.StandardOutput.ReadLine() != null){}
            
            process = new Process();
            process.StartInfo.FileName = "/bin/bash";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.Arguments = $"-c \" ../../../ShellScripts/./CopyExtJsFiles.sh {meta.Id} {meta.Name} \"";
            process.StartInfo.UseShellExecute = false;
            process.Start();
            while(process.StandardOutput.ReadLine() != null){}
        }

        private void Package(ProjectMeta meta, string path)
        {
            PackageFromTemplates(meta, path);

            var projectStructure = new ProjectStructure
            {
                Name = meta.Name,
            };
            
            var structureGenerator = new StructureGenerator();
            var mapGenerator = new MapGenerator();
            var daoGenerrator = new DaoGenerator();
            var controllerGenerator = new ControllerGenerator();
            //var dbMigrationGenerator = new DbMigrationGenerator();
            var storeGenerator = new StoreGenerator();
            var modelGenerator = new ModelGenerator();
            var gridGenerator = new GridGenerator();
            var formWindowGenerator = new FormWindowGenerator();
            var clientControllerGenerator = new ClientControllerGenerator();
            var chooseFormWindowGenerator = new ChooseFormWindowGenerator();

            foreach (var metaEntity in meta.Entities)
            {
                var entity = new EntityStructure
                {
                    AccessModificator = AccessModType.Public,
                    DisplayName = metaEntity.DisplayName,
                    Name = metaEntity.Name,
                    //todo:ParentStructure
                    Project = projectStructure,
                    StrucutureType = StructureType.Class,
                    IsChoosed = metaEntity.IsChoosed,
                    ShownProperty = metaEntity.ShownProperty
                };

                foreach (var metaProperty in metaEntity.Properties)
                    entity.AddProperty(new PropertyStructure
                    {
                        AccessModificator = AccessModType.Public,
                        DisplayName = metaProperty.DisplayName,
                        Name = metaProperty.Name,
                        Project = projectStructure,
                        Type = Type.GetType("System." + metaProperty.Type)
                    });

                foreach (var linkedEntityMeta in metaEntity.LinkedEntities)
                {
                    entity.AddLinkedEntity(new LinkedEntityStructure
                    {
                        Name = linkedEntityMeta.Name,
                        DisplayName = linkedEntityMeta.DisplayName,
                        ShownProperty = linkedEntityMeta.ShownProperty
                    });
                }

                var modelStructure = new ModelStructure(entity);
                var gridStructure = new GridStructure(entity);
                var formStructure = new FormStructure(entity);
                
                structureGenerator.Generate(entity,path);
                mapGenerator.Generate(entity,path);
                daoGenerrator.Generate(entity, path);
                controllerGenerator.Generate(entity,path);
                //dbMigrationGenerator.Generate(entity,path);
                storeGenerator.Generate(modelStructure,path);
                modelGenerator.Generate(modelStructure,path);
                gridGenerator.Generate(gridStructure,path);
                formWindowGenerator.Generate(formStructure,path);
                clientControllerGenerator.Generate(modelStructure,path);
                if (entity.IsChoosed)
                    chooseFormWindowGenerator.Generate(modelStructure, path);
            }


        }

        private void PackageFromTemplates(ProjectMeta meta, string path)
        {
            File.Delete(path + "/Startup.cs");
            File.Delete(path + $"/{meta.Name}.csproj");
            /*
            using(var fs = new FileStream("../../../Templates/Program.txt", FileMode.Open))
                using (var reader = new StreamReader(fs))
                {
                    var text = reader.ReadToEnd();
                    text = Regex.Replace(text, "'Name'", meta.Name);
                    
                    using(var fs2 = new FileStream(path + "/Program.cs", FileMode.OpenOrCreate))
                    using (var writer = new StreamWriter(fs2))
                        writer.Write(text);
                }*/
            
            
            using(var fs = new FileStream("../../../Templates/Startup.txt", FileMode.Open))
                using (var reader = new StreamReader(fs))
                {
                    var text = reader.ReadToEnd();
                    text = Regex.Replace(text, "'Name'", meta.Name);
                    var scoped = new StringBuilder();
                    foreach (var entity in meta.Entities)
                    {
                        scoped.AppendLine(
                            $"services.AddScoped(typeof(IDataAccessObject<{entity.Name}>), typeof({entity.Name}DAO));");
                    }

                    text = Regex.Replace(text, "'Scope'", scoped.ToString());
                    using(var fs2 = new FileStream(path + "/Startup.cs", FileMode.OpenOrCreate))
                    using (var writer = new StreamWriter(fs2))
                        writer.Write(text);
                }
            
            using(var fs = new FileStream("../../../Templates/BaseController.txt", FileMode.Open))
                using (var reader = new StreamReader(fs))
                {
                    var text = reader.ReadToEnd();
                    text = Regex.Replace(text, "'Name'", meta.Name);
                            
                    using(var fs2 = new FileStream(path + "/Controllers/BaseController.cs", FileMode.OpenOrCreate))
                    using (var writer = new StreamWriter(fs2))
                        writer.Write(text);
                }
            
           /* using(var fs = new FileStream("../../../Templates/ConnectionsFactory.txt", FileMode.Open))
                using (var reader = new StreamReader(fs))
                {
                    var text = reader.ReadToEnd();
                    text = Regex.Replace(text, "'Name'", meta.Name);
                    text = Regex.Replace(text, "!", meta.DbConnectionString);
                                
                    using(var fs2 = new FileStream(path + "/Data/ConnectionsFactory.cs", FileMode.OpenOrCreate))
                    using (var writer = new StreamWriter(fs2))
                        writer.Write(text);
                }*/
            
            using(var fs = new FileStream("../../../Templates/DataAccessObject.txt", FileMode.Open))
            using (var reader = new StreamReader(fs))
            {
                var text = reader.ReadToEnd();
                text = Regex.Replace(text, "'Name'", meta.Name);
                                
                using(var fs2 = new FileStream(path + "/Data/DataAccessObject.cs", FileMode.OpenOrCreate))
                using (var writer = new StreamWriter(fs2))
                    writer.Write(text);
            }
            
            using(var fs = new FileStream("../../../Templates/index.txt", FileMode.Open))
            using (var reader = new StreamReader(fs))
            {
                var text = reader.ReadToEnd();
                text = Regex.Replace(text, "'DisplayName'", meta.DisplayName);
                                
                using(var fs2 = new FileStream(path + "/wwwroot/index.html", FileMode.OpenOrCreate))
                using (var writer = new StreamWriter(fs2))
                    writer.Write(text);
            }
            
            using(var fs = new FileStream("../../../Templates/app.txt", FileMode.Open))
            using (var reader = new StreamReader(fs))
            {
                var text = reader.ReadToEnd();
                text = Regex.Replace(text, "'Name'", meta.Name);
                text = Regex.Replace(text, "'DisplayName'", meta.DisplayName);

                var controllers = String.Join(",", meta.Entities.Select(x => $"'{x.Name}Controller'"));
                text = Regex.Replace(text, "'Controllers'", controllers);
                
                var items = String.Join(",", meta.Entities.Select(x => $@"{{
                    title: '{x.DisplayName}',
                                    items: [{{
                    xtype: '{x.Name.ToLower()}-list'
                }}]
                }}"));
                text = Regex.Replace(text, "'Items'", items);
                
                using(var fs2 = new FileStream(path + "/wwwroot/ClientApp/app/app.js", FileMode.OpenOrCreate))
                using (var writer = new StreamWriter(fs2))
                    writer.Write(text);
            }
            
            using(var fs = new FileStream("../../../Templates/IHaveId.txt", FileMode.Open))
            using (var reader = new StreamReader(fs))
            {
                var text = reader.ReadToEnd();
                text = Regex.Replace(text, "'Name'", meta.Name);
                                
                using(var fs2 = new FileStream(path + "/Models/IHaveId.cs", FileMode.OpenOrCreate))
                using (var writer = new StreamWriter(fs2))
                    writer.Write(text);
            }
            
            using(var fs = new FileStream("../../../Templates/IDataAccessObject.txt", FileMode.Open))
            using (var reader = new StreamReader(fs))
            {
                var text = reader.ReadToEnd();
                text = Regex.Replace(text, "'Name'", meta.Name);
                                
                using(var fs2 = new FileStream(path + "/Data/IDataAccessObject.cs", FileMode.OpenOrCreate))
                using (var writer = new StreamWriter(fs2))
                    writer.Write(text);
            }
            
            using(var fs = new FileStream("../../../Templates/NHibernateHelper.txt", FileMode.Open))
            using (var reader = new StreamReader(fs))
            {
                var text = reader.ReadToEnd();
                text = Regex.Replace(text, "'Name'", meta.Name);
                text = Regex.Replace(text, "'ConnectionString'", meta.DbConnectionString);
                                
                using(var fs2 = new FileStream(path + "/Data/NHibernateHelper.cs", FileMode.OpenOrCreate))
                using (var writer = new StreamWriter(fs2))
                    writer.Write(text);
            }
            
            using(var fs = new FileStream("../../../Templates/csproj.txt", FileMode.Open))
            using (var reader = new StreamReader(fs))
            {
                var text = reader.ReadToEnd();
                using(var fs2 = new FileStream(path + $"/{meta.Name}.csproj", FileMode.OpenOrCreate))
                using (var writer = new StreamWriter(fs2))
                    writer.Write(text);
            }
            
            using(var fs = new FileStream("../../../Templates/IJson.txt", FileMode.Open))
            using (var reader = new StreamReader(fs))
            {
                var text = reader.ReadToEnd();
                text = Regex.Replace(text, "'Name'", meta.Name);
                using(var fs2 = new FileStream(path + $"/Models/IJson.cs", FileMode.OpenOrCreate))
                using (var writer = new StreamWriter(fs2))
                    writer.Write(text);
            }
            
        }
    }
}