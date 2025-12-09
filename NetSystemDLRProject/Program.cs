using System.Dynamic;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

ScriptEngine scriptEngine = Python.CreateEngine();
scriptEngine.Execute("print('Hello world')");
scriptEngine.ExecuteFile("prog01.py");


int x = 23, y = 45;
ScriptScope scriptScope = scriptEngine.CreateScope();
scriptScope.SetVariable("x", x);
scriptScope.SetVariable("y", y);

scriptEngine.ExecuteFile("prog02.py", scriptScope);
dynamic z = scriptScope.GetVariable("z");
Console.WriteLine($"From NET: z = {z}");

float a = 2f;
int exp = 5;

dynamic binPower = scriptScope.GetVariable("binPower");
dynamic result = binPower(a, exp);
Console.WriteLine(result);




