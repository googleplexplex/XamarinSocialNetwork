using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using SQLite;
using XamarinNetworkProj.Model;

namespace XamarinNetworkProj
{
    public class FriendAsyncRepository
    {
        SQLiteAsyncConnection database;

        public FriendAsyncRepository(string databasePath)
        {
            database = new SQLiteAsyncConnection(databasePath);
        }

        public async Task CreateTable()
        {
            await database.CreateTableAsync<Account>();
        }
        public async Task<List<Account>> GetItemsAsync()
        {
            return await database.Table<Account>().ToListAsync();

        }
        public async Task<Account> GetItemAsync(int id)
        {
            return await database.GetAsync<Account>(id);
        }
        public async Task<int> DeleteItemAsync(Account item)
        {
            return await database.DeleteAsync(item);
        }
        public async Task<int> SaveItemAsync(Account item)
        {
            if (item.Id != 0)
            {
                int err = await database.UpdateAsync(item);
                return item.Id;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }
    }
}
