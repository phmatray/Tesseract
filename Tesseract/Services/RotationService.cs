using Tesseract.Models;

namespace Tesseract.Services;

public interface IRotationService
{
    List<Vertex4D> RotateVertices(List<Vertex4D> vertices, double angleXW, double angleYW, double angleZW);
}

public class RotationService : IRotationService
{
    public List<Vertex4D> RotateVertices(List<Vertex4D> vertices, double angleXW, double angleYW, double angleZW)
    {
        var cosXW = Math.Cos(angleXW);
        var sinXW = Math.Sin(angleXW);

        var cosYW = Math.Cos(angleYW);
        var sinYW = Math.Sin(angleYW);

        var cosZW = Math.Cos(angleZW);
        var sinZW = Math.Sin(angleZW);

        return vertices.Select(v =>
        {
            // Rotate in XW plane
            var x1 = cosXW * v.X - sinXW * v.W;
            var w1 = sinXW * v.X + cosXW * v.W;

            // Rotate in YW plane
            var y1 = cosYW * v.Y - sinYW * w1;
            var w2 = sinYW * v.Y + cosYW * w1;

            // Rotate in ZW plane
            var z1 = cosZW * v.Z - sinZW * w2;
            var w3 = sinZW * v.Z + cosZW * w2;

            return new Vertex4D(x1, y1, z1, w3);
        }).ToList();
    }
}
