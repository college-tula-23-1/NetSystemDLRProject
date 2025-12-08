using System.Dynamic;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

ScriptEngine scriptEngine = Python.CreateEngine();
scriptEngine.Execute("print('Hello world')");
scriptEngine.ExecuteFile("prog01.py");




