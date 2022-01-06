using System;
using System.Text;

using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;


namespace workoutPlanner
{
    public class Database
    {
        readonly SQLiteAsyncConnection _dbase;

        public Database(string dbPath)
        {
            _dbase = new SQLiteAsyncConnection(dbPath);
            _dbase.CreateTableAsync<Plan>().Wait();
            _dbase.CreateTableAsync<Exercise>().Wait();
            _dbase.CreateTableAsync<PlanExercise>().Wait();
        }

        //shows plans
        public Task<List<Plan>> GetPlansAsync()
        {
            return _dbase.Table<Plan>().ToListAsync();
        }
        //shows exercises
        public Task<List<Exercise>> GetExercisesAsync()
        {
            return _dbase.Table<Exercise>().ToListAsync();
        }
        //save plan
        public Task<int> SavePlanAsync(Plan plan)
        {
            return _dbase.InsertAsync(plan);
        }
        //save exercise
        public Task<int> SaveExerciseAsync(Exercise exercise)
        {
            return _dbase.InsertAsync(exercise);
        }
        //delete plan
        public Task<int> DeletePlanAsync(Plan plan)
        {
            return _dbase.DeleteAsync(plan);
        }
        //delete exercise
        public Task<int> DeleteExerciseAsync(Exercise exercise)
        {
            return _dbase.DeleteAsync(exercise);
        }
        //update plan
        public Task<int> UpdatePlanAsync(Plan plan)
        {
            if (plan.ID != 0)
            {
                //return _dbase.
                //_dbase.UpdateWithChildren(plan);
                return _dbase.UpdateAsync(plan);
            }
            else
            {
                return _dbase.InsertAsync(plan);
            }
        }
        //update exercise
        public Task<int> UpdateExerciseAsync(Exercise exercise)
        {
            if (exercise.ID != 0)
            {
                //return _dbase.
                //_dbase.UpdateWithChildren(exercise);
                return _dbase.UpdateAsync(exercise);
            }
            else
            {
                return _dbase.InsertAsync(exercise);
            }
        }
    }
}
