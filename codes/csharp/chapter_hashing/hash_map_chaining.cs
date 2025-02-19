﻿/**
* File: hash_map_chaining.cs
* Created Time: 2023-06-26
* Author: hpstory (hpstory1024@163.com)
*/

namespace hello_algo.chapter_hashing;

/* 链式地址哈希表 */
class HashMapChaining {
    int size; // 键值对数量
    int capacity; // 哈希表容量
    double loadThres; // 触发扩容的负载因子阈值
    int extendRatio; // 扩容倍数
    List<List<Pair>> buckets; // 桶数组

    /* 构造方法 */
    public HashMapChaining() {
        size = 0;
        capacity = 4;
        loadThres = 2 / 3.0;
        extendRatio = 2;
        buckets = new List<List<Pair>>(capacity);
        for (int i = 0; i < capacity; i++) {
            buckets.Add(new List<Pair>());
        }
    }

    /* 哈希函数 */
    private int hashFunc(int key) {
        return key % capacity;
    }

    /* 负载因子 */
    private double loadFactor() {
        return (double)size / capacity;
    }

    /* 查询操作 */
    public string get(int key) {
        int index = hashFunc(key);
        // 遍历桶，若找到 key 则返回对应 val
        foreach (Pair pair in buckets[index]) {
            if (pair.key == key) {
                return pair.val;
            }
        }
        // 若未找到 key 则返回 null
        return null;
    }

    /* 添加操作 */
    public void put(int key, string val) {
        // 当负载因子超过阈值时，执行扩容
        if (loadFactor() > loadThres) {
            extend();
        }
        int index = hashFunc(key);
        // 遍历桶，若遇到指定 key ，则更新对应 val 并返回
        foreach (Pair pair in buckets[index]) {
            if (pair.key == key) {
                pair.val = val;
                return;
            }
        }
        // 若无该 key ，则将键值对添加至尾部
        buckets[index].Add(new Pair(key, val));
        size++;
    }

    /* 删除操作 */
    public void remove(int key) {
        int index = hashFunc(key);
        // 遍历桶，从中删除键值对
        foreach (Pair pair in buckets[index].ToList()) { 
            if (pair.key == key) {
                buckets[index].Remove(pair);
            }
        }
        size--;   
    }

    /* 扩容哈希表 */
    private void extend() {
        // 暂存原哈希表
        List<List<Pair>> bucketsTmp = buckets;
        // 初始化扩容后的新哈希表
        capacity *= extendRatio;
        buckets = new List<List<Pair>>(capacity);
        for (int i = 0; i < capacity; i++) {
            buckets.Add(new List<Pair>());
        }
        size = 0;
        // 将键值对从原哈希表搬运至新哈希表
        foreach (List<Pair> bucket in bucketsTmp) {
            foreach (Pair pair in bucket) {
                put(pair.key, pair.val);
            }
        }
    }

    /* 打印哈希表 */
    public void print() {
        foreach (List<Pair> bucket in buckets) {
            List<string> res = new List<string>();
            foreach (Pair pair in bucket) {
                res.Add(pair.key + " -> " + pair.val);
            }
            foreach (string kv in res) {
                Console.WriteLine(kv);
            }
        }
    }
}

public class hash_map_chaining {
    [Test]
    public void Test() {
        /* 初始化哈希表 */
        HashMapChaining map = new HashMapChaining();

        /* 添加操作 */
        // 在哈希表中添加键值对 (key, value)
        map.put(12836, "小哈");
        map.put(15937, "小啰");
        map.put(16750, "小算");
        map.put(13276, "小法");
        map.put(10583, "小鸭");
        Console.WriteLine("\n添加完成后，哈希表为\nKey -> Value");
        map.print();

        /* 查询操作 */
        // 向哈希表输入键 key ，得到值 value
        string name = map.get(13276);
        Console.WriteLine("\n输入学号 13276 ，查询到姓名 " + name);

        /* 删除操作 */
        // 在哈希表中删除键值对 (key, value)
        map.remove(12836);
        Console.WriteLine("\n删除 12836 后，哈希表为\nKey -> Value");
        map.print();
    }
}