using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Workers;

public static class RandomGenerator
{
    private static System.Random random = new();

    private static Dictionary<WorkerColor, int> colors = new ()
    {
        { WorkerColor.Red, 0 },
        { WorkerColor.Green, 0 },
        { WorkerColor.Blue, 0 },
        { WorkerColor.Purple, 0},
    };
    const int MAX_ALLOWED_AHEAD = 2;
    
    public static void Init(int iv = Int32.MinValue)
    {
        random = new System.Random(iv == Int32.MinValue ? System.Environment.TickCount : iv);
    }
    
    public static WorkerColor GetNextWorkerColor()
    {
        (int, WorkerColor) min = (Int32.MaxValue,0);
        (int, WorkerColor) max = (Int32.MinValue,0);

        foreach (var pair in colors)
        {
            min = pair.Value < min.Item1 ? (pair.Value,pair.Key) : min;
            max = pair.Value > max.Item1 ? (pair.Value,pair.Key) : max;
        }

        WorkerColor nextColor;
        if (max.Item1 - min.Item1 > MAX_ALLOWED_AHEAD) nextColor = min.Item2;
        else nextColor = (WorkerColor)(random.Next() % 4);
        
        colors[nextColor]++;
        return nextColor;
    }
}
