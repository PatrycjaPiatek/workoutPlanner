using System;
using System.Text;

using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;


namespace workoutPlanner
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Plan>().Wait();
            _database.CreateTableAsync<Exercise>().Wait();
            _database.CreateTableAsync<PlanExercise>().Wait();
        }

        //shows plans
        public Task<List<Plan>> GetPlansAsync()
        {
            return _database.Table<Plan>().ToListAsync();
        }
        //shows exercises
        public Task<List<Exercise>> GetExercisesAsync()
        {
            return _database.Table<Exercise>().ToListAsync();
        }
        //save plan
        public Task<int> SavePlanAsync(Plan plan)
        {
            return _database.InsertAsync(plan);
        }
        //save exercise
        public Task<int> SaveExerciseAsync(Exercise exercise)
        {
            return _database.InsertAsync(exercise);
        }
        //delete plan
        public Task<int> DeletePlanAsync(Plan plan)
        {
            return _database.DeleteAsync(plan);
        }
        //delete exercise
        public Task<int> DeleteExerciseAsync(Exercise exercise)
        {
            return _database.DeleteAsync(exercise);
        }
        //update plan
        public Task<int> UpdatePlanAsync(Plan plan)
        {
            if (plan.ID != 0)
            {
                //return _database.
                //_database.UpdateWithChildren(plan);
                return _database.UpdateAsync(plan);
            }
            else
            {
                return _database.InsertAsync(plan);
            }
        }
        //update exercise
        public Task<int> UpdateExerciseAsync(Exercise exercise)
        {
            if (exercise.ID != 0)
            {
                //return _database.
                //_database.UpdateWithChildren(exercise);
                return _database.UpdateAsync(exercise);
            }
            else
            {
                return _database.InsertAsync(exercise);
            }
        }
    }
}
