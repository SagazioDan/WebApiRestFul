using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalCityManager.Models
{
    public class Enrollement
    {
        public int EnrollementID { set; get; }
        public int CourseID { set; get; }
        public int StudentID { set; get; }
        public int? Grade { set; get; }


        public Course Course { set; get; }
        public Student Student { set; get; }            
     }
}
