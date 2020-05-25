using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDependency
{
    public class DependencyManager
    {
        public Dictionary<string, Dependency> dependency;
        public DependencyManager()
        {
            dependency = new Dictionary<string, Dependency>();
        }

        internal Dependency Get(string sourceComponent)
        {
            
            if(dependency.ContainsKey(sourceComponent))
            return dependency[sourceComponent];
            return null;
        }

        internal Dependency Add(string name)
        {
            var dep = new Dependency(name);
            dependency.Add(dep.Name, dep);
            return dep;
        }
    }
}
