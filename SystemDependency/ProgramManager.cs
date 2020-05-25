using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDependency
{
    public class ProgramManager
    {
        DependencyManager dependencyManager;
        List<string> installedProducts;
        public ProgramManager()
        {
            dependencyManager = new DependencyManager();
            installedProducts = new List<string>();
        }
        

        public bool Install(string component)
        {
            if (component.Trim() == string.Empty)
                return false;
            component = component.ToUpper();

            if (installedProducts.Contains(component))
            {
                PrintLine($"{component} is already installed.");
                return false;
            }
            
            InstallProduct(component);
         
              

            return true;

        }

        private void InstallProduct(string component)
        {
           
           

            
            var dep = dependencyManager.Get(component);
            //IF the depdency was not added adding it to the list
            if (dep == null)
            {
                dep = dependencyManager.Add(component);

            }
            InstallDependency(dep);

            installedProducts.Add(component);
            PrintLine($"Installing {component}");
        }

        internal void PrintLine(string s)
        {
             Console.WriteLine("   "+s);
        }
        internal void Print()
        {
            installedProducts.ForEach(a => PrintLine(a));
        }

        private void InstallDependency(Dependency dep)
        {
            foreach (var dependecy in dep.Dependencies)
            {
                if (!installedProducts.Contains(dependecy.Name))
                {

                    InstallProduct(dependecy.Name);
                }
                //if(dependecy.Dependencies.Count>0)
                //{
                //    InstallDependency(dependecy);
                //}
            }
        }

        public bool Remove(string program,bool print = true)
        {
            if (program.Trim() == string.Empty)
                return false;
            program = program.ToUpper();

            if (!installedProducts.Contains(program))
            {
                PrintLine($"{program} is not installed");
                return false;
            }

            foreach (var item in installedProducts)
            {
                if (item == program)
                    continue;
                if (dependencyManager.Get(item).HasDependency(program))
                {
                    if(print)
                        PrintLine($"{program} is still needed.");
                    return false;
                }
            }

            //our product does not have any dependency
            PrintLine($"Removing {program}");
            installedProducts.Remove(program);

            var curr = dependencyManager.Get(program);
            if (curr == null)
            {
                return true;
            }

            foreach (var item in curr.Dependencies)
            {
                Remove(item.Name,false);
            }
            return true;    


        }

       

        private void RemoveDependencies(Dependency dep)
        {
            if (dep.Dependencies.Count == 0)
                return ;
            var dependencies = dep.Dependencies.OrderByDescending(dp => dp.DepenedencyCount());

        }

        public bool Depend(string[] dependencies)
        {
            var input = dependencies.ToList().Where(inp => inp.Trim() != string.Empty).ToList();
            if (input.Count < 3)
                return false;
            string sourceComponent = input[1];

            var dep = dependencyManager.Get(sourceComponent);
            if(dep == null)
            {

                dep= dependencyManager.Add(sourceComponent);
            }

            for (int i = 2; i < input.Count(); i++)
            {
                var curr = input[i];
                var currDependency = dependencyManager.Get(curr);
                if (currDependency==null)
                {
                    currDependency = dependencyManager.Add(curr);
                }
                dep.AddDependency(currDependency);
            }

            return true;
        }

    }
}
