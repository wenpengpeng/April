// 文件名：AprilDbContext.cs
// 
// 创建标识：温朋朋 2018-08-11 11:01
// 
// 修改标识：温朋朋2018-08-11 11:01
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace April.ServiceStack.Redis.services
{
    public interface IServiceStackService
    {
        #region String
        /// <summary>
        ///     设值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Set(string key, string value);
        /// <summary>
        ///     设置key/value值并设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        bool Set(string key, string value, DateTime dt);
        /// <summary>
        ///     设置多个key/value值
        /// </summary>
        /// <param name="dic"></param>
        void SetAll(Dictionary<string, string> dic);
        /// <summary>
        ///     在原有的key对应值后面追加value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        int AppendToValue(string key, string value);
        /// <summary>
        ///     根据key取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string Get(string key);
        /// <summary>
        ///     根据key取值(泛型版)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);
        /// <summary>
        ///     获取多个key的value值
        /// </summary>
        /// <param name="keyList"></param>
        /// <returns></returns>
        List<string> GetAll(List<string> keyList);
        /// <summary>
        ///     获取多个key的value值(泛型版)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyList"></param>
        /// <returns></returns>
        List<T> GetAll<T>(List<string> keyList);
        /// <summary>
        ///     获取旧值设置新值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        string GetAndSetValue(string key, string value);
        /// <summary>
        ///     自增1，返回自增后的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        long IncrementValue(string key);
        /// <summary>
        ///     自增count，返回自增后的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        double IncrementValueBy(string key, int count);
        /// <summary>
        ///     自减1，返回自减后的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        long DecrementValue(string key);
        /// <summary>
        ///     自减count ，返回自减后的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        long DecrementValueBy(string key, int count);
        #endregion

        #region List(链表)

        /// <summary>
        ///     从左侧向list中添加值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void LeftPush(string key, string value);

        /// <summary>
        ///     从左侧添加值并设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dt"></param>
        void LeftPush(string key, string value, DateTime dt);

        /// <summary>
        ///     从左侧向list中添加值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void RightPush(string key, string value);

        /// <summary>
        ///     从左侧添加值并设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dt"></param>
        void RightPush(string key, string value, DateTime dt);

        /// <summary>
        ///     从list中取出值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string ListPop(string key);

        /// <summary>
        ///     从list中取出值(泛型版)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T ListPop<T>(string key);

        /// <summary>
        ///     添加到RedisList
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void AddToList(string key, string value);

        /// <summary>
        ///     添加到RedisList并设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dt"></param>
        void AddToList(string key, string value, DateTime dt);

        /// <summary>
        ///     添加多个值到list中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        void AddRangeToList(string key, List<string> values);

        /// <summary>
        ///     添加多个值到list中并设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="dt"></param>
        void AddRangeToList(string key, List<string> values, DateTime dt);

        /// <summary>
        ///     获取key的所有数据集合
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        List<string> GetAllItemFromList(string key);

        /// <summary>
        ///     移除list
        /// </summary>
        /// <param name="key"></param>
        void RemoveList(string key);


        /// <summary>
        ///     加入一个队列值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void EnQueenOnList(string key, string value);

        /// <summary>
        ///   队列中取出一个值  
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string DeQueenOnList(string key);

        /// <summary>
        ///     队列中取出一个值（泛型版）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T DeQueenOnList<T>(string key);

        #endregion

        #region Set(无序集合)

        /// <summary>
        ///     添加到Set
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void AddToSet(string key, string value);

        /// <summary>
        ///     添加多个值到set中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        void AddRangeToSet(string key, List<string> values);

        /// <summary>
        ///     随机从set中获取一个值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetRandomFromSet(string key);

        /// <summary>
        ///     获取set中的所有
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        HashSet<string> GetAllFromSet(string key);

        /// <summary>
        ///     删除set中值为value的项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void RemoveItemFromSet(string key, string value);

        /// <summary>
        ///     求并集
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        HashSet<string> GetUnionFromSets(string[] keys);

        /// <summary>
        ///     求交集
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        HashSet<string> GetIntersectFromSets(string[] keys);

        /// <summary>
        ///     求差集
        /// </summary>
        /// <param name="fromKey"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        HashSet<string> GetDifferencesFromSet(string fromKey, string[] keys);

        #endregion

        #region SortSet(有序集合)

        /// <summary>
        ///     添加值到sortSet中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool AddItemToSortedSet(string key, string value);

        /// <summary>
        /// 添加key/value,并设置value的分数
        /// </summary>
        bool AddItemToSortedSet(string key, string value, double score);

        /// <summary>
        /// 为key添加values集合，values集合中每个value的分数设置为score
        /// </summary>
        bool AddRangeToSortedSet(string key, List<string> values, double score);

        /// <summary>
        /// 获取key的所有集合
        /// </summary>
        List<string> GetAllItemsFromSortedSet(string key);

        /// <summary>
        /// 获取key的所有集合，倒叙输出
        /// </summary>
        List<string> GetAllItemsFromSortedSetDesc(string key);

        /// <summary>
        /// 获取所有集合，带分数
        /// </summary>
        IDictionary<string, double> GetAllWithScoresFromSortedSet(string key);

        /// <summary>
        /// 获取key为value的分数
        /// </summary>
        double GetItemScoreInSortedSet(string key, string value);

        /// <summary>
        ///     删除key为value的数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool RemoveItemFromSortedSet(string key, string value);

        #endregion

        #region Hash

        /// <summary>
        /// 向hashid集合中添加key/value
        /// </summary>       
        bool SetEntryInHash(string hashid, string key, string value);

        /// <summary>
        /// 如果hashid集合中存在key/value则不添加返回false，如果不存在在添加key/value,返回true
        /// </summary>
        bool SetEntryInHashIfNotExists(string hashid, string key, string value);

        /// <summary>
        /// 存储对象T t到hash集合中
        /// </summary>
        void StoreAsHash<T>(T t);

        /// <summary>
        /// 获取对象T中ID为id的数据。
        /// </summary>
        T GetFromHash<T>(object id);

        /// <summary>
        /// 获取所有hashid数据集的key/value数据集合
        /// </summary>
        Dictionary<string, string> GetAllEntriesFromHash(string hashid);

        /// <summary>
        /// 获取hashid数据集中所有key的集合
        /// </summary>
        List<string> GetHashKeys(string hashid);

        /// <summary>
        /// 获取hashid数据集中的所有value集合
        /// </summary>
        List<string> GetHashValues(string hashid);

        /// <summary>
        /// 获取hashid数据集中，key的value数据
        /// </summary>
        string GetValueFromHash(string hashid, string key);

        /// <summary>
        /// 获取hashid数据集中，多个keys的value集合
        /// </summary>
        List<string> GetValuesFromHash(string hashid, string[] keys);

        /// <summary>
        /// 删除hashid数据集中的key数据
        /// </summary>
        bool RemoveEntryFromHash(string hashid, string key);

        #endregion
    }
}