namespace TransformsPrototype
{
    public interface IMappingPlanePointViewModel
    {
        double X { get; set; }
        double Y { get; set; }

        IMappingPlanePointViewModel Next { get; set; }
    }
}