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
        public int ID { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }

        [ManyToMany(typeof(PlanExercise))]
        public List<Exercise> ExercisesInPlan { get; set; }        
    }
}