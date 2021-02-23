using CSRedis;

namespace GIS.Authority.Common
{
    public class RedisInstanceHelper
    {
        public static RedisInstanceHelper RedisInstance;

        private static CSRedisClient csClient;

        public double defaulTimeHour = 0.5;
        public static RedisInstanceHelper GetInstance()
        {
            if (RedisInstance == null)
            {
                RedisInstance = new RedisInstanceHelper();
                csClient = new CSRedisClient(ConfigurationData.RedisConnectionStrings);
                RedisHelper.Initialization(csClient);
            }
            return RedisInstance;
        }

        #region 字符串

        public bool SetKeyString(string key, object value, int expireSecond = -1)
        {
            return csClient.Set(key, value, expireSecond);
        }

        public bool RenameKey(string key, string  newKey,int second)
        {
            return csClient.Rename(key, newKey)&& csClient.Expire(newKey,second);
        }

        public string GetKeyString(string key)
        {
            return csClient.Get(key);
        }

        public long GetKeyTtl(string key)
        {
            return csClient.Ttl(key);
        }

        public bool DeleteKey(string key)
        {
            csClient.Del(key);
            return true;
        }

        #endregion 字符串



        #region 哈希

        public bool SetKeyHash(string key, string field, string value)
        {
            return csClient.HSet(key, field, value);
        }

        public bool SetKeyHashArray(string key, params object[] array)
        {
            return csClient.HMSet(key, array);
        }

        public string GetKeyHash(string key, string field)
        {
            return csClient.HGet(key, field);
        }

        public string[] GetKeyHashArray(string key, string[] field)
        {
            return csClient.HMGet(key, field);
        }

        #endregion 哈希


        #region List




        #endregion
    }
}