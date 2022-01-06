using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace workoutPlanner
{
    public class Exercise
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Img { get; set; }

        [ManyToMany(typeof(PersonEvent))]
        public List<Person> Participants { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
