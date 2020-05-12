using System;
using System.IO;
using AppsGenerate.CodeGenerate.Generators;
using AppsGenerate.CodeGenerate.Generators.Publication;
using AppsGenerate.CodeGenerate.Parse;
using AppsGenerate.Services;
using AppsGenerate.Structures;
using AppsGenerate.Structures.Impl;

namespace AppsGenerate
{
    class Program
    {
        static void Main(string[] args)
        {
            var cli = new Cli();
            
            cli.Start();
        }
    }
}
