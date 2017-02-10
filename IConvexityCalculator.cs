using System.Collections.Generic;

namespace TransformsPrototype
{
    public interface IConvexityCalculator
    {
        bool IsConvex(IEnumerable<IMappingPlanePointViewModel> collection);
    }
}