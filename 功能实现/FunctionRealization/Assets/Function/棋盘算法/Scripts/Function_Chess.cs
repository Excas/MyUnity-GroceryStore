using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 需求--  一个9X9的棋盘  判断T字形能放在哪个位置
/// </summary>
public class Function_Chess : MonoBehaviour
{
    static int[][] chessboard = new int[][]
    {
        new int[] {1, 1, 1},
        new int[] {1, 1, 1},
        new int[] {1, 1, 1},
        new int[] {0, 1, 0}
    };

    static int[][] polygon = new int[][]
    {
        new int[] {1, 1, 0},
        new int[] {1, 0, 0}
    };

    static bool calc(int i, int j, int[][] chessboard, int k, int l, int[][] polygon)
    {
        int newI = i + k;
        int newJ = j + l;
        int polygonPoint = polygon[k][l];

        // If the position on the chessboard is empty, no comparison is needed, return true
        if (polygonPoint == 0)
        {
            return true;
        }

        // Check if the height exceeds the boundary of the chessboard
        if (newI >= chessboard.Length)
        {
            Debug.Log($"{i} {j} 高度超出边界");
            return false;
        }

        int[] row = chessboard[newI];

        // Check if the length exceeds the boundary of the chessboard
        if (newJ >= row.Length)
        {
            Debug.Log($"{i} {j} 长度超出边界");
            return false;
        }

        int point = row[newJ];

        // If the position on the chessboard is not empty, return false
        if (!(point == polygonPoint))
        {
            Debug.Log($"{i} {j} 该处不允许放置");
            return false;
        }

        // If all conditions are met, return true
        return true;
    }

    static void doubleErgodic(int[][] array, Action<int, int> callback)
    {
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < array[i].Length; j++)
            {
                callback(i, j);
            }
        }
    }

    static List<int[]> getMatchPoints(int[][] chessboard, int[][] polygon)
    {
        List<int[]> points = new List<int[]>();

        doubleErgodic(chessboard, (i, j) =>
        {
            bool match = true;
            doubleErgodic(polygon, (k, l) => { match = match && calc(i, j, chessboard, k, l, polygon); });

            if (match)
            {
                points.Add(new int[] {i, j});
            }
        });

        return points;
    }

    private void Start()
    {
        List<int[]> points = getMatchPoints(chessboard, polygon);

        // Positions (0,0), (0,1), (1,0), (1,1), (2,0) are matched
        foreach (int[] point in points)
        {
            Debug.Log($"({point[0]},{point[1]})");
        }
    }
}
