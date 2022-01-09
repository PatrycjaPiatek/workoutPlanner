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
            //_dbdb.CreateTableAsync<PlanExercise>().Wait();

            ////////////////////////////////////////////////////
            //testowanie
            var name1 = new Name
            {

            };
            var name2 = new Name
            {

            };
            var name3 = new Name
            {

            };

            var exercise1 = new Exercise
            {
                Name = "1Exercise Test",
                Category = "1category Exercise Test"
            };

            var exercise2 = new Exercise
            {
                Name = "2Exercise Test",
                Category = "2category Exercise Test"
            };

            var exercise3 = new Exercise
            {
                Name = "3Exercise Test",
                Category = "3category Exercise Test"
            };

            var exercise4 = new Exercise
            {
                Name = "4Exercise Test",
                Category = "4category Exercise Test"
            };

            _dbdb.InsertAsync(exercise1);
            _dbdb.InsertAsync(exercise2);
            _dbdb.InsertAsync(exercise3);
            _dbdb.InsertAsync(exercise4);

            _dbdb.InsertAsync(name1);
            _dbdb.InsertAsync(name2);
            _dbdb.InsertAsync(name3);

            name1.ExcerciseName = exercise1.Name;
            name2.ExcerciseName = exercise2.Name;
            name3.ExcerciseName = exercise4.Name;

            _dbdb.UpdateAsync(name1);
            _dbdb.UpdateAsync(name2);
            _dbdb.UpdateAsync(name3);

            ////name1.ExercisesInPlan = new List<Exercise> { exercise1 };            
            ////_dbdb.UpdateAsync(name1);
            //_dbdb.UpdateWithChildrenAsync(name1);
            //_dbdb.UpdateWithChildrenAsync(plan2);
            //_dbdb.UpdateWithChildrenAsync(plan3);

            ////to jest nawet chyba niepotrzebne
            //var personStored1 = _dbdb.GetWithChildrenAsync<Name>(name1.IDExcerciseInPlan);
            //var personStored2 = _dbdb.GetWithChildrenAsync<Name>(plan2.IDExcerciseInPlan);
            //var personStored3 = _dbdb.GetWithChildrenAsync<Name>(plan3.IDExcerciseInPlan);
            //var eventStored1 = _dbdb.GetWithChildrenAsync<Exercise>(exercise1.ID);
            //var eventStored2 = _dbdb.GetWithChildrenAsync<Exercise>(exercise2.ID);
            //var eventStored3 = _dbdb.GetWithChildrenAsync<Exercise>(exercise3.ID);
            //var eventStored4 = _dbdb.GetWithChildrenAsync<Exercise>(exercise4.ID);
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
        //save plan
        public Task<int> SavePlanAsync(Name plan)
        {
            return _dbdb.InsertAsync(plan);
        }
        //save exercise
        public Task<int> SaveExerciseAsync(Exercise exercise)
        {
            return _dbdb.InsertAsync(exercise);
        }
        //delete plan
        public Task<int> DeletePlanAsync(Name plan)
        {
            return _dbdb.DeleteAsync(plan);
        }
        //delete exercise
        public Task<int> DeleteExerciseAsync(Exercise exercise)
        {
            return _dbdb.DeleteAsync(exercise);
        }
        //update plan
        public Task<int> UpdatePlanAsync(Name plan)
        {
            if (plan.IDExcerciseInPlan != 0)
            {
                //return _dbdb.
                //_dbdb.UpdateWithChildren(plan);
                return _dbdb.UpdateAsync(plan);
            }
            else
            {
                return _dbdb.InsertAsync(plan);
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
    }
}
