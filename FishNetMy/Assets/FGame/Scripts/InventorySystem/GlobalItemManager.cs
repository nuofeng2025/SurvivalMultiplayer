using System.Collections;
using System.Collections.Generic;
using System;
public static class GlobalItemManager
{
    private static readonly Random random = new Random();
    private static readonly object lockObj = new object();
    private static HashSet<int> usedIds = new HashSet<int>();

    public static int GenerateId()
    {
        lock (lockObj)
        {
            int id;
            do
            {
                // 生成 1,000,000 到 2,147,483,647 之间的随机数
                id = random.Next(1_000_000, int.MaxValue);
            } while (usedIds.Contains(id)); // 确保不重复

            usedIds.Add(id);
            return id;
        }
    }

    public static void ReleaseId(int id)
    {
        lock (lockObj)
        {
            usedIds.Remove(id);
        }
    }
}
