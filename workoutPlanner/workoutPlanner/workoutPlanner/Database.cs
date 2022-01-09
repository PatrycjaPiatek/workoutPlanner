using System;
using System.Text;

using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using System.IO;
using Xamarin.Essentials;
using SQLiteNetExtensionsAsync.Extensions;

namespace workoutPlanner
{
    public class Database
    {
        public readonly SQLiteAsyncConnection _dbdb;

        public Database(string dbPath)
        {
            _dbdb = new SQLiteAsyncConnection(dbPath);
            _dbdb.CreateTableAsync<Name>().Wait();
            _dbdb.CreateTableAsync<Exercise>().Wait();

            ////////////////////////////////////////////////////
            //testowanie
            var exercise1 = new Exercise
            {
                Name = "bench press",
                Category = "chest",
                Img = "benchPress.png",
                Details = "The bench press helps build many muscles in the upper body. You can do this exercise with either a barbell or dumbbells. Perform bench presses regularly as part of an upper-body workout for increased strength and muscle development."
            };

            var exercise2 = new Exercise
            {
                Name = "dumbbell bent-over row on bench",
                Category = "back",
                Img = "dumbbellBentOverRow.png",
                Details = "The one-arm dumbbell row is a good addition to any dumbbell workout. This movement targets the upper and lower back, shoulders, biceps, and hips while improving core stability.1 Five different joint actions take place in this compound exercise. Beginners can use light weights as they build strength. This is also a good exercise to do as part of a circuit training routine."
            };

            var exercise3 = new Exercise
            {
                Name = "hip thrust",
                Category = "lower body",
                Img = "pants.png",
                Details = "The hip thruster is effective for improving hip extension by engaging the hamstrings and gluteal muscles. Your hips extend when they move from a flexed position (where the hips are lower than or behind the shoulders and knees) to a fully extended position where the hips, shoulders, and knees are in line."
            };

            ////dodawane
            //var exercise4 = new Exercise
            //{
            //    Name = "bicycle crunches",
            //    Category = "abs",
            //    Img = "bicycleCrunches.png"
            //};
            ////dodawane
            //var exercise5 = new Exercise
            //{
            //    Name = "side plank leg lift",
            //    Category = "legs",
            //    Img = "sidePlankLegLift.png"
            //};

            _dbdb.InsertAsync(exercise1);
            _dbdb.InsertAsync(exercise2);
            _dbdb.InsertAsync(exercise3);
            //_dbdb.InsertAsync(exercise4);
            //_dbdb.InsertAsync(exercise5);

            var name1 = new Name
            {
                ExcerciseName = exercise1.Name
            };

            var name2 = new Name();

            _dbdb.InsertAsync(name1);
            _dbdb.InsertAsync(name2);

            name2.ExcerciseName = exercise2.Name;

            _dbdb.UpdateAsync(name1);
            _dbdb.UpdateAsync(name2);
        }
        //testowanie
        ////////////////////////////////////////////////////

        //shows plans
        public Task<List<Name>> GetPlansAsync()
        {
            return _dbdb.Table<Name>().ToListAsync();
        }
        //shows exercises
        public Task<List<Exercise>> GetExercisesAsync()
        {
            return _dbdb.Table<Exercise>().ToListAsync();
        }
        //save name
        public Task<int> SaveNameAsync(Name name)
        {
            return _dbdb.InsertAsync(name);
        }
        //save exercise
        public Task<int> SaveExerciseAsync(Exercise exercise)
        {
            return _dbdb.InsertAsync(exercise);
        }
        //delete name
        public Task<int> DeletePlanAsync(Name plan)
        {
            return _dbdb.DeleteAsync(plan);
        }
        //delete exercise
        public Task<int> DeleteExerciseAsync(Exercise exercise)
        {
            return _dbdb.DeleteAsync(exercise);
        }
        //update name
        public Task<int> UpdateNameAsync(Name name)
        {
            if (name.IDExcerciseInPlan != 0)
            {
                //return _dbdb.
                //_dbdb.UpdateWithChildren(name);
                return _dbdb.UpdateAsync(name);
            }
            else
            {
                return _dbdb.InsertAsync(name);
            }
        }
        //update exercise
        public Task<int> UpdateExerciseAsync(Exercise exercise)
        {
            if (exercise.ID != 0)
            {
                //return _dbdb.
                //_dbdb.UpdateWithChildren(exercise);
                return _dbdb.UpdateAsync(exercise);
            }
            else
            {
                return _dbdb.InsertAsync(exercise);
            }
        }
        //zwraca liste nazw
        public Task<List<Exercise>> ExcerciseQuery()
        {
            return _dbdb.QueryAsync<Exercise>("SELECT Name FROM [Excercise]");
        }
    }
}
