using LineDraw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineDraw.Misc
{
    /// <summary>
    /// This class provides facilities for working with graphs.
    /// </summary>
    public class GraphTools<T> where T : Node, new()
    {

        /// <summary>
        /// Create a matrix of <see cref="T"/> based on submitted height and width.
        /// The matrix has the dimensions [width][height]
        /// </summary>
        /// <param name="height">The height of the matrix.</param>
        /// <param name="width">The widht of the matrix </param>
        /// <returns></returns>
        public static T[][] CreateGraph(int height, int width)
        {
            T[][] graph = new T[width][];
            for (int i = 0; i < width; i++)
            {
                graph[i] = new T[height];
                for (int j = 0; j < height; j++)
                {
                    graph[i][j] = new T() { X = i, Y = j };
                }
            }
            return graph;
        }

        /// <summary>
        /// Prepare the submitted graph for search traversal by
        /// setting Distance to max value and Parent to null.
        /// </summary>
        /// <param name="graph">The graph to reset.</param>
        public static void SearchReset(T[][] graph)
        {
            foreach (T[] nodes in graph)
                foreach (T node in nodes)
                {
                    node.Parent = null;
                    node.Distance = int.MaxValue;
                }
        }

        /// <summary>
        /// Get the adjacent elements of the submitted element in
        /// the submitted matrix.
        /// </summary>
        /// <param name="graph">Matrix in which to look for adjacent elements in.</param>
        /// <param name="node">Element to search around.</param>
        /// <returns>An array of nodes adjacent to submitted element.</returns>
        public static T[] GetAdjacentElements(T[][] graph, T element)
        {
            List<T> nodes = new List<T>();

            //Vertical and horizontal elements
            // Check if the element above the submitted element is a valid index.
            if (element.Y - 1 >= 0)
                nodes.Add(graph[element.X][element.Y - 1]);

            // Check if the element to the right of the submitted element is a valid index.
            if (element.X + 1 < graph.Length)
                nodes.Add(graph[element.X + 1][element.Y]);

            // Check if the element below the submitted element is a valid index.
            if (element.Y + 1 < graph[element.X].Length)
                nodes.Add(graph[element.X][element.Y + 1]);

            // Check if the element to the left of the submitted element is a valid index.
            if (element.X - 1 >= 0)
                nodes.Add(graph[element.X - 1][element.Y]);

            // Diagonal elements
            // Check if the element above to the left the submitted element is a valid index.
            if (element.Y - 1 >= 0 && element.X - 1 >= 0)
                // Check if the top or left elements are occupied to avoid intersecting diagonal lines
                if (!graph[element.X - 1][element.Y].Occupied && !graph[element.X][element.Y - 1].Occupied)
                    nodes.Add(graph[element.X - 1][element.Y - 1]);

            // Check if the element above to the right of the submitted element is a valid index.
            if (element.X + 1 < graph.Length && element.Y - 1 >= 0)
                // Check if the top or right elements are occupied to avoid intersecting diagonal lines
                if (!graph[element.X][element.Y - 1].Occupied && !graph[element.X + 1][element.Y].Occupied)
                    nodes.Add(graph[element.X + 1][element.Y - 1]);

            // Check if the element below to the right of the submitted element is a valid index.
            if (element.Y + 1 < graph[element.X].Length && element.X + 1 < graph.Length)
                // Check if the below or right elements are occupied to avoid intersecting diagonal lines
                if (!graph[element.X][element.Y + 1].Occupied && !graph[element.X + 1][element.Y].Occupied)
                    nodes.Add(graph[element.X + 1][element.Y + 1]);

            // Check if the element below and to the left of the submitted element is a valid index.
            if (element.X - 1 >= 0 && element.Y + 1 < graph[element.X].Length)
                // Check if the below or left elements are occupied to avoid intersecting diagonal lines
                if (!graph[element.X][element.Y + 1].Occupied && !graph[element.X - 1][element.Y].Occupied)
                    nodes.Add(graph[element.X - 1][element.Y + 1]);

            return nodes.ToArray();
        }

        /// <summary>
        /// Calculate the distance between two nodes.
        /// </summary>
        /// <param name="a">Node a</param>
        /// <param name="b">Node b</param>
        /// <returns>Distance between submitted nodes.</returns>
        public static double Distance(T a, T b)
        {
            double width = Math.Abs(a.X - b.X);
            double height = Math.Abs(a.Y - b.Y);
            return Math.Sqrt(width * width + height * height);
        }
    }
}
