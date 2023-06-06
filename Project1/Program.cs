namespace Project1
{
    using System;
    using System.Collections.Generic;

    public class LabyrinthGenerator
    {
        private int width;
        private int height;
        private char[,] map;
        private int[,] path = new int[11, 11];

        public LabyrinthGenerator(int width, int height)
        {
            this.width = width;
            this.height = height;
            map = new char[width, height];
        }

        public void Generate()
        {
            // Fill the map with walls
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    map[x, y] = '#';
                }
            }

            // Generate the labyrinth starting from position (1, 1)
            GenerateLabyrinth(1, 1);

            // Set the start and end points
            map[1, 0] = '#';
            map[width - 2, height - 1] = '#';
        }

        private void GenerateLabyrinth(int x, int y)
        {
            // Mark the current cell as visited
            map[x, y] = ' ';

            // Create a random list of directions
            List<Direction> directions = new List<Direction>
        {
            Direction.Up, Direction.Down, Direction.Left, Direction.Right
        };
            Shuffle(directions);

            // Iterate through the directions
            foreach (Direction direction in directions)
            {
                int nextX = x + GetDirectionXOffset(direction) * 2;
                int nextY = y + GetDirectionYOffset(direction) * 2;

                if (IsValidCell(nextX, nextY))
                {
                    if (map[nextX, nextY] == '#')
                    {
                        // Carve a passage
                        map[x + GetDirectionXOffset(direction), y + GetDirectionYOffset(direction)] = ' ';
                        GenerateLabyrinth(nextX, nextY);
                    }
                }
            }
        }

        private bool IsValidCell(int x, int y)
        {
            return x > 0 && y > 0 && x < width && y < height;
        }

        private int GetDirectionXOffset(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: return 0;
                case Direction.Down: return 0;
                case Direction.Left: return -1;
                case Direction.Right: return 1;
                default: return 0;
            }
        }

        private int GetDirectionYOffset(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: return -1;
                case Direction.Down: return 1;
                case Direction.Left: return 0;
                case Direction.Right: return 0;
                default: return 0;
            }
        }

        private void Shuffle<T>(List<T> list)
        {
            Random random = new Random();

            for (int i = 0; i < list.Count - 1; i++)
            {
                int j = random.Next(i, list.Count);
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }

        public void PrintMap()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Write(map[x, y]);
                }
                Console.WriteLine();
            }
        }

        public void getPath()
        {
            for (int x = 0; x < width; x++)
            {
                if (map[x, 1] == ' ')
                {
                    path[x, 1] ++;
                    int startY = 1;
                    int startX = x;
                    while (true)
                {
                        if(startY == 8)
                        {
                            break;
                        }
                        if (startY + 1 < 9 &&  map[startX, startY + 1] == ' ' && path[startX, startY] > path[startX, startY+1])
                        {
                            startY++;
                            path[startX, startY] ++;  
                        }

                        else if (startX + 1 < 9 &&  map[startX +1 , startY] == ' ' && path[startX, startY] > path[startX+1, startY])
                        {
                            startX++;
                            path[startX, startY]++;
                        }

                        else if(startX - 1 >= 0 &&  map[startX-1, startY] == ' ' && path[startX, startY] > path[startX-1, startY])
                        {
                            startX--;
                            path[startX, startY]++;
                        }
                        else
                        {
                            path[startX, startY]++;

                            if (startY == 0)
                            {
                                break;
                            }

                            else if (startY - 1  >= 1 && map[startX, startY - 1] == ' ')
                            {
                                startY--;
                                path[startX, startY]++;
                            }

                            else if (startX + 1 < 9 &&  map[startX + 1, startY] == ' ')
                            {
                                startX++;
                                path[startX, startY]++;
                            }

                            else if (startX - 1 >= 0 &&  map[startX - 1, startY] == ' ')
                            {
                                startX--;
                                path[startX, startY]++;
                            }
                        }

                    }
                if(startY == 8)
                    {
                        break;
                    }
                }
            }
        }

        public void PrintPath()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                        
                    if(map[x, y] == ' ')
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }

        public void PrintSolvPath()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if(path[i, j] != 0)
                    {
                        Console.Write(path[i, j]);
                    }
                    else
                    {
                    Console.Write(0);
                    }
                }
                Console.WriteLine();
            }
        }
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    internal class Program
    {
        public static void Main()
        {
            int width = 11; // Width of the labyrinth map
            int height = 11; // Height of the labyrinth map
            Graph graph = new Graph(6);
            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 4);
            graph.AddEdge(2, 5);

            LabyrinthGenerator generator = new LabyrinthGenerator(width, height);
            generator.Generate();
            Console.WriteLine("generate map");
            Console.WriteLine();
            generator.PrintMap();
            Console.WriteLine();
            Console.WriteLine("generate path");
            generator.PrintPath();
            generator.getPath();
           generator.PrintSolvPath();
        }
    }
}