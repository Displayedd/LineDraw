using C5;
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
    public class DijkstraLineCalculator : ILineCalculator
    {
        /// <summary>
        /// Calculates the shortest path through the submitted graph
        /// from the submitted start and end points. Throws exception
        /// if a path cannot be computed. The implementation uses
        /// Dijkstra's algorithm to find the shortest path.
        /// </summary>
        /// <param name="graph">Graph to traverse.</param>
        /// <param name="startPoint">Start point of path.</param>
        /// <param name="endPoint">End point of path.</param>
        /// <returns></returns>
        public Point[] CalculateLine(Node[][] graph, Point startPoint, Point endPoint)
        {
            bool found = false;

            // This implementation requires the nodes to be PriorityQueueNodes.
            PriorityQueueNode[][] prioGraph = graph as PriorityQueueNode[][];

            // Prepare the nodes for searching
            GraphTools<Node>.SearchReset(graph);

            // Set distance to start point to 0
            PriorityQueueNode source = prioGraph[startPoint.X][startPoint.Y];
            source.Distance = 0;
            source.Handle = null;

            // Create a new priority queue and add the start node to it
            IPriorityQueue<PriorityQueueNode> queue = this.CreateQueue(graph);
            queue.Add(ref source.Handle, source);

            while(!queue.IsEmpty)
            {
                // Fetch node with least distance
                PriorityQueueNode u = queue.DeleteMin();

                // If we arrived at the end point break the loop
                if (u.X == endPoint.X && u.Y == endPoint.Y)
                {
                    found = true;
                    break;
                }                    

                // For each adjacent node
                PriorityQueueNode[] adjacent = GraphTools<PriorityQueueNode>.GetAdjacentElements(prioGraph, u);
                foreach (PriorityQueueNode v in adjacent)
                {
                    // That is not occupied
                    if(!v.Occupied)
                    {
                        // If dist[u] + dist[u][v] < dist[v] we found a shorter path to v.
                        double distance = u.Distance + GraphTools<Node>.Distance(u, v);
                        if(distance < v.Distance)
                        {
                            v.Distance = distance;
                            v.Parent = u;
                            // Add or replace the found node to the priority queue
                            PriorityQueueNode x;
                            if(v.Handle == null)
                                queue.Add(ref v.Handle, v);
                            else if (queue.Find(v.Handle, out x))
                                queue.Replace(v.Handle, v);
                            else
                                queue.Add(ref v.Handle, v);
                        }
                    }                    
                }                
            }

            if (!found)
                throw new ApplicationException("Unable to find a path between points.");

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
            while (current.Parent != null)
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

        /// <summary>
        /// Creates a new IPriorityQueue containing submitted
        /// Node elements.
        /// </summary>
        /// <param name="nodes">Elements to create queue with.</param>
        /// <returns>A IPriorityQueue containing sumbitted nodes.</returns>
        private IPriorityQueue<PriorityQueueNode> CreateQueue(Node[][] nodes)
        {
            IntervalHeap<PriorityQueueNode> queue = new IntervalHeap<PriorityQueueNode>(PriorityQueueNode.SortByDistance);
            foreach (Node[] subnodes in nodes)
                foreach (PriorityQueueNode node in subnodes)
                {
                    node.Handle = null;
                }

            return queue;
        }
    }
}
