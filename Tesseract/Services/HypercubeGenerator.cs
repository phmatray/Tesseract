using Tesseract.Models;

namespace Tesseract.Services;

public interface IHypercubeGenerator
{
    List<Vertex4D> GenerateVertices();
    bool IsEdge(Vertex4D v1, Vertex4D v2);
}

public class HypercubeGenerator : IHypercubeGenerator
{
    public List<Vertex4D> GenerateVertices()
    {
        var vertices = new List<Vertex4D>();
        for (int i = 0; i < 16; i++) // 2^4 vertices
        {
            vertices.Add(new Vertex4D(
                (i & 1) == 0 ? -1 : 1,
                (i & 2) == 0 ? -1 : 1,
                (i & 4) == 0 ? -1 : 1,
                (i & 8) == 0 ? -1 : 1
            ));
        }
        return vertices;
    }

    public bool IsEdge(Vertex4D v1, Vertex4D v2)
    {
        int diffCount = 0;
        if (v1.X != v2.X) diffCount++;
        if (v1.Y != v2.Y) diffCount++;
        if (v1.Z != v2.Z) diffCount++;
        if (v1.W != v2.W) diffCount++;
        return diffCount == 1; // Edge if exactly one coordinate differs
    }
}
