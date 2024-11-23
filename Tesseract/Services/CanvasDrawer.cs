using Blazor.Extensions.Canvas.Canvas2D;
using Tesseract.Models;

namespace Tesseract.Services;

public interface ICanvasDrawer
{
    Task DrawHypercubeAsync(List<Vertex4D> vertices, Func<Vertex4D, Vertex4D, bool> isEdge);
}

public class CanvasDrawer : ICanvasDrawer
{
    private readonly Canvas2DContext _context;
    private readonly IProjectionService _projectionService;

    public CanvasDrawer(Canvas2DContext context, IProjectionService projectionService)
    {
        _context = context;
        _projectionService = projectionService;
    }

    public async Task DrawHypercubeAsync(List<Vertex4D> vertices, Func<Vertex4D, Vertex4D, bool> isEdge)
    {
        double distance = 3.0;
        double scale = 150.0;
        double offsetX = 400;
        double offsetY = 300;

        var projectedVertices = vertices
            .Select(v => _projectionService.Project(v, distance, scale, offsetX, offsetY))
            .ToList();

        // Draw edges
        for (int i = 0; i < vertices.Count; i++)
        {
            for (int j = 0; j < vertices.Count; j++)
            {
                if (isEdge(vertices[i], vertices[j]))
                {
                    var start = projectedVertices[i];
                    var end = projectedVertices[j];
                    await _context.BeginPathAsync();
                    await _context.MoveToAsync(start.x, start.y);
                    await _context.LineToAsync(end.x, end.y);
                    await _context.StrokeAsync();
                }
            }
        }

        // Draw vertices
        foreach (var vertex in projectedVertices)
        {
            await _context.BeginPathAsync();
            await _context.ArcAsync(vertex.x, vertex.y, 4, 0, 2 * Math.PI);
            await _context.FillAsync();
        }
    }
}
