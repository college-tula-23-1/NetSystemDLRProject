using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSystemDLRProject
{
    public class Examples
    {
        public static void DynamicWelcomeExample()
        {
            dynamic dyn = 3;
            object obj = 3;
            //dyn = "Hello world";
            //dyn = new User("Mikky", 25);

            //Console.WriteLine(obj += 1);
            Console.WriteLine(dyn += 1);

            Employee bob = new("Bobby", 26);
            Console.WriteLine(bob.GetSalary("123", "string"));
            Console.WriteLine(bob.GetSalary(1234, "int"));
            Console.WriteLine(bob.GetSalary("12", ""));

        }

        public static void ExpandoObjectExample()
        {
            dynamic employee = new ExpandoObject();

            employee.name = "Tommy";
            employee.age = 26;
            employee.scills = new List<string>() { "C++", "C#", "Java" };

            Console.WriteLine($"Name: {employee.name}, Age: {employee.age}");
            foreach (var s in employee.scills)
                Console.WriteLine(s);

            employee.incrementAge = (Action<int>)(v => employee.age += v);

            employee.name = "Sammy";
            employee.incrementAge(5);
            Console.WriteLine($"Name: {employee.name}, Age: {employee.age}");

        }

        public static void DynamicObjectExample()
        {
            dynamic bob = new EmployeeObject();
            bob.Name = "Bobby";
            bob.Age = 30;

            Func<int, int> incrAge = (int a) =>
            {
                bob.Age += a;
                return bob.Age;
            };

            Action toConsole = () => Console.WriteLine($"Name: {bob.Name}, Age: {bob.Age}");

            bob.IncAge = incrAge;
            bob.ToConsole = toConsole;

            bob.ToConsole();
            bob.IncAge(5);
            bob.ToConsole();
        }
    }

    record class User(string Name, int Age);

    class Employee
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Employee(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public dynamic GetSalary(dynamic salary, string format)
        {
            if (format == "string")
                return $"{salary} rub.";
            else if (format == "int") return salary;
            else return 0.0;
        }
    }

    class EmployeeObject : DynamicObject
    {
        Dictionary<string, object> props = new Dictionary<string, object>();

        public override bool TrySetMember(SetMemberBinder binder, object? value)
        {
            if (value is not null)
            {
                props[binder.Name] = value;
                return true;
            }
            return false;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            result = null;
            if (props.ContainsKey(binder.Name))
            {
                result = props[binder.Name];
                return true;
            }
            return false;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object?[]? args, out object? result)
        {
            result = null;
            if (args is null || args.Length == 0)
            {
                dynamic action = props[binder.Name];
                action();
                return true;
            }

            if (args.Length > 0 && args[0] is int number)
            {
                dynamic func = props[binder.Name];
                result = func(number);
                return true;
            }

            return false;
        }
    }
}
