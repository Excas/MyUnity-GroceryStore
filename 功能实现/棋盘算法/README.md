# 棋盘算法
##需求
一X*X的棋盘，判断一个T字形 判断棋盘的哪些位置能放置
##实现

	static int[][] chessboard = new int[][] {
        new int[] {1, 1, 1},
        new int[] {1, 1, 1},
        new int[] {1, 1, 1},
        new int[] {0, 1, 0}
    };

    static int[][] polygon = new int[][] {
        new int[] {1, 1, 0},
        new int[] {1, 0, 0}
    };
    
    static bool calc(int i, int j, int[][] chessboard, int k, int l, int[][] polygon) 
    {
        int newI = i + k;
        int newJ = j + l;
        int polygonPoint = polygon[k][l];

        // If the position on the chessboard is empty, no comparison is needed, return true
        if (polygonPoint == 0) {
            return true;
        }

        // Check if the height exceeds the boundary of the chessboard
        if (newI >= chessboard.Length) {
            Console.WriteLine("{0} {1} 高度超出边界", i, j);
            return false;
        }

        int[] row = chessboard[newI];

        // Check if the length exceeds the boundary of the chessboard
        if (newJ >= row.Length) {
            Console.WriteLine("{0} {1} 长度超出边界", i, j);
            return false;
        }

        int point = row[newJ];

        // If the position on the chessboard is not empty, return false
        if (!(point == polygonPoint)) {
            Console.WriteLine("{0} {1} 该处不允许放置", i, j);
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
            
            if (match) { points.Add(new int[] {i, j}); }
        });

        return points;
    }

    public static void Main()
    {
        List<int[]> points = getMatchPoints(chessboard, polygon);

        // Positions (0,0), (0,1), (1,0), (1,1), (2,0) are matched
        foreach (int[] point in points) {
            Console.WriteLine("({0},{1})", point[0], point[1]);
        }
    }