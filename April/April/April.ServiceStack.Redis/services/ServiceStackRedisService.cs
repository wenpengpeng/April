// 文件名：AprilDbContext.cs
// 
// 创建标识：温朋朋 2018-08-11 11:01
// 
// 修改标识：温朋朋2018-08-11 11:01
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using April.ServiceStack.Redis.ClientManage;
using Newtonsoft.Json;
using ServiceStack.Redis;
namespace April.ServiceStack.Redis.services
{
    public class ServiceStackRedisService: IServiceStackService
    {
        /// <summary>
        ///     RedisClient
        /// </summary>
        private static readonly  IRedisClient RedisClient = RedisClientManage.GetRedisClient();

        #region String
        /// <summary>
        ///     设值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>当key已存在返回false</returns>
        public bool Set(string key, string value)
        {
            return RedisClient.Set<string>(key, value);
        }
        /// <summary>
        ///     设置key/value值并设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dt"></param>
        /// <returns>当key已存在返回false</returns>
        public bool Set(string key, string value, DateTime dt)
        {
            return RedisClient.Set(key, value, dt);
        }
        /// <summary>
        ///     设置多个key/value值
        /// </summary>
        /// <param name="dic"></param>
        public void SetAll(Dictionary<string, string> dic)
        {
            RedisClient.SetAll(dic);
        }
        /// <summary>
        ///     在原有的key对应值后面追加value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int AppendToValue(string key, string value)
        {
            return RedisClient.AppendToValue(key, value);
        }
        /// <summary>
        ///     根据key取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key)
        {
            return RedisClient.Get<string>(key);
        }
        /// <summary>
        ///     根据key取值(泛型版)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            return RedisClient.Get<T>(key);
        }
        /// <summary>
        ///     获取多个key的value值
        /// </summary>
        /// <param name="keyList"></param>
        /// <returns></returns>
        public List<string> GetAll(List<string> keyList)
        {
            return RedisClient.GetValues(keyList);
        }
        /// <summary>
        ///     获取多个key的value值(泛型版)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyList"></param>
        /// <returns></returns>
        public List<T> GetAll<T>(List<string> keyList)
        {
            return RedisClient.GetValues<T>(keyList);
        }
        /// <summary>
        ///     获取旧值设置新值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetAndSetValue(string key, string value)
        {
            return RedisClient.GetAndSetEntry(key, value);
        }
        /// <summary>
        /// 自增1，返回自增后的值
        /// </summary>
        public long IncrementValue(string key)
        {
            return RedisClient.IncrementValue(key);
        }
        /// <summary>
        /// 自增count，返回自增后的值
        /// </summary>
        public double IncrementValueBy(string key, int count)
        {
            return RedisClient.IncrementValueBy(key, count);
        }
        /// <summary>
        /// 自减1，返回自减后的值
        /// </summary>
        public long DecrementValue(string key)
        {
            return RedisClient.DecrementValue(key);
        }
        /// <summary>
        /// 自减count ，返回自减后的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public long DecrementValueBy(string key, int count)
        {
            return RedisClient.DecrementValueBy(key, count);
        }
        #endregion

        #region List（链表）

        #region 栈相关
        /// <summary>
        ///     从左侧向list中添加值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void LeftPush(string key, string value)
        {
            RedisClient.PushItemToList(key,value);
        }
        /// <summary>
        ///     从左侧添加值并设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dt"></param>
        public void LeftPush(string key, string value, DateTime dt)
        {
            RedisClient.PushItemToList(key,value);
            RedisClient.ExpireEntryAt(key, dt);
        }
        /// <summary>
        ///     从左侧向list中添加值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void RightPush(string key, string value)
        {
            RedisClient.PrependItemToList(key, value);
        }
        /// <summary>
        ///     从左侧添加值并设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dt"></param>
        public void RightPush(string key, string value, DateTime dt)
        {
            RedisClient.PrependItemToList(key, value);
            RedisClient.ExpireEntryAt(key, dt);
        }
        /// <summary>
        ///     从list中取出值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string ListPop(string key)
        {
            return RedisClient.PopItemFromList(key);
        }
        /// <summary>
        ///     从list中取出值(泛型版)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T ListPop<T>(string key)
        {
            var str= RedisClient.PopItemFromList(key);
            return JsonConvert.DeserializeObject<T>(str);
        }
        /// <summary>
        ///     添加到RedisList
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddToList(string key,string value)
        {
            RedisClient.AddItemToList(key,value);
        }
        /// <summary>
        ///     添加到RedisList并设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dt"></param>
        public void AddToList(string key, string value, DateTime dt)
        {
            RedisClient.AddItemToList(key,value);
            RedisClient.ExpireEntryAt(key, dt);
        }
        /// <summary>
        ///     添加多个值到list中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        public void AddRangeToList(string key, List<string> values)
        {
            RedisClient.AddRangeToList(key,values);
        }
        /// <summary>
        ///     添加多个值到list中并设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="dt"></param>
        public void AddRangeToList(string key, List<string> values, DateTime dt)
        {
            RedisClient.AddRangeToList(key,values);
            RedisClient.ExpireEntryAt(key, dt);
        }
        /// <summary>
        ///     获取key的所有数据集合
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> GetAllItemFromList(string key)
        {
            return RedisClient.GetAllItemsFromList(key);
        }
        /// <summary>
        ///     移除list
        /// </summary>
        /// <param name="key"></param>
        public void RemoveList(string key)
        {
            RedisClient.RemoveAllFromList(key);
        }
        #endregion

        #region 队列相关
        /// <summary>
        ///     加入一个队列值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void EnQueenOnList(string key, string value)
        {
            RedisClient.EnqueueItemOnList(key,value);
        }
        /// <summary>
        ///   队列中取出一个值  
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string DeQueenOnList(string key)
        {
            return RedisClient.DequeueItemFromList(key);
        }
        /// <summary>
        ///     队列中取出一个值（泛型版）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T DeQueenOnList<T>(string key)
        {
            var str = RedisClient.DequeueItemFromList(key);
            return !string.IsNullOrWhiteSpace(str) ? JsonConvert.DeserializeObject<T>(str) : default(T);
        }
        #endregion

        #endregion

        #region Set(无序集合)
        /// <summary>
        ///     添加到Set
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddToSet(string key, string value)
        {
            RedisClient.AddItemToSet(key,value);
        }
        /// <summary>
        ///     添加多个值到set中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        public void AddRangeToSet(string key, List<string> values)
        {
            RedisClient.AddRangeToSet(key,values);
        }
        /// <summary>
        ///     随机从set中获取一个值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetRandomFromSet(string key)
        {
            return RedisClient.GetRandomItemFromSet(key);
        }
        /// <summary>
        ///     获取set中的所有
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public HashSet<string> GetAllFromSet(string key)
        {
            return RedisClient.GetAllItemsFromSet(key);
        }
        /// <summary>
        ///     删除set中值为value的项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void RemoveItemFromSet(string key, string value)
        {
            RedisClient.RemoveItemFromSet(key,value);
        }
        /// <summary>
        ///     求并集
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public HashSet<string> GetUnionFromSets(string[] keys)
        {
            return RedisClient.GetUnionFromSets(keys);
        }
        /// <summary>
        ///     求交集
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public HashSet<string> GetIntersectFromSets(string[] keys)
        {
            return RedisClient.GetIntersectFromSets(keys);
        }
        /// <summary>
        ///     求差集
        /// </summary>
        /// <param name="fromKey"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public HashSet<string> GetDifferencesFromSet(string fromKey,string[] keys)
        {
            return RedisClient.GetDifferencesFromSet(fromKey,keys);
        }
        #endregion

        #region SortSet(有序集合)
        /// <summary>
        ///     添加值到sortSet中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool AddItemToSortedSet(string key, string value)
        {
            return RedisClient.AddItemToSortedSet(key, value);
        }
        /// <summary>
        /// 添加key/value,并设置value的分数
        /// </summary>
        public bool AddItemToSortedSet(string key, string value, double score)
        {
            return RedisClient.AddItemToSortedSet(key, value, score);
        }
        /// <summary>
        /// 为key添加values集合，values集合中每个value的分数设置为score
        /// </summary>
        public bool AddRangeToSortedSet(string key, List<string> values, double score)
        {
            return RedisClient.AddRangeToSortedSet(key, values, score);
        }
        /// <summary>
        /// 获取key的所有集合
        /// </summary>
        public List<string> GetAllItemsFromSortedSet(string key)
        {
            return RedisClient.GetAllItemsFromSortedSet(key);
        }
        /// <summary>
        /// 获取key的所有集合，倒叙输出
        /// </summary>
        public List<string> GetAllItemsFromSortedSetDesc(string key)
        {
            return RedisClient.GetAllItemsFromSortedSetDesc(key);
        }
        /// <summary>
        /// 获取所有集合，带分数
        /// </summary>
        public IDictionary<string, double> GetAllWithScoresFromSortedSet(string key)
        {
            return RedisClient.GetAllWithScoresFromSortedSet(key);
        }
        /// <summary>
        /// 获取key为value的分数
        /// </summary>
        public double GetItemScoreInSortedSet(string key, string value)
        {
            return RedisClient.GetItemScoreInSortedSet(key, value);
        }
        /// <summary>
        ///     删除key为value的数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool RemoveItemFromSortedSet(string key, string value)
        {
            return RedisClient.RemoveItemFromSortedSet(key, value);
        }
        #endregion

        #region Hash
        /// <summary>
        /// 向hashid集合中添加key/value
        /// </summary>       
        public bool SetEntryInHash(string hashid, string key, string value)
        {
            return RedisClient.SetEntryInHash(hashid, key, value);
        }
        /// <summary>
        /// 如果hashid集合中存在key/value则不添加返回false，如果不存在在添加key/value,返回true
        /// </summary>
        public bool SetEntryInHashIfNotExists(string hashid, string key, string value)
        {
            return RedisClient.SetEntryInHashIfNotExists(hashid, key, value);
        }
        /// <summary>
        /// 存储对象T t到hash集合中
        /// </summary>
        public void StoreAsHash<T>(T t)
        {
            RedisClient.StoreAsHash<T>(t);
        }
        /// <summary>
        /// 获取对象T中ID为id的数据。
        /// </summary>
        public T GetFromHash<T>(object id)
        {
            return RedisClient.GetFromHash<T>(id);
        }
        /// <summary>
        /// 获取所有hashid数据集的key/value数据集合
        /// </summary>
        public Dictionary<string, string> GetAllEntriesFromHash(string hashid)
        {
            return RedisClient.GetAllEntriesFromHash(hashid);
        }
        /// <summary>
        /// 获取hashid数据集中所有key的集合
        /// </summary>
        public List<string> GetHashKeys(string hashid)
        {
            return RedisClient.GetHashKeys(hashid);
        }
        /// <summary>
        /// 获取hashid数据集中的所有value集合
        /// </summary>
        public List<string> GetHashValues(string hashid)
        {
            return RedisClient.GetHashValues(hashid);
        }
        /// <summary>
        /// 获取hashid数据集中，key的value数据
        /// </summary>
        public string GetValueFromHash(string hashid, string key)
        {
            return RedisClient.GetValueFromHash(hashid, key);
        }
        /// <summary>
        /// 获取hashid数据集中，多个keys的value集合
        /// </summary>
        public List<string> GetValuesFromHash(string hashid, string[] keys)
        {
            return RedisClient.GetValuesFromHash(hashid, keys);
        }
        /// <summary>
        /// 删除hashid数据集中的key数据
        /// </summary>
        public bool RemoveEntryFromHash(string hashid, string key)
        {
            return RedisClient.RemoveEntryFromHash(hashid, key);
        }
        #endregion
    }
}