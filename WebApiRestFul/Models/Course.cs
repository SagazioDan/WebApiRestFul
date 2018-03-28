using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalCityManager.Models
{
    public class Course
    {

        public int ID { get; set; }
        public string Title { get; set; }
        public string Credits { get; set; }

        public ICollection<Enrollement> Enrollements { set; get; }


    }
}
