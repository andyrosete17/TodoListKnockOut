namespace TDL.Services.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using TDL.Domain;
    using TDL.Services.Interfaces;

    public class LocalDataContext : DataContext, ILocalDataContext
    {
        public DbSet<ToDoListItems> CalculatorOperations { get; set; }
        public DataContext dataContext;

        public LocalDataContext()
        {
            this.dataContext = new DataContext();
        }

        DbSet<T> ILocalDataContext.GetDbSet<T>()
        {
            return this.Set<T>();
        }

        private int SaveChanges()
        {
            int ret = -1;

            try
            {
                // Commit the changes
                ret = dataContext.SaveChanges();
            }
            catch (DbEntityValidationException dbve)
            {
                foreach (var validationErrors in dbve.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        // Show the validation exceptions which have occurred
                        Trace.TraceError("Property: {0}, Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                throw;
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Exception: {0}", e.Message);
                throw;
            }

            // Return the response
            return (ret);
        }
    }
}