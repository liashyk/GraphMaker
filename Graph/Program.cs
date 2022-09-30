using System;

namespace Graph
{
    internal class Program
    {
        private static void ShowMatrix<T>(T[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write("{0,3}", matrix[i, j]);
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            GraphMaker graphMaker = new GraphMaker();
            for (int i = 1; i <=6; i++)
            {
                graphMaker.AddPoint();
            }

            //fill graph by arcs
            graphMaker.AddExitArc(1, 2, 10);
            graphMaker.AddExitArc(1, 4, 8);
            graphMaker.AddExitArc(1, 3, 18);
            graphMaker.AddExitArc(2, 1, 10);
            graphMaker.AddExitArc(2, 3, 16);
            graphMaker.AddExitArc(2, 4, 9);
            graphMaker.AddExitArc(2, 5, 21);
            graphMaker.AddExitArc(3, 2, 16);
            graphMaker.AddExitArc(3, 6, 15);
            graphMaker.AddExitArc(4, 1, 7);
            graphMaker.AddExitArc(4, 2, 9);
            graphMaker.AddExitArc(4, 6, 12);
            graphMaker.AddExitArc(5, 6, 23);
            graphMaker.AddExitArc(6, 3, 15);
            graphMaker.AddExitArc(6, 5, 23);

            //It is graph from Task7's example
            graphMaker.DrawGraph();

            Console.WriteLine("\nD Matrix after all iterations:\n");
            ShowMatrix(graphMaker.FloydMethod());
            Console.ReadKey();
        }
    }
}
