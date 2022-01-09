using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;


namespace workoutPlanner
{
    public class Exercise
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Img { get; set; }

        //[ManyToMany(typeof(PlanExercise))]
        //public List<Plan> PlansWithExercise { get; set; }

        //public override string ToString()
        //{
        //    return ExcerciseName;
        //}
    }
}
