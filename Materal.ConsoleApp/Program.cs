using MateralTools.Base;
using MateralTools.MLinQ;
using MateralTools.MData;
using MateralTools.MMongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Materal.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<UserModel> listUserM = new List<UserModel>();
            for (int i = 0; i < 100; i++)
            {
                listUserM.Add(new UserModel
                {
                    ID = Guid.NewGuid(),
                    Score = i
                });
            }
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            FilterInfo<UserModel>[] filters =
            {
                new FilterInfo<UserModel>(x =>numbers.Contains(Convert.ToInt32(x)),3, ConditionEnum.And)
            };
            List<UserModel> resM = listUserM.Where(filters).ToList();
        }
        public class UserModel
        {
            /// <summary>
            /// 唯一标识
            /// </summary>
            public Guid ID { get; set; }
            /// <summary>
            /// 分数
            /// </summary>
            public int Score { get; set; }
        }
    }
}
