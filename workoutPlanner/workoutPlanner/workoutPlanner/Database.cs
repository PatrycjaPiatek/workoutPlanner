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
        }

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
