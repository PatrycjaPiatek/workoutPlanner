using System;
using System.Text;

using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace workoutPlanner
{
    public class Database
    {
        readonly SQLiteAsyncConnection _dbdb;

        public Database(string dbPath)
        {
            _dbdb = new SQLiteAsyncConnection(dbPath);
            _dbdb.CreateTableAsync<Plan>().Wait();
            _dbdb.CreateTableAsync<Exercise>().Wait();
            _dbdb.CreateTableAsync<PlanExercise>().Wait();

            ////////////////////////////////////////////////////
            //testowanie
            var plan1 = new Plan
            {
                Name = "Plan Test",
                Details = "details Plan Test"
            };

            var exercise1 = new Exercise
            {
                Name = "Exercise Test",
                Category = "category Exercise Test"
            };

            _dbdb.InsertAsync(plan1);
            _dbdb.InsertAsync(exercise1);

            plan1.ExercisesInPlan = new List<Exercise> { exercise1 };
            _dbdb.UpdateAsync(plan1);

            var personStored = _dbdb.GetAsync<Plan>(plan1.ID);
            var eventStored = _dbdb.GetAsync<Exercise>(exercise1.ID);
        }
        //testowanie
        ////////////////////////////////////////////////////

        //shows plans
        public Task<List<Plan>> GetPlansAsync()
        {
            return _dbdb.Table<Plan>().ToListAsync();
        }
        //shows exercises
        public Task<List<Exercise>> GetExercisesAsync()
        {
            return _dbdb.Table<Exercise>().ToListAsync();
        }
        //save plan
        public Task<int> SavePlanAsync(Plan plan)
        {
            return _dbdb.InsertAsync(plan);
        }
        //save exercise
        public Task<int> SaveExerciseAsync(Exercise exercise)
        {
            return _dbdb.InsertAsync(exercise);
        }
        //delete plan
        public Task<int> DeletePlanAsync(Plan plan)
        {
            return _dbdb.DeleteAsync(plan);
        }
        //delete exercise
        public Task<int> DeleteExerciseAsync(Exercise exercise)
        {
            return _dbdb.DeleteAsync(exercise);
        }
        //update plan
        public Task<int> UpdatePlanAsync(Plan plan)
        {
            if (plan.ID != 0)
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
