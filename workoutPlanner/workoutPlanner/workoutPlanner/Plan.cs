using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensionsAsync;


namespace workoutPlanner
{
    public class Plan
    {
        [PrimaryKey, AutoIncrement]
        public int IDPlan { get; set; }
        //unique na probe
        //[Unique]
        public string NamePlan { get; set; }
        public string Details { get; set; }
        public int ExerciseID { get; set; }

        //[ManyToMany(typeof(PlanExercise))]
        //public List<Exercise> ExercisesInPlan { get; set; }       
        
        //public int test { get; set; }
    }
}