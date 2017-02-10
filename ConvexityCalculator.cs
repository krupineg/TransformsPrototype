using System;
using System.Collections.Generic;
using System.Linq;

namespace TransformsPrototype
{
    public class ConvexityCalculator : IConvexityCalculator
    {
        public bool IsConvex(IEnumerable<IMappingPlanePointViewModel> collection)
        {
            var items = collection.ToArray();
            int count = items.Length;
            int sign = 0;
            for (int currentIndex = 0; currentIndex < count; currentIndex++)
            {
                var nextIndex = (currentIndex + 1) % count;
                var afterNextIndex = (currentIndex + 2) % count;

                var crossProduct = (items[nextIndex].X - items[currentIndex].X) * (items[afterNextIndex].Y - items[nextIndex].Y);
                crossProduct -= (items[nextIndex].Y - items[currentIndex].Y) * (items[afterNextIndex].X - items[nextIndex].X);

                if (sign != 0 && sign != Math.Sign(crossProduct))
                {
                    return false;
                }

                sign = Math.Sign(crossProduct);
            }
            return true;
        }
        
    }
}