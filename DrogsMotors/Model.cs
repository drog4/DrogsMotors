using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrogsMotors
{
    public class Model
    {
        public int id { get; set; }
        public string name { get; set; }
        public string info { get; set; }

        public Model() { }
        public Model(int id, string name, string info)
        {
            this.id = id;
            this.name = name;
            this.info = info;
        }
    }
}
