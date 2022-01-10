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
        public int IDExcerciseInPlan { get; set; }
        public int NamePlan { get; set; }
        public string ListOfExcercisesName { get; set; }
        //public string Details { get; set; }

        //[ManyToMany(typeof(PlanExercise))]
        //public List<Exercise> ExercisesInPlan { get; set; }       
        
        //public int test { get; set; }
    }
}