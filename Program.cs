
using System.Dynamic;
using System.Numerics;
using System.Text.Json;
using System.Linq;
using System.ComponentModel;

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


        //REPEATED VERTICES??
        static void Main(string[] args)
        {

            /* Steps
             * 1. Dequeue
             * 2. Look at neighbors + enqueueue
             * 3. Reset Distances + founders
             * 4. 
             * */
            string edges = File.ReadAllText("C:\\Users\\nickj\\Source\\Repos\\DijkstraAlgorithm\\AirportProblemEdges.txt");
            string vertices = File.ReadAllText("C:\\Users\\nickj\\Source\\Repos\\DijkstraAlgorithm\\AirportProblemVertices.txt");

            Graph<string> dijkstraGraph = new Graph<string>();
            dijkstraGraph.Edges = new List<Edge<string>>();
            dijkstraGraph.Vertices = new List<Vertex<string>>();

            EdgeDTO[] Edges = JsonSerializer.Deserialize<EdgeDTO[]>(edges);
            Edge<string>[] EdgeArray = new Edge<string>[Edges.Length];
            
            string[] Vertices = JsonSerializer.Deserialize<string[]>(vertices);

            //int currentVertex = 0;


            Dictionary<Vertex<string>, float> totalDistances = new Dictionary<Vertex<string>, float>();
            for (int i = 0; i < Vertices.Length; i++)
            {
      
                dijkstraGraph.AddVertex(new Vertex<string>(Vertices[i]));
            }


            for (int i = 0; i < Edges.Length; i++)
            {
                Vertex<string> startVertex = dijkstraGraph.Vertices.FirstOrDefault(v => v.Value == Edges[i].Start);
                Vertex<string> endVertex = dijkstraGraph.Vertices.FirstOrDefault(v => v.Value == Edges[i].End);

                if (startVertex != null && endVertex != null)
                {
                    dijkstraGraph.AddEdge(startVertex, endVertex, Edges[i].Distance);
                }
            }
        

            for (int i = 1; i < dijkstraGraph.Vertices.Count; i++)
            {
                dijkstraGraph.Vertices[i].Neighbors.Add(new Edge<string>(null, dijkstraGraph.Vertices[i], float.PositiveInfinity));
            }

         

            List<Vertex<string>> visitedVertices = new List<Vertex<string>>();

            PriorityQueue<Vertex<string>, float> queuedDistances = new PriorityQueue<Vertex<string>, float>();

            foreach(var v in dijkstraGraph.Vertices)
            {
                  totalDistances.Add(v, float.PositiveInfinity);
            }
            totalDistances[dijkstraGraph.Vertices[0]] = 0;

            queuedDistances.Enqueue(dijkstraGraph.Vertices[0], 0);



            while (queuedDistances.Count > 0) 
            {
                Vertex<string> currentVertex = queuedDistances.Dequeue();

                if (visitedVertices.Contains(currentVertex))
                    continue;
                visitedVertices.Add(currentVertex);

                foreach (var edge in currentVertex.Neighbors)
                {
                    if (totalDistances[currentVertex] + totalDistances[currentVertex]
                        < totalDistances[edge.EndingPoint])
                     {
                        totalDistances[edge.EndingPoint] = totalDistances[currentVertex] + edge.Distance;


                        queuedDistances.Enqueue(edge.EndingPoint, totalDistances[currentVertex] + edge.Distance);
                        //visitedVertices.Remove(edge.EndingPoint);
                    }


                }
            } 


            foreach (var vertex in totalDistances)
            {
                Console.WriteLine($"Vertex: {vertex.Key.Value}, Distance: {vertex.Value}");
            }


        }
    }
}
