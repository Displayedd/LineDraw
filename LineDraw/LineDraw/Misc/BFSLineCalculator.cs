using LineDraw.Interfaces;
using LineDraw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineDraw.Misc
{
    /// <summary>
    /// This class provides facility for calculating lines
    /// between two points.
    /// </summary>
    public class BFSLineCalculator : ILineCalculator
    {
        /// <summary>
        /// Calculates the shortest path through the submitted graph
        /// from the submitted start and end points. Throws exception
        /// if a path cannot be computed. The implementation uses the
        /// breadth-first search algorithm to traverse the graph.
        /// </summary>
        /// <param name="graph">Graph to traverse.</param>
        /// <param name="startPoint">Start point of path.</param>
        /// <param name="endPoint">End point of path.</param>
        /// <returns></returns>
        public Point[] CalculateLine(Node[][] graph, Point startPoint, Point endPoint)
        {
            // Prepare the graph for searching.
            GraphTools<Node>.SearchReset(graph);

            // Queue that will hold the next nodes to be processed.
            Queue<Node> queue = new Queue<Node>();

            Node startNode = graph[startPoint.X][startPoint.Y];
            // Set the start node to be parent of itself to signify it as start point.
            startNode.Parent = graph[startPoint.X][startPoint.Y];
            // Set distance of start node to 0
            startNode.Distance = 0;
            // Enqueue the start point unless it is allready occupied.
            if (!startNode.Occupied)
                queue.Enqueue(graph[startPoint.X][startPoint.Y]);

            while (queue.Count > 0)
            {
                // Next node in the queue.
                Node current = queue.Dequeue();
                // Get nodes adjacent to the node being processed.
                Node[] adjacent = GraphTools<Node>.GetAdjacentElements(graph, current);

                // For each adjacent node check that it's not occupied by another line
                // and it has not been visisted by a shorter path.
                foreach (Node node in adjacent)
                {
                    if (!node.Occupied && (current.Distance + GraphTools<Node>.Distance(current, node)) < node.Distance)
                    {
                        // Update the adjacent node and enqueue it.
                        node.Parent = current;
                        node.Distance = current.Distance + GraphTools<Node>.Distance(current, node);
                        queue.Enqueue(node);
                    }
                }
            }

            // Use the helper function to find the computed path and return it.
            return GetPath(graph[endPoint.X][endPoint.Y]);
        }

        /// <summary>
        /// Get the path from a traversed graph.
        /// </summary>
        /// <param name="endPoint">Point where path ends.</param>
        /// <returns>An array of <see cref="Point"/>.</returns>
        private Point[] GetPath(Node endPoint)
        {
            List<Point> path = new List<Point>();
            Node current = endPoint;

            // Add nodes to the path until we reach the nodes which has itself as parent
            // which signifies it is the start point.
            while (current.Parent != current)
            {                
                path.Add(new Point { X = current.X, Y = current.Y });
                current = current.Parent;
            }

            // Add the last point
            path.Add(new Point { X = current.X, Y = current.Y });

            // Reverse the order since we began at the end point.
            path.Reverse();

            return path.ToArray();
        }

    }
}
