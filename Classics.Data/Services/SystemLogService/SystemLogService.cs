using System.Collections.Generic;
using Classics.Data.Services.DatabaseService;
using Classics.Infra;


namespace Classics.Data.Services.SystemLogService
{
    class SystemLogService : ISystemLogService
    {
        private readonly IDatabaseService _databaseService;
        private readonly DataBase _database;

        public SystemLogService(string connectionString)
        {
            connectionString = "data source=classics.database.windows.net;initial catalog=classics;persist security info=True;user id=developer;password=devapp@22;multipleActiveResultSets=True";
            _databaseService = new DatabaseService.DatabaseService();
            _database = new DataBase(connectionString, 100);
        }

        public void Save(List<ObjectValue.SystemLog> logs)
        {
            _database.OpenConnection();
            var systemLogsInternalTable = _databaseService.ConvertToInternalTable(logs, "systemLogs");
            _database.BulkInsert(systemLogsInternalTable);
        }
    }
}