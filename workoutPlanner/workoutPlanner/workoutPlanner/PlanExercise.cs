using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensionsAsync;

namespace workoutPlanner
{
    public class PlanExercise
    {
        [ForeignKey(typeof(Plan))]
        public int PlanId { get; set; }

        [ForeignKey(typeof(Exercise))]
        public int ExerciseId { get; set; }
    }
}

