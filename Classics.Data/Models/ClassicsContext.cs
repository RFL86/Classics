using System;
using System.Data.Entity;
using System.Linq;
using Classics.Data.Services.ChangeTrackerService;
using Classics.Data.Services.SystemLogService;
using System.Threading;


namespace Classics.Data.Models
{
    public class ClassicsContext : ClassicsContextLog
    {
        public Guid UserId;
        public const string ContextName = "ClassicsContext";
        private readonly IChangeTrackerService _changeTrackerService;
        //private readonly ISystemLogService _systemLogService;

        public ClassicsContext(ExtraInfo extra = null) : base(ContextName, extra)
        {
            _changeTrackerService = new ChangeTrackerService();
           // _systemLogService = new SystemLogService(ContextName);
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ClassicsContext>(null);
            base.OnModelCreating(modelBuilder);
        }
        //public override int SaveChanges()
        //{
        //    try
        //    {
        //        var systemLogs = _changeTrackerService.GetChangeTrackerEntries(this, UserId);

        //        if (systemLogs.Any())
        //        {
        //            var thread = new Thread(() =>
        //            {
        //                _systemLogService.Save(systemLogs);
        //            });

        //            thread.Start();
        //        }
        //    }

        //    catch (Exception)
        //    {
        //        return base.SaveChanges();
        //    }


        //    return base.SaveChanges();
        //}
    }
    public class ExtraInfo
    {
        /// <summary>
        /// This Properties reflecs on .net core appsetting.json envName
        /// </summary>
        public string EnvName { get; set; }
        /// <summary>
        /// This property reflects on  Log changes, the user in this property  is stored as the one whom made the change
        /// </summary>
        public Guid LoggedUser { get; set; }
    }

}
