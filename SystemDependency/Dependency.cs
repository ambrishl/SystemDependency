using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDependency
{
    public class Dependency
    {
        private string _name = string.Empty;
        public Dependency(string name)
        {
            _name = name.ToUpper();
            Dependencies = new List<Dependency>();
        }
        public String Name
        {
            get
            {
                return _name;
            }
        }

        
        public List<Dependency> Dependencies { get;  }

        public void AddDependency(Dependency dep)
        {
            var curr = Dependencies.FirstOrDefault(d => d.Name == dep.Name);
            if(curr==null)
            {
                Dependencies.Add(dep);
            }
        }
        public int DepenedencyCount()
        {
            //int count = 0;
            return Dependencies.Sum(dep => dep.DepenedencyCount());/*;*/

        }

        public bool HasDependency(string component)
        {
           

           var result =  Dependencies.FirstOrDefault(dep => dep.Name == component);
            if (result==null)
                return false;

            if (Dependencies.TrueForAll(dep => !dep.HasDependency(component)))
                return true;
            return false;
            
        }

    }
}
