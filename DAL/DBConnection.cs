using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class DBConnection
    {
        public DBConnection() { }
        public List<T> GetDbSet<T>() where T : class
        {
            using (Project_FinishEntities projectFinishEntity = new Project_FinishEntities())
            {
                return projectFinishEntity.Set<T>().ToList();
            }
        }
        public enum ExecuteActions
        {
            Insert,
            Update,
            Delete
        }
        public void Execute<T>(T entity, ExecuteActions exAction) where T : class
        {
            using (Project_FinishEntities projectFinishEntity = new Project_FinishEntities())
            {
                var model = projectFinishEntity.Set<T>();
                switch (exAction)
                {
                    case ExecuteActions.Insert:
                        model.Add(entity);
                        break;
                    case ExecuteActions.Update:
                        model.Attach(entity);
                        projectFinishEntity.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                        break;
                    case ExecuteActions.Delete:
                        model.Attach(entity);
                        projectFinishEntity.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                        break;
                    default:
                        break;
                }
                projectFinishEntity.SaveChanges();

            }
        }
    }
}

