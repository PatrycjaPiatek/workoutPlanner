using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace workoutPlanner
{
    public class PersonEvent
    {
        [ForeignKey(typeof(Person))]
        public int PersonId { get; set; }

        [ForeignKey(typeof(Exercise))]
        public int EventId { get; set; }
    }
}

