using System.Collections.Generic;

namespace TransformsPrototype
{
    public interface IConvexityCalculator
    {
        int Calculate(IEnumerable<IMappingPlanePointViewModel> collection);
    }
}