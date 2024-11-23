using Tesseract.Models;

namespace Tesseract.Services;

public interface IProjectionService
{
    (double x, double y) Project(Vertex4D vertex, double distance, double scale, double offsetX, double offsetY);
}

public class PerspectiveProjectionService : IProjectionService
{
    public (double x, double y) Project(Vertex4D vertex, double distance, double scale, double offsetX, double offsetY)
    {
        // 4D to 3D projection
        double wFactor = distance / (distance - vertex.W);
        double x3D = vertex.X * wFactor;
        double y3D = vertex.Y * wFactor;
        double z3D = vertex.Z * wFactor;

        // 3D to 2D projection
        double x2D = x3D / (distance - z3D);
        double y2D = y3D / (distance - z3D);

        // Scale and center on canvas
        return (x2D * scale + offsetX, y2D * scale + offsetY);
    }
}
