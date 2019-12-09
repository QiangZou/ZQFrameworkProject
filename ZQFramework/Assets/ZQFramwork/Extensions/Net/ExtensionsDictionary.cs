using System.Collections.Generic;

public static class ExtensionsDictionary
{
    /// <summary>
    /// 添加(安全) 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="self"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool AddSafety<TKey, TValue>(this Dictionary<TKey, TValue> self, TKey key, TValue value)
    {
        if (self.ContainsKey(key))
        {
            return false;
        }

        self.Add(key, value);
        return true;
    }

    /// <summary>
    /// 更新(安全)
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="self"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool UpdateSafety<TKey, TValue>(this Dictionary<TKey, TValue> self, TKey key, TValue value)
    {
        if (!self.ContainsKey(key))
        {
            return false;
        }

        self[key] = value;
        return true;
    }

    /// <summary>
    /// 添加或更新
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="self"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> self, TKey key, TValue value)
    {
        if (self.ContainsKey(key))
        {
            self[key] = value;
        }
        else
        {
            self.Add(key, value);
        }
    }

}
