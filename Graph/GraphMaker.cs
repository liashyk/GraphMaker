using System;
using System.Collections.Generic;
using System.Text;

namespace Graph
{
    internal class GraphMaker
    {
        //conatin array of points in graph
        private LinkedList<GraphPoint> points;

        //contain current number of point
        private int currentNumber;

        public GraphMaker()
        {
            points = new LinkedList<GraphPoint>();
            currentNumber = 1;
        }

        //find each point's minimal path by Deyxtra Method
        public void DeyxtraMethod()
        {
            try
            {
                List<int> excludedNumbers = new List<int>();
                while (excludedNumbers.Count < points.Count)
                {
                    GraphPoint point = SmallestPath(excludedNumbers);
                    excludedNumbers.Add(point.number);
                    foreach (Arc arc in point.arcExits)
                    {
                        DetermineEndPointPath(arc);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
        }

        public double[,] FloydMethod()
        {
            double[,] dMatrix = makeDMatrix();
            for (int i = 0; i < dMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < dMatrix.GetLength(0); j++)
                {
                    if (j != i && dMatrix[j, i] != -1)
                        for (int d = 0; d < dMatrix.GetLength(1); d++)
                        {
                            if (dMatrix[i, d] != -1 && d!=i)
                            {
                                double buffer = dMatrix[j, i] + dMatrix[i, d];
                                if (dMatrix[j, d] > buffer || dMatrix[j, d] == -1)
                                {
                                    dMatrix[j, d] = buffer;
                                }
                            }
                        }
                }
            }
            return dMatrix;
        }

        private double[,] makeDMatrix()
        {
            double[,] matrix = new double[points.Count, points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                for (int j = 0; j < points.Count; j++)
                {
                    matrix[i, j] = -1;
                    if (i == j)
                    {
                        matrix[i, j] = 0;
                    }
                }
                var currentPoint = SearchPoint(i + 1);
                for (int k = 0; k < currentPoint.arcExits.Count; k++)
                {
                    matrix[i, currentPoint.arcExits[k].endPoint.number - 1] = currentPoint.arcExits[k].range;
                }
            }
            return matrix;
        }

        //find path of poind in end of arc
        private void DetermineEndPointPath(Arc arc)
        {
            if (arc.startPoint.path != -1)
            {
                double newPath;
                newPath = arc.range + arc.startPoint.path;
                if (arc.endPoint.path > newPath || arc.endPoint.path == -1)
                {
                    arc.endPoint.path = newPath;
                }
            }
        }

        //find point with the smallest path, except points that contain in excludedNumbers
        private GraphPoint SmallestPath(List<int> excludedNumbers)
        {
            GraphPoint minPathPoint = FindWithPath();
            foreach (GraphPoint p in points)
            {
                if ((minPathPoint == null || p.path < minPathPoint.path) && p.path >= 0 && !excludedNumbers.Contains(p.number))
                {
                    minPathPoint = p;
                }
            }
            return minPathPoint;
        }

        //return point,which path isn't equal -1
        private GraphPoint FindWithPath()
        {
            foreach (GraphPoint p in points)
            {
                if (p.path >= 0) return p;
            }
            throw new Exception("All points without path");
        }

        //add new point to graph with path in parametr.
        public void AddPoint(double path = -1)
        {
            points.AddLast(new GraphPoint(currentNumber, path));
            currentNumber++;
        }


        public void AddExitArc(int number1, int number2, double range = -1)
        {
            var point1 = SearchPoint(number1);
            var point2 = SearchPoint(number2);
            point1.AddExitArc(point2, range);
        }

        private GraphPoint SearchPoint(int number)
        {
            foreach (GraphPoint point in points)
            {
                if (point.number == number) return point;
            }
            return null;
        }

        public void DrawGraph()
        {
            foreach (GraphPoint point in points)
            {
                System.Console.WriteLine(point);
            }
        }

        class GraphPoint
        {
            public double path;
            public List<Arc> arcExits;
            public int number;

            public GraphPoint(int number, double path)
            {
                this.number = number;
                this.path = path;
                arcExits = new List<Arc>();
            }

            public void AddExitArc(GraphPoint point, double range = -1)
            {
                arcExits.Add(new Arc(this, point, range));
            }

            public override string ToString()
            {
                StringBuilder stringBuilder = new StringBuilder();
                String pathValue;
                if (path == -1)
                {
                    pathValue = "Infinty";
                }
                else pathValue = path.ToString();

                stringBuilder.Append("#" + number + " path= " + pathValue + " ");
                foreach (Arc point in arcExits)
                {
                    stringBuilder.Append("( " + point + " ),");
                }
                return stringBuilder.ToString();
            }
        }


        class Arc
        {
            public Arc(GraphPoint startPoint, GraphPoint endPoint, double range = 1)
            {
                this.startPoint = startPoint;
                this.endPoint = endPoint;
                this.range = range;
            }



            public GraphPoint startPoint;
            public GraphPoint endPoint;
            public double range;

            public override string ToString()
            {
                return "#" + startPoint.number + "-->#" + endPoint.number + " , range: " + range;
            }
        }
    }
}
