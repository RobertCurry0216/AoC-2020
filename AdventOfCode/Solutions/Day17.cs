using AoCHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day17 : BaseDay
    {
        private bool[,,] pocket3D;
        private bool[,,,] pocket4D;
        private int pocketSize = 30;

        public Day17()
        {
            var y = 10;
            pocket3D = CreatePocket3D(pocketSize);
            pocket4D = CreatePocket4D(pocketSize);
            File.ReadAllLines(base.InputFilePath).ToList()
                .ForEach((l => 
                {
                    var x = 10;
                    foreach (var c in l.ToCharArray())
                    {
                        if (c == '#')
                        {
                            pocket3D[x, y, 10] = true;
                            pocket4D[x, y, 10, 10] = true;
                        }
                        x++;
                    }
                    y++;
                }));
        }

        private bool[,,] CreatePocket3D(int x)
        {
            return new bool[x,x,x];
        }

        private bool[,,,] CreatePocket4D(int x)
        {
            return new bool[x, x, x, x];
        }

        public override string Solve_1()
        {
            var count = 0;
            for (int i = 0; i < 6; i++)
            {
                Cycle3D();
            }

            foreach (var v in pocket3D)
            {
                if (v) count++;
            }

            return $"{count}";
        }

        private void Cycle3D()
        {
            var newPocket = CreatePocket3D(pocketSize);
            for (int z = 1; z < pocketSize - 1; z++)
            {
                for (int y = 1; y < pocketSize - 1; y++)
                {
                    for (int x = 1; x < pocketSize - 1; x++)
                    {
                        var n = CountNeighbors3D(x, y, z);
                        if (pocket3D[x, y, z])
                        {
                            newPocket[x, y, z] = n == 2 || n == 3;
                        }
                        else
                        {
                            newPocket[x, y, z] = n == 3;
                        }
                    }
                }
            }
            pocket3D = newPocket;
        }

        private int CountNeighbors3D(int x, int y, int z)
        {
            var count = 0;
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dz = -1; dz <= 1; dz++)
                    {
                        if (pocket3D[x+dx, y+dy, z+dz]) count++;
                    }
                }
            }
            if (pocket3D[x, y, z]) count--;
            return count;
        }

        public override string Solve_2()
        {
            var count = 0;
            for (int i = 0; i < 6; i++)
            {
                Cycle4D();
            }

            foreach (var v in pocket4D)
            {
                if (v) count++;
            }

            return $"{count}";
        }

        private void Cycle4D()
        {
            var newPocket = CreatePocket4D(pocketSize);
            for (int w = 1; w < pocketSize - 1; w++)
            {
                for (int z = 1; z < pocketSize - 1; z++)
                {
                    for (int y = 1; y < pocketSize - 1; y++)
                    {
                        for (int x = 1; x < pocketSize - 1; x++)
                        {
                            var n = CountNeighbors4D(x, y, z, w);
                            if (pocket4D[x, y, z, w])
                            {
                                newPocket[x, y, z, w] = n == 2 || n == 3;
                            }
                            else
                            {
                                newPocket[x, y, z, w] = n == 3;
                            }
                        }
                    }
                }
            }
            pocket4D = newPocket;
        }

        private int CountNeighbors4D(int x, int y, int z, int w)
        {
            var count = 0;
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dz = -1; dz <= 1; dz++)
                    {
                        for (int dw = -1; dw <= 1; dw++)
                        {
                            if (pocket4D[x + dx, y + dy, z + dz, w + dw]) count++;
                        }
                    }
                }
            }
            if (pocket4D[x, y, z, w]) count--;
            return count;
        }
    }
}
