using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AppsGenerate.CodeGenerate.Parse.Meta;
using AppsGenerate.Structures;
using AppsGenerate.Structures.Impl;
using GitHub;

namespace AppsGenerate.CodeGenerate.Generators.Publication
{
    public class Builder
    {
        public void Build(ProjectMeta meta)
        {
            Package(meta);
            //CreateProject();
            //BuildProject();
            //GitProject();
        }

        private void GitProject()
        {
            throw new System.NotImplementedException();            
        }

        private void BuildProject()
        {
            throw new System.NotImplementedException();
        }

        private void CreateProject()
        {
            throw new System.NotImplementedException();
        }

        private void Package(ProjectMeta meta)
        {
            var path = $"../../Projects/{meta.Id}";
            
            PackageFromTemplates(meta, path);

            var projectStructure = new ProjectStructure
            {
                Name = meta.Name,
            };
            
            var structureGenerator = new StructureGenerator();
            var mapGenerator = new MapGenerator();
            var controllerGenerator = new ControllerGenerator();
            var dbMigrationGenerator = new DbMigrationGenerator();
            var storeGenerator = new StoreGenerator();
            var modelGenerator = new ModelGenerator();
            var gridGenerator = new GridGenerator();
            var formWindowGenerator = new FormWindowGenerator();
            var clientControllerGenerator = new ClientControllerGenerator();

            foreach (var metaEntity in meta.Entities)
            {
                var entity = new EntityStructure
                {
                    AccessModificator = AccessModType.Public,
                    DisplayName = metaEntity.DisplayName,
                    Name = metaEntity.Name,
                    //todo:ParentStructure
                    Project = projectStructure,
                    StrucutureType = StructureType.Class
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

                var modelStructure = new ModelStructure(entity);
                var gridStructure = new GridStructure(entity);
                var formStructure = new FormStructure(entity);
                
                structureGenerator.Generate(entity,path);
                mapGenerator.Generate(entity,path);
                controllerGenerator.Generate(entity,path);
                dbMigrationGenerator.Generate(entity,path);
                storeGenerator.Generate(modelStructure,path);
                modelGenerator.Generate(modelStructure,path);
                gridGenerator.Generate(gridStructure,path);
                formWindowGenerator.Generate(formStructure,path);
                clientControllerGenerator.Generate(modelStructure,path);
            }


        }

        private void PackageFromTemplates(ProjectMeta meta, string path)
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

            
            using(var fs = new FileStream("Templates/Program.txt", FileMode.Open))
                using (var reader = new StreamReader(fs))
                {
                    var text = reader.ReadToEnd();
                    text = Regex.Replace(text, "'Name'", meta.Name);
                    
                    using(var fs2 = new FileStream(path + "/Program.cs", FileMode.OpenOrCreate))
                    using (var writer = new StreamWriter(fs2))
                        writer.Write(text);
                }
            
            //todo:startup scope
            using(var fs = new FileStream("Templates/Startup.txt", FileMode.Open))
                using (var reader = new StreamReader(fs))
                {
                    var text = reader.ReadToEnd();
                    text = Regex.Replace(text, "'Name'", meta.Name);
                        
                    using(var fs2 = new FileStream(path + "/Startup.cs", FileMode.OpenOrCreate))
                    using (var writer = new StreamWriter(fs2))
                        writer.Write(text);
                }
            
            using(var fs = new FileStream("Templates/BaseController.txt", FileMode.Open))
                using (var reader = new StreamReader(fs))
                {
                    var text = reader.ReadToEnd();
                    text = Regex.Replace(text, "'Name'", meta.Name);
                            
                    using(var fs2 = new FileStream(path + "/Controllers/BaseController.cs", FileMode.OpenOrCreate))
                    using (var writer = new StreamWriter(fs2))
                        writer.Write(text);
                }
            
            using(var fs = new FileStream("Templates/ConnectionsFactory.txt", FileMode.Open))
                using (var reader = new StreamReader(fs))
                {
                    var text = reader.ReadToEnd();
                    text = Regex.Replace(text, "'Name'", meta.Name);
                    text = Regex.Replace(text, "!", meta.DbConnectionString);
                                
                    using(var fs2 = new FileStream(path + "/Data/ConnectionsFactory.cs", FileMode.OpenOrCreate))
                    using (var writer = new StreamWriter(fs2))
                        writer.Write(text);
                }
            
            using(var fs = new FileStream("Templates/DataAccessObject.txt", FileMode.Open))
            using (var reader = new StreamReader(fs))
            {
                var text = reader.ReadToEnd();
                text = Regex.Replace(text, "'Name'", meta.Name);
                                
                using(var fs2 = new FileStream(path + "/Data/DataAccessObject.cs", FileMode.OpenOrCreate))
                using (var writer = new StreamWriter(fs2))
                    writer.Write(text);
            }
            
            using(var fs = new FileStream("Templates/index.txt", FileMode.Open))
            using (var reader = new StreamReader(fs))
            {
                var text = reader.ReadToEnd();
                text = Regex.Replace(text, "'DisplayName'", meta.DisplayName);
                                
                using(var fs2 = new FileStream(path + "/wwwroot/index.html", FileMode.OpenOrCreate))
                using (var writer = new StreamWriter(fs2))
                    writer.Write(text);
            }
            
            using(var fs = new FileStream("Templates/app.txt", FileMode.Open))
            using (var reader = new StreamReader(fs))
            {
                var text = reader.ReadToEnd();
                text = Regex.Replace(text, "'Name'", meta.Name);
                text = Regex.Replace(text, "'DisplayName'", meta.DisplayName);

                var controllers = String.Join(",", meta.Entities.Select(x => $"'{x.Name}'"));
                text = Regex.Replace(text, "'Controllers'", controllers);
                
                var items = String.Join(",", meta.Entities.Select(x => $@"title: '{x.DisplayName}',
                                                                                    items: [{{
                                                                                    xtype: '{x.Name.ToLower()}-list'
                                                                                    }}]"));
                text = Regex.Replace(text, "'Items'", items);
                
                using(var fs2 = new FileStream(path + "/wwwroot/ClientApp/app.js", FileMode.OpenOrCreate))
                using (var writer = new StreamWriter(fs2))
                    writer.Write(text);
            }
            
            
            
        }
    }
}