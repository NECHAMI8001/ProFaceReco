using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BL.Classes
{
    public class FaceRecognitionBL
    {


        DBConnection dbCon;
        public FaceRecognitionBL()
        {
            dbCon = new DBConnection();
        }
        public enum Result
        {
            IncorrectDetails,
            NotFound,
            Found
        }
        public List<T> GetDbSet<T>() where T : class
        {
            return dbCon.GetDbSet<T>();
        }
        public void AddToDB<T>(T entity) where T : class
        {
            dbCon.Execute<T>(entity, DBConnection.ExecuteActions.Insert);
        }
        public void DeleteToDB<T>(T entity) where T : class
        {
            dbCon.Execute<T>(entity, DBConnection.ExecuteActions.Delete);
        }
        public void UpdateToDB<T>(T entity) where T : class
        {
            dbCon.Execute<T>(entity, DBConnection.ExecuteActions.Update);
        }
    }
}

