using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
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
            if (SearchVertex(a) && SearchVertex(b))
            {
                Edge<T> AConnector = new Edge<T>(a, b, distance);
                Edges.Add(AConnector);
                if (!a.Neighbors.Contains(AConnector))
                    a.Neighbors.Add(AConnector);

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

        public Vertex<T> Search(T vertex)
        {
            int count = -1;
            for (int i = 0; i < Vertices.Count; i++)
            {
                if (Vertices[i].Value.Equals(vertex))
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

        public List<Vertex<T>> Dijkstra(Vertex<T> start, Vertex<T> end)
        {
            //Init
            Dictionary<Vertex<T>, float> totalDistances = new Dictionary<Vertex<T>, float>();
            List<Vertex<T>> visitedVertices = new List<Vertex<T>>();
            PriorityQueue<Vertex<T>, float> queuedDistances = new PriorityQueue<Vertex<T>, float>();

            //set each vertex as Unknown
            foreach (var v in Vertices)
            {
                totalDistances.Add(v, float.PositiveInfinity);
            }

            //prepare start vertex
            totalDistances[start] = 0;

            queuedDistances.Enqueue(start, 0);


            //looks till visits all vertex
            while (!visitedVertices.Contains(end))
            {
                Vertex<T> currentVertex = queuedDistances.Dequeue();

                if (visitedVertices.Contains(currentVertex))
                    continue;
                visitedVertices.Add(currentVertex);

                foreach (var edge in currentVertex.Neighbors)
                {
                    if (totalDistances[currentVertex] + edge.Distance
                        < totalDistances[edge.EndingPoint])
                    {
                        totalDistances[edge.EndingPoint] = totalDistances[currentVertex] + edge.Distance;


                        queuedDistances.Enqueue(edge.EndingPoint, totalDistances[currentVertex] + edge.Distance);

                    }


                }
            }
            //traces backwards to the beginning

            Stack<Vertex<T>> reversePath = new Stack<Vertex<T>>();
            Vertex<T> lastVertex = end;
            while (!lastVertex.Equals(start))
            {
                foreach (var vertex in totalDistances)
                {

                    for (int i = 0; i < Edges.Count; i++)
                    {
                        if (Edges[i].EndingPoint.Equals(lastVertex) && Edges[i].StartingPoint.Value.Equals(vertex.Key.Value)
                            && vertex.Value + Edges[i].Distance == totalDistances[lastVertex])
                        {
                            lastVertex = vertex.Key;
                            reversePath.Push(Edges[i].EndingPoint);
                            break;
                        }
                    }

                }
            }

            List<Vertex<T>> path = new List<Vertex<T>>();

            while (reversePath.Count > 0)
            {
                path.Add(reversePath.Pop());
            }

            return path;
        }

        public List<Vertex<T>> ASTAR(Vertex<T> start, Vertex<T> end)
        {
            Dictionary<Vertex<T>, float> totalDistances = new Dictionary<Vertex<T>, float>();
            List<Vertex<T>> visitedVertices = new List<Vertex<T>>();
            PriorityQueue<Vertex<T>, float> queuedDistances = new PriorityQueue<Vertex<T>, float>();

            foreach (var v in Vertices)
            {
                totalDistances.Add(v, float.PositiveInfinity);
            }

            totalDistances[start] = 0;

            queuedDistances.Enqueue(start, 0);

            while (visitedVertices.Contains(end))
            {
                Vertex<T> vertex = queuedDistances.Dequeue();

                if (visitedVertices.Contains(vertex))
                    continue;

                visitedVertices.Add(vertex);

                foreach (var neigh in vertex.Neighbors)
                {
                    //if(Euclidean(neigh))
                }

            }



            List<Vertex<T>> path = new List<Vertex<T>>();

            return path;
        }

        /* public float Manhattan(Vertex<T> start, Vertex<T> end)
          {
              float dx = MathF.Abs(start.x - end.x);
              float dy = MathF.Abs(start.x - end.y);
              //distance from one square to another
              float D = 1;
              return D * (dx + dy);
          }
        

        public float Diagonal(Vertex<T> start, Vertex<T> end)
        {
            float dx = MathF.Abs(start.x - end.x);
            float dy = MathF.Abs(start.x - end.y);
            //distance from one square to another
            float D = 1;
            float DTwo = MathF.Sqrt(2);
            return D * (dx + dy) + (DTwo - 2 * D) * MathF.Min(dx, dy);

        }

        public float Euclidean(Vertex<T> start, Vertex<T> end)
        {
            float dx = MathF.Abs(start.x - end.x);
            float dy = MathF.Abs(start.x - end.y);
            //distance from one square to another
            float D = 1;
            float DTwo = MathF.Sqrt(2);
            return D * MathF.Sqrt(dx*dx + dy*dy);

        }*/
    }
}
