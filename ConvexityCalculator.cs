using System.Collections.Generic;
using System.Linq;

namespace TransformsPrototype
{
    public class ConvexityCalculator : IConvexityCalculator
    {
        public int Calculate(IEnumerable<IMappingPlanePointViewModel> collection)
        {
            var p = collection.ToArray();
            int i, j, k;
            int flag = 0;
            double z;
            var n = p.Length;
            if (n < 3)
                return (0);

            for (i = 0; i < n; i++)
            {
                j = (i + 1) % n;
                k = (i + 2) % n;
                z = (p[j].X - p[i].X) * (p[k].Y - p[j].Y);
                z -= (p[j].Y - p[i].Y) * (p[k].X - p[j].X);
                if (z < 0)
                    flag |= 1;
                else if (z > 0)
                    flag |= 2;
                if (flag == 3)
                    return 1;
            }
            if (flag != 0)
                return -1;
            else
                return (0);
        }
    }
}