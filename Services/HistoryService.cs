using MiviaMaui.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiviaMaui.Services
{
    public class HistoryService
    {
        private readonly SQLiteAsyncConnection _database;

        private string _dbPath = DatabasePath.GetDatabasePath("MiviaMaui.db3");

        public HistoryService(string? dbPath = null)
        {
            if (dbPath == null)
            {
                dbPath = _dbPath;
            }

            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<HistoryRecord>().Wait();
        }

        public Task<List<HistoryRecord>> GetHistoryAsync()
        {
            return _database.Table<HistoryRecord>().ToListAsync();
        }

        public Task<int> SaveHistoryRecordAsync(HistoryRecord record)
        {
            if (record.Id != 0)
            {
                return _database.UpdateAsync(record);
            }
            else
            {
                return _database.InsertAsync(record);
            }
        }

        public Task<int> DeleteHistoryRecordAsync(HistoryRecord record)
        {
            return _database.DeleteAsync(record);
        }

        public async Task DropHistoryTableAsync()
        {
            await _database.ExecuteAsync("DROP TABLE IF EXISTS HistoryRecord");
        }

        public async Task RecreateHistoryTableAsync()
        {
            await DropHistoryTableAsync();  // Drop the existing table
            await _database.CreateTableAsync<HistoryRecord>();  // Recreate the table with the updated schema
        }


    }
}
