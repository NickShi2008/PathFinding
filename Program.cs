
using System.Dynamic;
using System.Numerics;
using System.Text.Json;
using System.Linq;
using System.ComponentModel;

namespace PathFinding
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
            string edges = File.ReadAllText("\\\\GMRDC1\\Folder Redirection\\Nicholas.Shi\\Documents\\Github\\Data-Structures\\DijkstraAlgorithm\\AirportProblemEdges.txt");
            string vertices = File.ReadAllText("\\\\GMRDC1\\Folder Redirection\\Nicholas.Shi\\Documents\\Github\\Data-Structures\\DijkstraAlgorithm\\AirportProblemVertices.txt");

            Graph<string> dijkstraGraph = new Graph<string>();
            dijkstraGraph.Edges = new List<Edge<string>>();
            dijkstraGraph.Vertices = new List<Vertex<string>>();

            EdgeDTO[] Edges = JsonSerializer.Deserialize<EdgeDTO[]>(edges);

            string[] Vertices = JsonSerializer.Deserialize<string[]>(vertices);

            //int currentVertex = 0;


            //Adding verticies
            for (int i = 0; i < Vertices.Length; i++)
            {

                dijkstraGraph.AddVertex(new Vertex<string>(Vertices[i]));
            }

            //adding edges
            for (int i = 0; i < Edges.Length; i++)
            {
                Vertex<string> startVertex = dijkstraGraph.Vertices.FirstOrDefault(v => v.Value == Edges[i].Start);
                Vertex<string> endVertex = dijkstraGraph.Vertices.FirstOrDefault(v => v.Value == Edges[i].End);

                if (startVertex != null && endVertex != null)
                {
                    dijkstraGraph.AddEdge(startVertex, endVertex, Edges[i].Distance);
                }
            }

            //adding neighbors
            for (int i = 1; i < dijkstraGraph.Vertices.Count; i++)
            {
                dijkstraGraph.Vertices[i].Neighbors.Add(new Edge<string>(null, dijkstraGraph.Vertices[i], float.PositiveInfinity));
            }


            var result = dijkstraGraph.Dijkstra(dijkstraGraph.Search("JFK"), dijkstraGraph.Search("IND"));

            foreach (var index in result)
            {
                Console.WriteLine($"Vertex : {index.Value}");
            }
            

            //hardcode for jfk to ind vertex
            
            



            //code for smallest distances
            /* totalDistances[dijkstraGraph.Vertices[0]] = 0;

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
                     }


                 }
             } 


             foreach (var vertex in totalDistances)
             {
                 Console.WriteLine($"Vertex: {vertex.Key.Value}, Distance: {vertex.Value}");
             }*/


        }
    }
}
