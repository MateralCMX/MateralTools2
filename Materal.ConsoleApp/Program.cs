using MateralTools.Base;
using MateralTools.MData;
using MateralTools.MMongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Materal.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var connStr = "mongodb://127.0.0.1:27017/?safe=true";
            MongoDBHelper db = new MongoDBHelper(connStr, "MateralTools");
            Person person = new Person();
            person.Name = "xxx";
            person.Pwd = "123";
            person.Score = "80";
            List<FilterInfo<Person>> filters = new List<FilterInfo<Person>>();
            filters.Add(new FilterInfo<Person>("Pwd", "123"));
            filters.Add(new FilterInfo<Person>("Score", "80"));
            //增加
            person = db.Insert(person);
            //查询
            List<Person> listM = db.Query(filters.ToArray());
            //修改
            person = listM[0];
            person.Name = "Materal";
            UpdateResult updateResult = db.Update(person);
            listM = db.Query(filters.ToArray());
            //删除
            DeleteResult deleteResult = db.Delete(filters.ToArray());
            listM = db.Query(filters.ToArray());
            Console.ReadKey();
        }
    }
    [MTableModel("person")]
    public class Person
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string Pwd { get; set; }
        public string Score { get; set; }
    }
}
