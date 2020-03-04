using System;

namespace NewLife.Caching
{
    public sealed class ProtectionFullRedis : FullRedis
    {
        #region 构造

        /// <summary>实例化增强版Redis</summary>
        public ProtectionFullRedis() : base()
        {
        }

        /// <summary>实例化增强版Redis</summary>
        /// <param name="server"></param>
        /// <param name="password"></param>
        /// <param name="db"></param>
        public ProtectionFullRedis(String server, String password, Int32 db) : base(server, password, db)
        {
        }

        #endregion

        /// <summary>获取字符串区间</summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public String[] LRANGE(String key, Int64 start, Int64 end) =>
            Execute(key, r => r.Execute<String[]>("LRANGE", key, start, end));
    }
}