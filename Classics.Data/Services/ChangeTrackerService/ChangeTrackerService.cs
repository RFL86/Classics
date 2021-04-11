using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Infrastructure;
using EntityState = System.Data.Entity.EntityState;
using Classics.Data.Models;

namespace Classics.Data.Services.ChangeTrackerService
{
    public class ChangeTrackerService : IChangeTrackerService
    {
        private Guid GetPrimaryKeyValue(DbEntityEntry entry, IObjectContextAdapter toneFinderContext)
        {

            var objectStateEntry = toneFinderContext.ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity).EntitySet;
            var keyName = objectStateEntry.ElementType.KeyMembers.Select(k => k.Name).ToArray().FirstOrDefault();

            var values = entry.State == EntityState.Deleted
                ? entry.OriginalValues.PropertyNames.ToDictionary(pn => pn, pn => entry.OriginalValues[pn])
                : entry.CurrentValues.PropertyNames.ToDictionary(pn => pn, pn => entry.CurrentValues[pn]);

            return keyName != null ? new Guid(values[keyName].ToString()) : new Guid();
        }

        public void SetCorrectOriginalValues(DbEntityEntry entry)
        {
            var values = entry.CurrentValues.Clone();
            var state = entry.State;
            entry.Reload();
            entry.CurrentValues.SetValues(values);
            entry.State = state;
        }

        private string GetEntityName(DbEntityEntry entry)
        {
            var baseType = entry.Entity.GetType().BaseType;
            var entityName = entry.State != EntityState.Added ? ((baseType != null) ? baseType.Name : null) : entry.Entity.GetType().Name;
            if (entityName == "Object")
                entityName = entry.Entity.GetType().Name;
            return entityName;
        }

        //public List<ObjectValue.SystemLog> GetChangeTrackerEntries(ClassicsContext toneFinderContext, Guid userId)
        //{
        //    var listAlterValue = new List<ObjectValue.SystemLog>();
        //    var changeTrackerEntries = toneFinderContext.ChangeTracker.Entries().Where(a => a.State != EntityState.Unchanged && a.State != EntityState.Detached && a.State != EntityState.Added);

        //    foreach (var entry in changeTrackerEntries)
        //    {
        //        var entityName = GetEntityName(entry);

        //        if (toneFinderContext.SystemLogTable.Any(slt => slt.TableName == entityName))
        //            continue;

        //        var currentValues = entry.State == EntityState.Deleted ? null : entry.CurrentValues.PropertyNames.ToDictionary(pn => pn, pn => entry.CurrentValues[pn]);


        //        if (entry.State == EntityState.Added)
        //        {

        //            listAlterValue.AddRange(from currentValue in currentValues
        //                                    let value = currentValue.Value?.ToString()
        //                                    where value != null
        //                                    select new ObjectValue.SystemLog
        //                                    {
        //                                        TableName = entityName,
        //                                        Created = true,
        //                                        FieldName = currentValue.Key,
        //                                        CurrentValue = value,
        //                                        OriginalValue = null,
        //                                        UserId = userId,
        //                                        SystemLogId = Guid.NewGuid(),
        //                                        ReferId = GetPrimaryKeyValue(entry, toneFinderContext),
        //                                        CreatedOn = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"))
        //                                    });
        //        }

        //        if (entry.State == EntityState.Deleted)
        //        {
        //            var originalValues = entry.OriginalValues.PropertyNames.ToDictionary(pn => pn, pn => entry.OriginalValues[pn]);

        //            listAlterValue.AddRange(from originalValue in originalValues
        //                                    let value = originalValue.Value?.ToString()
        //                                    where value != null
        //                                    select new ObjectValue.SystemLog
        //                                    {
        //                                        TableName = entityName,
        //                                        Deleted = true,
        //                                        FieldName = originalValue.Key,
        //                                        CurrentValue = null,
        //                                        OriginalValue = value,
        //                                        UserId = userId,
        //                                        SystemLogId = Guid.NewGuid(),
        //                                        ReferId = GetPrimaryKeyValue(entry, toneFinderContext),
        //                                        CreatedOn = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"))
        //                                    });
        //        }

        //        if (entry.State == EntityState.Modified)
        //        {
        //            SetCorrectOriginalValues(entry);

        //            var originalValues = entry.OriginalValues.PropertyNames.ToDictionary(pn => pn, pn => entry.OriginalValues[pn]);

        //            listAlterValue.AddRange(from original in originalValues
        //                                    let oValue = original.Value?.ToString()
        //                                    let cValue =
        //                                    (currentValues[original.Key] != null ? currentValues[original.Key].ToString() : null)
        //                                    where cValue != oValue
        //                                    select new ObjectValue.SystemLog
        //                                    {
        //                                        TableName = entityName,
        //                                        Changed = true,
        //                                        FieldName = original.Key,
        //                                        CurrentValue = cValue,
        //                                        OriginalValue = oValue,
        //                                        UserId = userId,
        //                                        SystemLogId = Guid.NewGuid(),
        //                                        ReferId = GetPrimaryKeyValue(entry, toneFinderContext),
        //                                        CreatedOn = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"))
        //                                    });
        //        }
        //    }

        //    return listAlterValue;

        //}
    }
}
