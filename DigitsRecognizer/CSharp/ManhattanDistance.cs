using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp
{
    public class ManhattanDistance : IDistance
    {
        public double Between(int[] p1, int[] p2)
        {
            if (p1.Length != p2.Length)
            {
                throw new ArgumentException("Inconsistent image sizes");
            }

            var length = p1.Length;
            var distance = 0;
            for (int i = 0; i < length; i++)
            {
                distance += Math.Abs(p1[i] - p2[i]);
            }
            return distance;
        }
    }
}
