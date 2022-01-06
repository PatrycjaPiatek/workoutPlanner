using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;


namespace workoutPlanner
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        [ManyToMany(typeof(PersonEvent))]
        public List<Exercise> Events { get; set; }        
    }
}