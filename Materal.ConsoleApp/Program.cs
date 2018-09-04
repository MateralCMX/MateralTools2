using MateralTools.Base;
using MateralTools.MMongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Materal.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoDBHelper DB = new MongoDBHelper("mongodb://127.0.0.1:27017", "MateralTools");
            MT_User userFromDB = DB.Insert(new MT_User
            {
                Name = "Materal"
            });
            FilterInfo<MT_User>[] filters = new FilterInfo<MT_User>[]
            {
                new FilterInfo<MT_User>(nameof(MT_User.Name), "Materal")
            };
            userFromDB = DB.QueryOne(filters);
            DB.DeleteOne(filters);
            userFromDB = DB.QueryOne(filters);
            Console.ReadKey();
        }
    }
    public class T_User
    {
        [BsonId]
        public Guid ID { get; set; }
        public string Name { get; set; }
    }
    public class MT_User:T_User
    {
    }
}
