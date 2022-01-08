using System;
using System.Text;

using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace workoutPlanner
{
    public class Database
    {
        readonly SQLiteAsyncConnection _plansDataBase;

        public Database(string dbPath)
        {
            _plansDataBase = new SQLiteAsyncConnection(dbPath);
            //_plansDataBase.CreateTableAsync<Plan>().Wait();
            _plansDataBase.CreateTableAsync<Exercise>().Wait();
            //_plansDataBase.
            //_plansDataBase.CreateTableAsync<PlanExercise>().Wait();

            ////////////////////////////////////////////////////
            //testowanie
            //var plan1 = new Plan
            //{
            //    Name = "Plan Test",
            //    Details = "details Plan Test"
            //};

            var exercise1 = new Exercise
            {
                Name = "First exercise test",
                Category = "First category exercise test"
            };

            //_plansDataBase.InsertAsync(plan1);
            _plansDataBase.InsertAsync(exercise1);

            //plan1.ExercisesInPlan = new List<Exercise> { exercise1 };
            //_plansDataBase.UpdateAsync(plan1);

            //var personStored = _plansDataBase.GetAsync<Plan>(plan1.ID);
            //var eventStored = _plansDataBase.GetAsync<Exercise>(exercise1.ID);
        }
        //testowanie
        ////////////////////////////////////////////////////

        //shows plans
        public Task<List<Plan>> GetPlansAsync()
        {
            return _plansDataBase.Table<Plan>().ToListAsync();
        }
        //shows exercises
        public Task<List<Exercise>> GetExercisesAsync()
        {
            return _plansDataBase.Table<Exercise>().ToListAsync();
        }
        //save plan
        public Task<int> SavePlanAsync(Plan plan)
        {
            return _plansDataBase.InsertAsync(plan);
        }
        //save exercise
        public Task<int> SaveExerciseAsync(Exercise exercise)
        {
            return _plansDataBase.InsertAsync(exercise);
        }
        //delete plan
        public Task<int> DeletePlanAsync(Plan plan)
        {
            return _plansDataBase.DeleteAsync(plan);
        }
        //delete exercise
        public Task<int> DeleteExerciseAsync(Exercise exercise)
        {
            return _plansDataBase.DeleteAsync(exercise);
        }
        //update plan
        public Task<int> UpdatePlanAsync(Plan plan)
        {
            if (plan.ID != 0)
            {
                //return _plansDataBase.
                //_plansDataBase.UpdateWithChildren(plan);
                return _plansDataBase.UpdateAsync(plan);
            }
            else
            {
                return _plansDataBase.InsertAsync(plan);
            }
        }
        //update exercise
        public Task<int> UpdateExerciseAsync(Exercise exercise)
        {
            if (exercise.ID != 0)
            {
                //return _plansDataBase.
                //_plansDataBase.UpdateWithChildren(exercise);
                return _plansDataBase.UpdateAsync(exercise);
            }
            else
            {
                return _plansDataBase.InsertAsync(exercise);
            }
        }
    }
}
