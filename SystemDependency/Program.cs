using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDependency
{
    class Program
    {
        static void Main(string[] args)
        {
            var programManager = new ProgramManager();
            var input = Console.ReadLine();
            input = input.ToUpper();
            while (!input.Trim().ToLower().StartsWith("end "))
            {
                string[] inp = input.Split(' ');
                
                switch (inp[0].Trim())
                {
                    case "DEPEND":
                        programManager.Depend(inp);
                        break;
                    case "INSTALL":
                        programManager.Install(inp[1]);
                        break;
                    case "REMOVE":
                        programManager.Remove(inp[1]);
                        break;
                    case "LIST":
                        programManager.Print();
                        break;
                    default:
                        Console.WriteLine("Invalid Input Please retry");
                        break;

                }
                input = Console.ReadLine();
                input = input.ToUpper();
            }

        }
    }
}
