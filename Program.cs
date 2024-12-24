
using System.Dynamic;
using System.Numerics;
using System.Text.Json;

namespace DijkstraAlgorithm
{
    public class Program
    {
        struct EdgeDTO
        {
            public string Start { get; set; }
            public string End { get; set; }
            public int Distance { get; set; }
        }

        class Info
        {
            public int Distance { get; set; }
            public Edge<string> Founder { get; set; }
        }

        static void Main(string[] args)
        {
         
            /* Steps
             * 1. Dequeue
             * 2. Look at neighbors + enqueueue
             * 3. Reset Distances + founders
             * 4. 
             * */
            string edges = File.ReadAllText("C:\\Users\\Nicholas.Shi\\source\\repos\\DijkstraAlgorithm\\AirportProblemEdges.txt");
            string vertices = File.ReadAllText("../../../AirportProblemVertices.txt");

            Graph<string> dijkstraGraph = new Graph<string>();
            dijkstraGraph.Edges = new List<Edge<string>>();

            EdgeDTO[] Edges = JsonSerializer.Deserialize<EdgeDTO[]>(edges);
            string[] Vertices = JsonSerializer.Deserialize<string[]>(vertices);

            int currentVertex = 0;

            Dictionary<Vertex<string>, float> totalDistances = new Dictionary<Vertex<string>, float>();

            for(int i = 0; i < Edges.Length; i++) 
            {
                dijkstraGraph.Edges[i] = new Edge<string>(new Vertex<string>(Edges[i].Start), new Vertex<string>(Edges[i].End), Edges[i].Distance);
            }

            for (int i = 0; i < Vertices.Length; i++)
            {
                dijkstraGraph.Vertices[i] = new Vertex<string>(Vertices[i]);
            }

            for (int i = 1; i < dijkstraGraph.Vertices.Count; i++)
            {
                dijkstraGraph.Vertices[i].Neighbors.Add(new Edge<string>(null, dijkstraGraph.Vertices[i], float.PositiveInfinity));
            }
            dijkstraGraph.Vertices[0].Neighbors.Add(new Edge<string>(dijkstraGraph.Vertices[0], dijkstraGraph.Vertices[0],  0));

            dijkstraGraph.Vertices = new List<Vertex<string>>();

            List<Vertex<string>> visitedVertices = new List<Vertex<string>>();

            PriorityQueue<Vertex<string>, float> queuedDistances = new PriorityQueue<Vertex<string>, float>();
            queuedDistances.Enqueue(dijkstraGraph.Vertices[currentVertex], dijkstraGraph.Vertices[currentVertex].FindFirstEdge(dijkstraGraph.Vertices[currentVertex]).Distance);


            Vertex<string> vertex = queuedDistances.Dequeue();
            totalDistances.Add(vertex, 0);
            visitedVertices.Add(vertex);

            foreach (var edge in vertex.Neighbors)
            {
                if (vertex.FindFirstEdge(edge.EndingPoint).Distance + totalDistances[dijkstraGraph.Vertices[currentVertex]] < edge.Distance)
                {

                }
            }


           /* int smallDistanceIndex = 0;
            for (int i = 1; i < dijkstraGraph.Vertices[currentVertex].Neighbors.Count; i++)
            {
                if (dijkstraGraph.Vertices[currentVertex].Neighbors[i].Distance < dijkstraGraph.Vertices[currentVertex].Neighbors[smallDistanceIndex].Distance)
                {
                    bool hasVisited = false;
                    foreach(var v in visitedVertices)
                    {
                        if (v.Equals(dijkstraGraph.Vertices[currentVertex].Neighbors[i].EndingPoint))
                        {
                            hasVisited = true;
                        }
                    }
                    if (!hasVisited)
                    {
                        smallDistanceIndex = i;
                    }
                }

            }

            foreach(var edge in dijkstraGraph.Vertices[currentVertex].Neighbors)
            {
                if(!edge.EndingPoint.Equals(dijkstraGraph.Vertices[currentVertex].Neighbors[smallDistanceIndex].EndingPoint))
                {
                    queuedDistances.Enqueue(edge.EndingPoint, edge.Distance);
                }
            }

            for (int i = 0; dijkstraGraph.VertexCount > 0; i++)
            {
                if (dijkstraGraph.Vertices[currentVertex].Neighbors[smallDistanceIndex].EndingPoint.Equals(dijkstraGraph.Vertices[i]))
                {
                    
                    currentVertex = i;
                    float totalDistance = dijkstraGraph.Vertices[currentVertex].Neighbors[smallDistanceIndex].Distance;

                    totalDistances.Add(dijkstraGraph.Vertices[currentVertex], dijkstraGraph.Vertices[currentVertex].Neighbors[smallDistanceIndex].Distance);
                   
                    
                }
            }*/

            



        }
    }
}
