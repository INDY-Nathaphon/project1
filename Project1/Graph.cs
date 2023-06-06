using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    using System;
    using System.Collections.Generic;

    internal class Graph
    {
        private int V; // Number of vertices
        private List<int>[] adjList; // Adjacency list representation of the graph

        public Graph(int v)
        {
            V = v;
            adjList = new List<int>[V];
            for (int i = 0; i < V; i++)
            {
                adjList[i] = new List<int>();
            }
        }

        public void AddEdge(int v, int w)
        {
            adjList[v].Add(w); // Add w to v's adjacency list
        }

        public void DFS(int startVertex)
        {
            bool[] visited = new bool[V]; // Mark all vertices as not visited

            DFSUtil(startVertex, visited);
        }

        private void DFSUtil(int vertex, bool[] visited)
        {
            visited[vertex] = true; // Mark the current vertex as visited
            Console.Write(vertex + " ");

            // Recur for all the vertices adjacent to this vertex
            foreach (int adjVertex in adjList[vertex])
            {
                if (!visited[adjVertex])
                {
                    DFSUtil(adjVertex, visited);
                }
            }
        }
    }
}
