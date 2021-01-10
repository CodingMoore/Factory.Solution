using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
    public class Engineer
    {
        public Engineer()
        {
            this.JoinEntries = new HashSet<EngineerMachine>(); //creating an empty hash set of course students. HashSet is an unordered collection of unique elements. "Studends" would be more accurately named "JoinEntries" since it is a HashSet of CourseStudents.
        }
        public int EngineerId { get; set; }

        [DisplayName("Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get; set; }

        [DisplayName("Engineer Name")]
        public string EngineerName { get; set; }
        
        public virtual ICollection<EngineerMachine> JoinEntries { get; set; } //IColletion is basically a list. The ICollection<T> interface is the base interface for classes in the System.Collections.Generic namespace.
    }
}

