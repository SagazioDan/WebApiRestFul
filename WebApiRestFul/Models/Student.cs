using GlobalCityManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Student
{
    public int ID { get; set; } 
    public string LastName { get; set; }
    public string FirstMidName { get; set; }
    public DateTime EnrollementDate { get; set; }


    public ICollection<Enrollement> Enrollments { get; set; }
}
