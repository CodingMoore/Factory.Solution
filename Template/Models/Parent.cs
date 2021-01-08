using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Template.Models
{
    public class Parent
    {
        public Parent()
        {
            this.JoinEntries = new HashSet<ParentChild>(); //creating an empty hash set of course students. HashSet is an unordered collection of unique elements. "Studends" would be more accurately named "JoinEntries" since it is a HashSet of CourseStudents.
        }
        [DisplayName("Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd hh:mm tt}")]
        public DateTime StartDate { get; set; }
        public int ParentId { get; set; }

        [DisplayName("Parent Name")]
        public string ParentName { get; set; }
        public virtual ICollection<ParentChild> JoinEntries { get; set; } //IColletion is basically a list. The ICollection<T> interface is the base interface for classes in the System.Collections.Generic namespace.
    }
}

