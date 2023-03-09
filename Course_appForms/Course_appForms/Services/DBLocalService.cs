using System;
using System.Collections.Generic;
using System.Text;
using Course_appForms.Models;
using SQLite;


namespace Course_appForms.Services
{
    public class DBLocalService
    {
        SQLiteConnection database;
        public DBLocalService(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<SheduleDB>();
        }
        public IEnumerable<SheduleDB> GetItems()
        {
            return database.Table<SheduleDB>().ToList();
        }
        public SheduleDB GetItem(int id)
        {
            return database.Get<SheduleDB>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<SheduleDB>(id);
        }
        public void TruncateTable()
        {
            database.Execute($"delete from Shedule");
        }
        public int SaveItem(SheduleDB item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }
}
