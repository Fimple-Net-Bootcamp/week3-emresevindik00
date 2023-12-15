using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Health
    {
        public int Id { get; set; }
        public string Situation { get; set; }
        public Pet Pet { get; set; }
    }
}
