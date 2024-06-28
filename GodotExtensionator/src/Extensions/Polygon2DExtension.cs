using Godot;
using Godot.Collections;

namespace GodotExtensionator {

    public static class Polygon2DExtension {

        /// <summary>
        /// Creates a circle polygon with the specified radius and number of segments.
        /// </summary>
        /// <param name="polygon">The Polygon2D to modify.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="segments">The number of segments to approximate the circle (default: 32).</param>
        public static void Circle(this Polygon2D polygon, float radius, int segments = 32) {
            Array<Vector2> points = [];

            foreach (int index in GD.Range(segments)) {
                float angle = index * 2 * Mathf.Pi / segments;
                points.Add(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius);
            }

            polygon.Polygon = [.. points];
        }

        /// <summary>
        /// Creates a curved polygon approximating a circle sector.
        /// </summary>
        /// <param name="polygon">The Polygon2D to modify.</param>
        /// <param name="radius">The radius of the entire circle the sector originates from.</param>
        /// <param name="startAngle">The starting angle of the sector in radians (default: 0, points to the right, positive values rotate counter-clockwise).</param>
        /// <param name="endAngle">The ending angle of the sector in radians (default: Mathf.PI).</param>
        /// <param name="segments">The number of segments to approximate the curve (default: 32, higher segments create a smoother curve).</param>
        /// <remarks>
        /// This function is useful for creating shapes like pie charts, curved edges, or custom UI elements.
        /// </remarks>
        public static void PartialCircle(this Polygon2D polygon, float radius, float startAngle = 0, float endAngle = Mathf.Pi, int segments = 32) {
            Array<Vector2> points = [];

            foreach (int index in GD.Range(segments)) {
                float t = (float)index / (segments - 1);
                float angle = Mathf.Lerp(startAngle, endAngle, t);
                Vector2 point = Vector2.Right.Rotated(angle) * radius;

                points.Add(point);
            }

            polygon.Polygon = [.. points];
        }

        /// <summary>
        /// Creates a ring-shaped polygon with specified inner and outer radii.
        /// </summary>
        /// <param name="polygon">The Polygon2D to modify.</param>
        /// <param name="innerRadius">The radius of the hole in the center of the donut.</param>
        /// <param name="outerRadius">The radius of the outer edge of the donut.</param>
        /// <param name="segments">The number of segments for both the inner and outer circles (default: 32, higher segments create smoother curves).</param>
        /// <remarks>
        /// This function is useful for creating objects with holes, rings, or donut shapes.
        /// </remarks>
        public static void Donut(this Polygon2D polygon, float innerRadius, float outerRadius, int segments = 32) {
            Array<Vector2> innerPoints = [];
            Array<Vector2> outerPoints = [];

            foreach (int index in GD.Range(segments)) {
                float angle = index * 2 * Mathf.Pi / segments;
                outerPoints.Add(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * outerRadius);
                innerPoints.Add(new Vector2(Mathf.Cos(-angle), Mathf.Sin(-angle)) * innerRadius);
            }

            polygon.Polygon = [.. outerPoints, .. innerPoints];
        }

        /// <summary>
        /// Creates a basic rectangular polygon with the specified width and height.
        /// </summary>
        /// <param name="polygon">The Polygon2D to modify.</param>
        /// <param name="width">The horizontal dimension of the rectangle.</param>
        /// <param name="height">The vertical dimension of the rectangle.</param>
        /// <remarks>
        /// This function is a fundamental building block for many other 2D shapes and UI elements.
        /// </remarks>
        public static void Rectangle(this Polygon2D polygon, float width, float height) {
            float halfWidth = width / 2f;
            float halfHeight = height / 2f;

            Array<Vector2> points = [
                new Vector2(-halfWidth, -halfHeight),
            new Vector2(halfWidth, -halfHeight),
            new Vector2(halfWidth, halfHeight),
            new Vector2(-halfWidth, halfHeight),
            new Vector2(-halfWidth, -halfHeight),

        ];

            polygon.Polygon = [.. points];
        }

        /// <summary>
        /// Creates a rectangle polygon with rounded corners of the specified radius.
        /// </summary>
        /// <param name="polygon">The Polygon2D to modify.</param>
        /// <param name="width">The horizontal dimension of the rectangle.</param>
        /// <param name="height">The vertical dimension of the rectangle.</param>
        /// <param name="cornerRadius">The radius of the curvature applied to each corner.</param>
        /// <remarks>
        /// This function is useful for creating UI elements or shapes with a softer and more visually appealing look.
        /// </remarks>
        public static void RoundedRectangle(this Polygon2D polygon, float width, float height, float cornerRadius) {
            polygon.Rectangle(width - cornerRadius * 2, height - cornerRadius * 2);

            Array<Vector2> points = [new Vector2(-width / 2, 0)];
            Vector2[] RectPoints = polygon.Polygon;

            foreach (int index in GD.Range(4)) {
                float angle = -3f / 4f * Mathf.Pi + Mathf.Pi / 2f * index;
                polygon.PartialCircle(cornerRadius, angle - Mathf.Pi / 4, angle + Mathf.Pi / 4);
                Vector2[] cornerCirclePoints = polygon.Polygon;

                points.AddRange(Translate(cornerCirclePoints, RectPoints[index]));
            }

            points.Add(points.First());

            polygon.Polygon = [.. points];
        }

        /// <summary>
        /// Translates an array of points by a specified vector.
        /// </summary>
        /// <param name="points">The array of points to translate.</param>
        /// <param name="translation">The vector representing the amount and direction of the translation.</param>
        /// <returns>A new array containing the translated points.</returns>
        private static Vector2[] Translate(Vector2[] points, Vector2 translation) => points * new Transform2D(0, -translation);
    }

}