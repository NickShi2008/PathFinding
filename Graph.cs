using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraAlgorithm
{
    public class Graph<T> where T : IComparable
    {
        public List<Vertex<T>> Vertices { get; set; }
        public List<Edge<T>> Edges { get; set; }

        public int VertexCount => Vertices.Count;

        public Graph()
        {
            Vertices = new List<Vertex<T>>();

            Edges = new List<Edge<T>>();
        }

        public void AddVertex(Vertex<T> vertex)
        {
            if (!SearchVertex(vertex) && vertex.NeighborCount == 0)
            {
                Vertices.Add(vertex);
            }
        }

        public bool RemoveVertex(Vertex<T> vertex)
        {
            if (Vertices.Contains(vertex))
            {
                foreach (Edge<T> edges in vertex.Neighbors)
                {
                    edges.EndingPoint.Neighbors.Remove(edges.EndingPoint.FindFirstEdge(vertex));
                    vertex.Neighbors.Remove(vertex.FindFirstEdge(edges.EndingPoint));
                }
                Vertices.Remove(vertex);
                return true;
            }
            return false;
        }

        private bool SearchVertex(Vertex<T> vertex)
        {
            bool check = Vertices.Contains(vertex); 
            return vertex != null && Vertices.Contains(vertex);
        }

        public bool AddEdge(Vertex<T> a, Vertex<T> b, float distance)
        {
            if (SearchVertex(a) && SearchVertex(b) && !a.HasEdge(b) && !b.HasEdge(a))
            {
                Edge<T> AConnector = new Edge<T>(a, b, distance);
                Edge<T> BConnector = new Edge<T>(b, a, distance);
                Edges.Add(AConnector);
                Edges.Add(BConnector);
                if(!a.Neighbors.Contains(AConnector))
                    a.Neighbors.Add(AConnector);
                if (!b.Neighbors.Contains(BConnector))
                    b.Neighbors.Add(BConnector);

                return true;
            }
            return false;
        }

        public bool RemoveEdge(Vertex<T> a, Vertex<T> b)
        {
            if (SearchVertex(a) && SearchVertex(b) && a.HasEdge(b) && b.HasEdge(a))
            {
                a.Neighbors.Remove(a.FindFirstEdge(b));
                b.Neighbors.Remove(b.FindFirstEdge(a));
                return true;
            }
            return false;
        }

        public Vertex<T> Search(Vertex<T> vertex)
        {
            int count = -1;
            for (int i = 0; i < Vertices.Count; i++)
            {
                if (Vertices[i] == vertex)
                {
                    count = i;
                    break;
                }
            }

            if (count == -1)
            {
                return null;
            }
            return Vertices[count];
        }

        public Edge<T> GetEdge(Vertex<T> a, Vertex<T> b)
        {
            if (a != null && b != null && a.HasEdge(b) && b.HasEdge(a))
            {
                return a.FindFirstEdge(b);
            }
            return null;
        }

    }
}
