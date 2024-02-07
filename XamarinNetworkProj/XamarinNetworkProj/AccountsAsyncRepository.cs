using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using SQLite;
using XamarinNetworkProj.Model;

namespace XamarinNetworkProj
{
    public class AsyncRepository<T> where T : new()
    {
        SQLiteAsyncConnection database;

        public AsyncRepository(string databasePath)
        {
            database = new SQLiteAsyncConnection(databasePath);
        }

        public async Task CreateTable()
        {
            await database.CreateTableAsync<T>();
        }
        public async Task<List<T>> GetItemsAsync()
        {
            return await database.Table<T>().ToListAsync();
        }
        public async Task<T> GetItemAsync(int id)
        {
            return await database.GetAsync<T>(id);
        }
        public async Task<int> DeleteItemAsync(T item)
        {
            return await database.DeleteAsync(item);
        }
        public async Task<int> InsertItemAsync(T item)
        {
            return await database.InsertAsync(item);
        }
        public async Task<int> UpdateItemAsync(T item)
        {
            return await database.UpdateAsync(item);
        }
        public async Task<int> Clear()
        {
            return await database.DeleteAllAsync<T>();
        }
    }


    public class FriendAsyncRepository : AsyncRepository<Account>
    {
        public FriendAsyncRepository(string databasePath) : base(databasePath) { }
    }
    public class PostAsyncRepository : AsyncRepository<Post>
    {
        public PostAsyncRepository(string databasePath) : base(databasePath) { }
    }


}
