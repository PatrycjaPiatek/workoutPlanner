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
        readonly SQLiteAsyncConnection _dbdb;

        public Database(string dbPath)
        {
            _dbdb = new SQLiteAsyncConnection(dbPath);
            _dbdb.CreateTableAsync<Plan>().Wait();
            _dbdb.CreateTableAsync<Exercise>().Wait();
            //_dbdb.CreateTableAsync<PlanExercise>().Wait();

            ////////////////////////////////////////////////////
            //testowanie
            var plan1 = new Plan
            {
                NamePlan = "1Plan Test",
                Details = "1details Plan Test",
                ExerciseID = 1
            };

            var plan1a = new Plan
            {
                NamePlan = "1Plan Test",
                Details = "2details Plan Test",
                ExerciseID = 2
            };

            var plan2 = new Plan
            {
                NamePlan = "2Plan Test",
                Details = "2details Plan Test",
                ExerciseID = 3
            };

            var exercise1 = new Exercise
            {
                NameExercise = "1Exercise Test",
                Category = "1category Exercise Test"
            };

            var exercise2 = new Exercise
            {
                NameExercise = "2Exercise Test",
                Category = "2category Exercise Test"
            };

            var exercise3 = new Exercise
            {
                NameExercise = "3Exercise Test",
                Category = "3category Exercise Test"
            };

            var exercise4 = new Exercise
            {
                NameExercise = "4Exercise Test",
                Category = "4category Exercise Test"
            };

            _dbdb.InsertAsync(exercise1);
            _dbdb.InsertAsync(exercise2);
            _dbdb.InsertAsync(exercise3);
            _dbdb.InsertAsync(exercise4);

            _dbdb.InsertAsync(plan1);
            _dbdb.InsertAsync(plan1a);
            _dbdb.InsertAsync(plan2);


            //plan1.ExercisesInPlan = new List<Exercise> {};
            //plan1.ExercisesInPlan.Add(exercise1);
            //plan1.ExercisesInPlan.Add(exercise2);

            //plan2.ExercisesInPlan = new List<Exercise> { };
            //plan2.ExercisesInPlan.Add(exercise3);
            //plan2.ExercisesInPlan.Add(exercise4);

            //plan3.ExercisesInPlan = new List<Exercise> { };
            //plan3.ExercisesInPlan.Add(exercise1);
            //plan3.ExercisesInPlan.Add(exercise2);
            //plan3.ExercisesInPlan.Add(exercise3);
            //plan3.ExercisesInPlan.Add(exercise4);

            ////plan1.ExercisesInPlan = new List<Exercise> { exercise1 };            
            ////_dbdb.UpdateAsync(plan1);
            //_dbdb.UpdateWithChildrenAsync(plan1);
            //_dbdb.UpdateWithChildrenAsync(plan2);
            //_dbdb.UpdateWithChildrenAsync(plan3);

            ////to jest nawet chyba niepotrzebne
            //var personStored1 = _dbdb.GetWithChildrenAsync<Plan>(plan1.IDPlan);
            //var personStored2 = _dbdb.GetWithChildrenAsync<Plan>(plan2.IDPlan);
            //var personStored3 = _dbdb.GetWithChildrenAsync<Plan>(plan3.IDPlan);
            //var eventStored1 = _dbdb.GetWithChildrenAsync<Exercise>(exercise1.IDPlan);
            //var eventStored2 = _dbdb.GetWithChildrenAsync<Exercise>(exercise2.IDPlan);
            //var eventStored3 = _dbdb.GetWithChildrenAsync<Exercise>(exercise3.IDPlan);
            //var eventStored4 = _dbdb.GetWithChildrenAsync<Exercise>(exercise4.IDPlan);
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
            if (plan.IDPlan != 0)
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
            if (exercise.IDExercise != 0)
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
        public Task<List<Exercise>> GetExcercisesOfPlan(string PlanName)
        {
            return _dbdb.QueryAsync<Exercise>("SELECT * FROM [Plan] WHERE [NamePlan] LIKE \"" + PlanName + "\"");
        }

    }
}
