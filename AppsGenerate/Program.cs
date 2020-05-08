using System;
using System.IO;
using AppsGenerate.CodeGenerate.Generators;
using AppsGenerate.CodeGenerate.Generators.Publication;
using AppsGenerate.CodeGenerate.Parse;
using AppsGenerate.Structures;
using AppsGenerate.Structures.Impl;

namespace AppsGenerate
{
    class Program
    {
        static void Main(string[] args)
        {
            
          using (var fs = File.Open("../../../ParserTest.txt", FileMode.Open))
          using (var reader = new StreamReader(fs))
          {
              var json = reader.ReadToEnd();
              var parser = new MetaParser();
              var projectMeta = parser.Parse(json);
              
              var builder = new Builder();
              builder.Build(projectMeta);
          }

            /*using (var fs = File.Open("ParserTest.txt", FileMode.Open))
            using (var reader = new StreamReader(fs))
            {
                var json = reader.ReadToEnd();
                var parser = new MetaParser();
                var projectMeta = parser.Parse(json);
                Console.WriteLine("Done");
            }*/


            /*var project1 = new ProjectStructure();
            project1.Name = "Clients";
            var class1 = new EntityStructure();
            var class2 = new EntityStructure();
            class1.Project = project1;
            class2.Project = project1;
            class2.Name = "Parent";
            class1.ParentStructure = class2;
            class1.Name = "User";
            class1.AccessModificator = AccessModType.Public;
            
            class1.AddProperty(new PropertyStructure
            {
                AccessModificator = AccessModType.Public,
                Name = "Name",
                Type = typeof(String)
            });
            
            class1.AddProperty(new PropertyStructure
            {
                AccessModificator = AccessModType.Public,
                Name = "Password",
                Type = typeof(String)
            });
            
            class1.AddProperty(new PropertyStructure
            {
                AccessModificator = AccessModType.Public,
                Name = "Id",
                Type = typeof(Int64)
            });
            
            var enum1 = new EnumStructure();
            enum1.Project = project1;
            enum1.Name = "NumType";
            enum1.AccessModificator = AccessModType.Public;
            enum1._consts.Add("Inn");
            enum1._consts.Add("Ogrn");
            enum1._consts.Add("Kpp");
            
            var gen = new StructureGenerator();
            gen.Generate(class1);
            gen.Generate(enum1);
            
            var viewModel = new ModelStructure(class1);
            
            var gen2 = new ModelGenerator();
            gen2.Generate(viewModel);*/

        }
    }
}
