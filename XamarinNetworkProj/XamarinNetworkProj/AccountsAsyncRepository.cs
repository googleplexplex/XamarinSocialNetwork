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
        public SQLiteAsyncConnection database;
        public int lastErr;

        public AsyncRepository(SQLiteAsyncConnection database)
        {
            this.database = database;
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
        public FriendAsyncRepository(SQLiteAsyncConnection database) : base(database) { }
        public async Task<List<Account>> GetItemsAsyncById(int accId)
        {
            return await database.Table<Account>().Where(f => f.Id == accId).ToListAsync();
        }
        public async Task<Account> GetItemAsyncByLP(string login, string password)
        {
            return await database.Table<Account>().FirstOrDefaultAsync(f => f.nickname == login && f.password == password);
        }
    }
    public class PostAsyncRepository : AsyncRepository<Post>
    {
        public PostAsyncRepository(SQLiteAsyncConnection database) : base(database) { }
        public async Task<List<Post>> GetItemsAsyncById(int autorId)
        {
            return await database.Table<Post>().Where(f => f.autorId == autorId).ToListAsync();
        }
    }


}
