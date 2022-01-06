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
            _database.CreateTableAsync<Person>().Wait();
            _database.CreateTableAsync<Exercise>().Wait();
        }

        public Task<List<Person>> GetPeopleAsync()
        {
            return _database.Table<Person>().ToListAsync();
        }

        public Task<int> SavePersonAsync(Person person)
        {
            return _database.InsertAsync(person);
        }

        public Task<int> DeleteItemAsync(Person person)
        {
            return _database.DeleteAsync(person);
        }
        public Task<int> SaveItemAsync(Person person)
        {
            if (person.ID != 0)
            {
                //return _database.
                //_database.UpdateWithChildren(person);
                return _database.UpdateAsync(person);
            }
            else
            {
                return _database.InsertAsync(person);
            }
        }
    }
}
