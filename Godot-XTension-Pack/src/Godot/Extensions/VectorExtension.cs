using Godot;
using Godot.Collections;

namespace Godot_XTension_Pack {
    public static class VectorExtension {
        private static readonly RandomNumberGenerator _rng = new();

        private static readonly Godot.Collections.Dictionary<Vector2, Vector2> OppositeUpDirectionsV2 = new() {
                {Vector2.Up, Vector2.Down},
                {Vector2.Down, Vector2.Up},
                {Vector2.Left, Vector2.Right},
                {Vector2.Right, Vector2.Left},
            };

        private static readonly Godot.Collections.Dictionary<Vector3, Vector3> OppositeUpDirectionsV3 = new() {
                {Vector3.Up, Vector3.Down},
                {Vector3.Down, Vector3.Up},
                {Vector3.Left, Vector3.Right},
                {Vector3.Right, Vector3.Left},
                {Vector3.Forward, Vector3.Back },
                {Vector3.Back, Vector3.Forward }
            };

        /// <summary>
        /// Retrieves the opposite direction of a given up direction based on a pre-defined mapping.
        /// </summary>
        /// <param name="vector">The vector to use as a reference for opposite directions.</param>
        /// <returns>The opposite direction of the provided upDirection, or Vector2.Zero/Vector3.Zero if not found.</returns>
        public static Vector2 UpDirectionOpposite(this Vector2 vector) {
            if (OppositeUpDirectionsV2.TryGetValue(vector, out Vector2 direction))
                return direction;

            return Vector2.Zero;
        }

        /// <summary>
        /// Retrieves the opposite direction of a given up direction based on a pre-defined mapping.
        /// </summary>
        /// <param name="vector">The vector to use as a reference for opposite directions.</param>
        /// <returns>The opposite direction of the provided upDirection, or Vector3.Zero/Vector3.Zero if not found.</returns>
        public static Vector3 UpDirectionOpposite(this Vector3 vector) {
            if (OppositeUpDirectionsV3.TryGetValue(vector, out Vector3 direction))
                return direction;

            return Vector3.Zero;
        }

        /// <summary>
        /// Gets the opposite direction based on the character's current UpDirection.
        /// </summary>
        /// <param name="body">The CharacterBody2D object (extended on).</param>
        /// <returns>
        /// The opposite direction vector of the character's UpDirection, or Vector2.Zero if not found.
        /// </returns>
        /// <remarks>
        /// This extension method retrieves the opposite direction corresponding to the character's current UpDirection (presumably stored in the `body.UpDirection` property).
        /// It utilizes a pre-defined dictionary named `OppositeUpDirectionsV2` (assumed to be defined elsewhere) to efficiently map the UpDirection to its opposite.
        /// If a mapping exists for the current UpDirection, the corresponding opposite direction vector is returned. Otherwise, the method returns Vector2.Zero.
        /// </remarks>
        public static Vector2 UpDirectionOpposite(this CharacterBody2D body) {
            if (OppositeUpDirectionsV2.TryGetValue(body.UpDirection, out Vector2 direction))
                return direction;

            return Vector2.Zero;
        }

        /// <summary>
        /// Gets the opposite direction based on the character's current UpDirection.
        /// </summary>
        /// <param name="body">The CharacterBody3D object (extended on).</param>
        /// <returns>
        /// The opposite direction vector of the character's UpDirection, or Vector3.Zero if not found.
        /// </returns>
        /// <remarks>
        /// This extension method behaves similarly to the `UpDirectionOpposite` for CharacterBody2D, but operates on 3D vectors.
        /// It retrieves the opposite direction corresponding to the character's current UpDirection (presumably stored in the `body.UpDirection` property).
        /// It utilizes a pre-defined dictionary named `OppositeUpDirectionsV3` (assumed to be defined elsewhere) to map the UpDirection to its opposite in 3D space.
        /// If a mapping exists for the current UpDirection, the corresponding opposite direction vector is returned. Otherwise, the method returns Vector3.Zero.
        /// </remarks>
        public static Vector3 UpDirectionOpposite(this CharacterBody3D body) {
            if (OppositeUpDirectionsV3.TryGetValue(body.UpDirection, out Vector3 direction))
                return direction;

            return Vector3.Zero;
        }

        /// <summary>
        /// Limits the horizontal angle of a Vector2 direction within a specified range.
        /// </summary>
        /// <param name="direction">The Vector2 direction to limit.</param>
        /// <param name="limitAngle">The maximum allowed angle deviation from zero (in radians).</param>
        /// <returns>A new Vector2 with the horizontal angle clamped within the limit.</returns>
        public static Vector2 LimitHorizontalAngle(this Vector2 direction, float limitAngle) {
            float angle = direction.Angle();

            if (Mathf.Abs(angle) > limitAngle && Mathf.Abs(angle) < Mathf.Pi - limitAngle) {
                if (Mathf.Abs(angle) < Mathf.Pi / 2.0f)
                    angle = limitAngle * Mathf.Sign(angle);
                else
                    angle = (Mathf.Pi - limitAngle) * Mathf.Sign(angle);
            }

            return new(Mathf.Cos(angle), Mathf.Sin(angle));
        }

        /// <summary>
        /// Rotates the given vector3 randomly around a horizontal arc (either upwards or downwards).
        /// </summary>
        /// <param name="vector3">The vector3 to be rotated.</param>
        /// <returns>The rotated vector3.</returns>
        /// <remarks>
        /// The rotation angle is randomly generated between -90 and 90 degrees, resulting in a horizontal arc.
        /// The direction of rotation (upwards or downwards) is also randomly chosen.
        /// </remarks>
        public static Vector3 RotateHorizontalRandom(this Vector3 vector3) {
            Vector3 arcDirection = new Array<Vector3>() { Vector3.Down, Vector3.Up }.PickRandom();

            return vector3.Rotated(arcDirection, _rng.RandfRange(-MathF.PI / 2, MathF.PI / 2));
        }

        /// <summary>
        /// Rotates the given vector3 randomly around a vertical arc (either left or right).
        /// </summary>
        /// <param name="vector3">The vector3 to be rotated.</param>
        /// <returns>The rotated vector3.</returns>
        /// <remarks>
        /// The rotation angle is randomly generated between -90 and 90 degrees, resulting in a vertical arc.
        /// The direction of rotation (left or right) is also randomly chosen.
        /// </remarks>
        public static Vector3 RotateVerticalRandom(this Vector3 vector3) {
            Vector3 arcDirection = new Array<Vector3>() { Vector3.Left, Vector3.Right }.PickRandom();

            return vector3.Rotated(arcDirection, _rng.RandfRange(-MathF.PI / 2, MathF.PI / 2));
        }

        public static System.Numerics.Vector2 ToNumericVector(this Vector2 vector) => new(vector.X, vector.Y);
        public static Vector2 ToGodotVector(this System.Numerics.Vector2 vector) => new(vector.X, vector.Y);
        public static Vector2I ToGodotVectorI(this System.Numerics.Vector2 vector) => new((int)vector.X, (int)vector.Y);
        public static Vector3 ToGodotVector(this System.Numerics.Vector3 vector) => new(vector.X, vector.Y, vector.Z);
        public static Vector3I ToGodotVectorI(this System.Numerics.Vector3 vector) => new((int)vector.X, (int)vector.Y, (int)vector.Z);
        public static Vector4 ToGodotVector(this System.Numerics.Vector4 vector) => new(vector.X, vector.Y, vector.Z, vector.W);
        public static Vector4I ToGodotVectorI(this System.Numerics.Vector4 vector) => new((int)vector.X, (int)vector.Y, (int)vector.Z, (int)vector.W);
        public static System.Numerics.Vector2 ToNumericVector(this Vector2I vector) => new(vector.X, vector.Y);
        public static System.Numerics.Vector3 ToNumericVector(this Vector3 vector) => new(vector.X, vector.Y, vector.Z);
        public static System.Numerics.Vector3 ToNumericVector(this Vector3I vector) => new(vector.X, vector.Y, vector.Z);
        public static System.Numerics.Vector4 ToNumericVector(this Vector4 vector) => new(vector.X, vector.Y, vector.Z, vector.W);
        public static System.Numerics.Vector4 ToNumericVector(this Vector4I vector) => new(vector.X, vector.Y, vector.Z, vector.W);

        /// <summary>
        /// Negates (flips) the sign of each component of a Vector2, resulting in a new vector pointing in the opposite direction.
        /// </summary>
        /// <param name="vector">The Vector2 to flip.</param>
        /// <returns>A new Vector2 with the flipped components.</returns>
        public static Vector2 Flip(this Vector2 vector) => -vector;

        /// <summary>
        /// Negates (flips) the sign of each component of a Vector3, resulting in a new vector pointing in the opposite direction.
        /// </summary>
        /// <param name="vector">The Vector3 to flip.</param>
        /// <returns>A new Vector3 with the flipped components.</returns>
        public static Vector3 Flip(this Vector3 vector) => -vector;
        public static Vector2I Flip(this Vector2I vector) => -vector;
        public static Vector3I Flip(this Vector3I vector) => -vector;
        public static Vector4I Flip(this Vector4I vector) => -vector;

        /// <summary>
        /// Reverses the order of the components in a Vector2, swapping X and Y.
        /// </summary>
        /// <param name="vector">The Vector2 to reverse.</param>
        /// <returns>A new Vector2 with the components swapped (Y becomes X and X becomes Y).</returns>
        public static Vector2 Reverse(this Vector2 vector) => new(vector.Y, vector.X);

        /// <summary>
        /// Reverses the order of the components in a Vector3, swapping X and Y.
        /// </summary>
        /// <param name="vector">The Vector3 to reverse.</param>
        /// <returns>A new Vector3 with the components swapped (Y becomes X and X becomes Y).</returns>
        public static Vector3 Reverse(this Vector3 vector) => new(vector.Z, vector.Y, vector.X);

        /// <summary>
        /// Extracts the X and Z components of a Vector3, representing a top-down 2D vector.
        /// </summary>
        /// <param name="vector">The Vector3 to extract the top-down vector from.</param>
        /// <returns>A new Vector2 containing the X and Z components of the input Vector3 (assuming a top-down view).</returns>
        public static Vector2 GetTopdownVector(this Vector3 vector) => new(vector.X, vector.Z);


        /// <summary>
        /// Extends a Vector2 in a specified direction by a given amount.
        /// </summary>
        /// <param name="vector">The Vector2 to extend.</param>
        /// <param name="angle">The angle (in radians) in which to extend the vector.</param>
        /// <param name="amount">The distance to extend the vector.</param>
        /// <returns>A new Vector2 representing the extended vector.</returns>
        public static Vector2 Extend(this Vector2 vector, float angle, float amount = 1.0f)
            => new(vector.X + amount * Mathf.Cos(angle), vector.Y + amount * Mathf.Sin(angle));

        /// <summary>
        /// Extends a Vector3 in a specified direction by a given amount.
        /// </summary>
        /// <param name="vector">The Vector3 to extend.</param>
        /// <param name="angle">The angle (in radians) in which to extend the vector (assumes XZ plane).</param>
        /// <param name="amount">The distance to extend the vector.</param>
        /// <returns>A new Vector3 representing the extended vector.</returns>
        public static Vector3 Extend(this Vector3 vector, float angle, float amount = 1.0f)
            => new(vector.X + amount * Mathf.Cos(angle), 0f, vector.Z + amount * Mathf.Sin(angle));

        /// <summary>
        /// Determines if two Vector2 objects are within a specified squared distance.
        /// </summary>
        /// <param name="v1">The first Vector2 to compare.</param>
        /// <param name="v2">The second Vector2 to compare.</param>
        /// <param name="distance">The squared distance threshold.</param>
        /// <returns>True if the two vectors are within the specified squared distance, false otherwise.</returns>
        /// <remarks>
        /// This function provides increased efficiency by comparing squared distances directly,
        /// avoiding a potentially expensive square root operation.
        /// </remarks>
        public static bool IsWithinDistanceSquared(this Vector2 v1, Vector2 v2, float distance)
            => v1.DistanceSquaredTo(v2) <= distance * distance;

        /// <summary>
        /// Determines if two Vector3 objects are within a specified squared distance.
        /// </summary>
        /// <param name="v1">The first Vector3 to compare.</param>
        /// <param name="v2">The second Vector3 to compare.</param>
        /// <param name="distance">The squared distance threshold.</param>
        /// <returns>True if the two vectors are within the specified squared distance, false otherwise.</returns>
        /// <remarks>
        /// This function also uses squared distance comparison for efficiency.
        /// </remarks>
        public static bool IsWithinDistanceSquared(this Vector3 v1, Vector3 v2, float distance)
            => v1.DistanceSquaredTo(v2) <= distance * distance;


        public static bool IsNotZeroApprox(this Vector2 vector) => !vector.IsZeroApprox();
        public static bool IsNotZeroApprox(this Vector3 vector) => !vector.IsZeroApprox();
        public static bool IsNotZeroApprox(this Vector4 vector) => !vector.IsZeroApprox();


        /// <summary>
        /// Checks if two Vector3 positions are approximately equal within a specified tolerance.
        /// </summary>
        /// <param name="from">The first Vector3 position.</param>
        /// <param name="to">The second Vector3 position.</param>
        /// <param name="positionPrecision">Optional tolerance value for considering positions equal (default: 0.0003f).</param>
        /// <returns>True if the positions are considered equal within the specified precision, False otherwise.</returns>
        public static bool EqualsPositionApprox(this Vector3 from, Vector3 to, float positionPrecision = 0.0003f) {
            var diff = from - to;

            return Mathf.Abs(diff.X) < positionPrecision && Mathf.Abs(diff.Y) < positionPrecision && Mathf.Abs(diff.Z) < positionPrecision;
        }

        /// <summary>
        /// Checks if the current Vector2 is close to a target Vector2 within a specified threshold.
        /// </summary>
        /// <param name="vector">The Vector2 to check.</param>
        /// <param name="target">The target Vector2 to compare against.</param>
        /// <param name="threshold">The maximum distance considered "close".</param>
        /// <returns>True if the distance between the current vector and the target is less than the threshold, false otherwise.</returns>
        public static bool CloseTo(this Vector2 vector, Vector2 target, float threshold) => vector.DistanceTo(target) < threshold;

        /// <summary>
        /// Checks if the current Vector3 is close to a target Vector3 within a specified threshold.
        /// </summary>
        /// <param name="vector">The Vector3 to check.</param>
        /// <param name="target">The target Vector3 to compare against.</param>
        /// <param name="threshold">The maximum distance considered "close".</param>
        /// <returns>True if the distance between the current vector and the target is less than the threshold, false otherwise.</returns>
        public static bool CloseTo(this Vector3 vector, Vector3 target, float threshold) => vector.DistanceTo(target) < threshold;


        /// <summary>
        /// Generates a sequence of random directions with angles in degrees, confined to a horizontal plane.
        /// 
        /// This function uses Vector2.Rotated with Vector3.Up as the rotation axis to create 
        /// random directions within a 2D plane around the specified origin.
        /// 
        /// - The generated directions will always be perpendicular to the Y-axis.
        /// - Consider potential adjustments if your use case requires full 3D space interpretation 
        ///   of these directions.
        /// </summary>
        /// <param name="origin">The origin point from which the directions are generated.</param>
        /// <param name="numDirections">The number of random directions to generate (default: 10).</param>
        /// <param name="minAngleRange">The range of angles in degrees (default: 0 to 360).</param>
        /// <param name="maxAngleRange">The range of angles in degrees (default: 0 to 360).</param>
        /// <returns>An enumerable sequence of Vector2 representing the random directions.</returns>
        public static IEnumerable<Vector2> GenerateRandomDirectionsOnDegreesAngleRange(this Vector2 origin, int numDirections = 10, float minAngleRange = 0, float maxAngleRange = 360f) {
            return Enumerable.Range(0, numDirections).Select(_ => origin.Rotated(GenerateRandomAngleInDegrees(minAngleRange, maxAngleRange)));
        }


        /// <summary>
        /// Generates a sequence of random directions with angles in radians, confined to a horizontal plane.
        /// 
        /// This function uses Vector2.Rotated with Vector3.Up as the rotation axis to create 
        /// random directions within a 2D plane around the specified origin. Similar to the degrees version.
        /// 
        /// - The generated directions will always be perpendicular to the Y-axis.
        /// - Consider potential adjustments if your use case requires full 3D space interpretation 
        ///   of these directions.
        /// </summary>
        /// <param name="origin">The origin point from which the directions are generated.</param>
        /// <param name="numDirections">The number of random directions to generate (default: 10).</param>
        /// <param name="minAngleRange">The range of angles in radians (default: 0 to 2π).</param>
        /// <param name="maxAngleRange">The range of angles in radians (default: 0 to 2π).</param>
        /// <returns>An enumerable sequence of Vector2 representing the random directions.</returns>
        public static IEnumerable<Vector2> GenerateRandomDirectionsOnRadiansAngleRange(this Vector2 origin, int numDirections = 10, float minAngleRange = 0, float maxAngleRange = 6.2831853072f) {
            return Enumerable.Range(0, numDirections).Select(_ => origin.Rotated(GenerateRandomAngleInRadians(minAngleRange, maxAngleRange)));
        }

        /// <summary>
        /// Generates a sequence of random directions with angles in degrees, rotating around the Y-axis.
        /// 
        /// This function uses Vector3.Rotated with Vector3.Up as the rotation axis to create 
        /// random directions in 3D space, effectively rotating around the Y-axis.
        /// 
        /// - The generated directions will be distributed within a full circle around the Y-axis.
        /// </summary>
        /// <param name="origin">The origin point from which the directions are generated.</param>
        /// <param name="numDirections">The number of random directions to generate (default: 10).</param>
        /// <param name="minAngleRange">The range of angles in degrees (default: 0 to 360).</param>
        /// <param name="maxAngleRange">The range of angles in degrees (default: 0 to 360).</param>
        /// <returns>An enumerable sequence of Vector3 representing the random directions.</returns>
        public static IEnumerable<Vector3> GenerateRandomDirectionsOnDegreesAngleRange(this Vector3 origin, int numDirections = 10, float minAngleRange = 0, float maxAngleRange = 360f) {
            return Enumerable.Range(0, numDirections).Select(_ => origin.Rotated(Vector3.Up, GenerateRandomAngleInDegrees(minAngleRange, maxAngleRange)));
        }

        /// <summary>
        /// Generates a sequence of random directions with angles in radians, rotating around the Y-axis.
        /// 
        /// This function uses Vector3.Rotated with Vector3.Up as the rotation axis to create 
        /// random directions in 3D
        /// </summary>
        public static IEnumerable<Vector3> GenerateRandomDirectionsOnRadiansAngleRange(this Vector3 origin, int numDirections = 10, float minAngleRange = 0, float maxAngleRange = 6.2831853072f) {
            return Enumerable.Range(0, numDirections).Select(_ => origin.Rotated(Vector3.Up, GenerateRandomAngleInRadians(minAngleRange, maxAngleRange)));
        }

        public static float GenerateRandomAngleInRadians(float minAngle = 0, float maxAngle = 6.2831853072f) {
            return minAngle + GD.Randf() * (maxAngle - minAngle);
        }

        public static float GenerateRandomAngleInDegrees(float minAngle = 0, float maxAngle = 360f) {
            return minAngle + GD.Randf() * (maxAngle - minAngle);
        }


        /// <summary>
        /// Generates a random and **normalized** 2D direction vector with values 
        /// ranging from -1 to 1 (inclusive) for both x and y components.
        /// 
        /// This function ensures even distribution across all directions and guarantees 
        /// a magnitude of 1 for the resulting vector.
        /// </summary>
        /// <returns>A random normalized 2D direction vector.</returns>
        public static Vector2 GenerateRandomDirection(this Vector2 _) {
            return new Vector2(_rng.RandfRange(-1f, 1f), _rng.RandfRange(-1f, 1f));
        }

        /// <summary>
        /// Generates a random **normalized** 2D direction vector with integer components 
        /// ranging from -1 to 1. 
        /// 
        /// **Note:** This approach also has limitations and does not cover 
        /// the entire spectrum of directions due to the use of integer components.
        /// </summary>
        /// <returns>A random normalized 2D vector with integer components between -1 and 1.</returns>
        public static Vector2 GenerateRandomFixedDirection(this Vector2 _) {
            return new Vector2(_rng.RandiRange(-1, 1), _rng.RandiRange(-1, 1)).Normalized();
        }

        /// <summary>
        /// Generates a random and **normalized** 2D direction vector with values 
        /// ranging from -1 to 1 (inclusive) for both x and y components.
        /// 
        /// This function ensures even distribution across all directions and guarantees 
        /// a magnitude of 1 for the resulting vector.
        /// </summary>
        /// <returns>A random normalized 3D direction vector.</returns>
        public static Vector3 GenerateRandomDirection(this Vector3 _) {
            RandomNumberGenerator rng = new();

            return new Vector3(_rng.RandfRange(-1f, 1f), _rng.RandfRange(-1f, 1f), _rng.RandfRange(-1f, 1f));
        }

        /// <summary>
        /// Generates a random **normalized** 3D direction vector with integer components 
        /// ranging from -1 to 1. 
        /// 
        /// **Note:** This approach also has limitations and does not cover 
        /// the entire spectrum of directions due to the use of integer components.
        /// </summary>
        /// <returns>A random normalized 3D vector with integer components between -1 and 1.</returns>
        public static Vector3 GenerateRandomFixedDirection(this Vector3 _)
            => new Vector3(_rng.RandiRange(-1, 1), _rng.RandiRange(-1, 1), _rng.RandiRange(-1, 1)).Normalized();

        /// <summary>
        /// Converts a rotation value (in radians) to a normalized direction vector (2D).
        /// </summary>
        /// <param name="rotation">The rotation value in radians.</param>
        /// <returns>A new Vector2 representing the direction based on the rotation.</returns>
        public static Vector2 DirectionFromRotation(this Vector2 _, float rotation)
            => new(Mathf.Cos(rotation * Mathf.Pi / Mathf.Pi), Mathf.Sin(rotation * Mathf.Pi / Mathf.Pi));

        /// <summary>
        /// Converts a rotation value (in radians) to a normalized direction vector (3D), with Z-axis set to zero.
        /// </summary>
        /// <param name="rotation">The rotation value in radians.</param>
        /// <param name="_">Unused boolean parameter (allows method overloading).</param>
        /// <returns>A new Vector3 representing the direction based on the rotation (Z is zero).</returns>
        public static Vector3 DirectionFromRotation(this Vector3 _, float rotation) //  Boolean to allow use the same function name
            => new(Mathf.Cos(rotation * Mathf.Pi / Mathf.Pi), 0.0f, Mathf.Sin(rotation * Mathf.Pi / Mathf.Pi));

        /// <summary>
        /// Converts a rotation value (in degrees) to a normalized direction vector (2D).
        /// </summary>
        /// <param name="rotation">The rotation value in degrees.</param>
        /// <returns>A new Vector2 representing the direction based on the rotation.</returns>
        public static Vector2 DirectionFromRotationDegrees(this Vector2 _, float rotation) {
            float degrees = Mathf.DegToRad(rotation);
            float piDegrees = Mathf.RadToDeg(Mathf.Pi);

            return new(Mathf.Cos(degrees * piDegrees / piDegrees), Mathf.Sin(degrees * piDegrees / piDegrees));
        }

        /// <summary>
        /// Converts a rotation value (in degrees) to a normalized direction vector (3D).
        /// </summary>
        /// <param name="rotation">The rotation value in degrees.</param>
        /// <returns>A new Vector2 representing the direction based on the rotation.</returns>
        public static Vector3 DirectionFromRotationDegrees(this Vector3 _, float rotation) {
            float degrees = Mathf.DegToRad(rotation);
            float piDegrees = Mathf.RadToDeg(Mathf.Pi);

            return new(Mathf.Cos(degrees * piDegrees / piDegrees), 0.0f, Mathf.Sin(degrees * piDegrees / piDegrees));
        }

        /// <summary>
        /// Calculates the signed angle between two normalized Vector2 directions.
        /// </summary>
        /// <param name="_">Unused self parameter (likely due to extension method syntax).</param>
        /// <param name="vectorStart">The starting direction vector.</param>
        /// <param name="vectorEnd">The ending direction vector.</param>
        /// <returns>The signed angle in radians between the two directions.</returns>
        public static float SideAngleByVectors(this Vector2 _, Vector2 vectorStart, Vector2 vectorEnd) {
            vectorStart = vectorStart.NormalizeVector();
            vectorEnd = vectorEnd.NormalizeVector();

            return -Mathf.Atan2(-vectorStart.Y * vectorEnd.X + vectorStart.X * vectorEnd.Y, vectorStart.X * vectorEnd.X + vectorStart.Y * vectorEnd.Y);
        }

        /// <summary>
        /// Calculates the signed angle between two angles in radians.
        /// </summary>
        /// <param name="_">Unused self parameter (likely due to extension method syntax).</param>
        /// <param name="startAngle">The starting angle in radians.</param>
        /// <param name="endAngle">The ending angle in radians.</param>
        /// <returns>The signed angle in radians between the two angles.</returns>
        public static float SideAngleByAngles(this Vector2 _, float startAngle, float endAngle) {
            startAngle = startAngle.NormalizeRadiansAngle();
            endAngle = endAngle.NormalizeRadiansAngle();

            Vector2 start = new(startAngle, startAngle);
            Vector2 end = new(endAngle, endAngle);

            return Vector2.One.SideAngleByVectors(start, end);
        }

        public static Vector2 Scale(this Vector2 vector, float lenght) => vector.NormalizeVector() * lenght;
        public static Vector3 Scale(this Vector3 vector, float lenght) => vector.NormalizeVector() * lenght;
        public static Vector4 Scale(this Vector4 vector, float lenght) => vector.Normalized() * lenght;
        public static Color Color(this Vector3 vector) => new(vector.X, vector.Y, vector.Z);
        public static Color Color(this Vector4 vector) => new(vector.X, vector.Y, vector.Z, vector.W);

        /// <summary>
        /// Translates an X-axis value (-1.0f or 1.0f) to a corresponding 2D direction vector.
        /// 
        /// This function accurately handles near-equal comparisons using `Mathf.Epsilon`
        /// to account for potential floating-point imprecision.
        /// 
        /// - If the input value is close to -1.0f (within `Mathf.Epsilon`), the function returns Vector2.Left.
        /// - If the input value is close to 1.0f (within `Mathf.Epsilon`), the function returns Vector2.Right.
        /// </summary>
        /// <param name="axis">The X-axis value (-1.0f or 1.0f) to translate.</param>
        /// <returns>The corresponding 2D direction vector (Vector2.Left or Vector2.Right), 
        /// or a default value (optional) if the input is invalid.</returns>
        public static Vector2 TranslateXAxisToVector(this Vector2 _, float axis) {
            if (axis >= 1.0f)
                return Vector2.Right;
            else if (axis <= -1.0f)
                return Vector2.Left;
            else
                return Vector2.Zero;
        }

        /// <summary>
        /// Translates an X-axis value (-1.0f or 1.0f) to a corresponding 2D direction vector.
        /// 
        /// This function accurately handles near-equal comparisons using `Mathf.Epsilon`
        /// to account for potential floating-point imprecision.
        /// 
        /// - If the input value is close to -1.0f (within `Mathf.Epsilon`), the function returns Vector2.Up.
        /// - If the input value is close to 1.0f (within `Mathf.Epsilon`), the function returns Vector2.Down.
        /// </summary>
        /// <param name="axis">The X-axis value (-1.0f or 1.0f) to translate.</param>
        /// <returns>The corresponding 2D direction vector (Vector2.Up or Vector2.Down), 
        /// or a default value (optional) if the input is invalid.</returns>
        public static Vector2 TranslateYAxisToVector(this Vector2 _, float axis) {
            if (axis >= 1.0f)
                return Vector2.Down;
            else if (axis <= -1.0f)
                return Vector2.Up;
            else
                return Vector2.Zero;
        }
        /// <summary>
        /// Normalizes a Vector2, handling diagonal directions for proper scaling.
        /// </summary>
        /// <param name="vector">The Vector2 to normalize.</param>
        /// <returns>A normalized Vector2, with special handling for diagonal directions.</returns>
        public static Vector2 NormalizeVector(this Vector2 vector) {
            Vector2 result = NormalizeDiagonalVector(vector);

            if (result.IsEqualApprox(vector))
                return vector.IsNormalized() ? vector : result.Normalized();

            return result;
        }

        /// <summary>
        /// Normalizes a Vector2, assuming it represents a diagonal direction and applying a specific scale factor.
        /// </summary>
        /// <param name="vector">The Vector2 representing a diagonal direction.</param>
        /// <returns>A normalized Vector2 considering the diagonal direction.</returns>
        public static Vector2 NormalizeDiagonalVector(this Vector2 vector) {
            if (IsDiagonalDirection(vector))
                return vector * Mathf.Sqrt(2);

            return vector;
        }

        /// <summary>
        /// Checks if a Vector2 represents a diagonal direction (both X and Y components are non-zero).
        /// </summary>
        /// <param name="direction">The Vector2 to check.</param>
        /// <returns>True if the vector represents a diagonal direction, False otherwise.</returns>
        private static bool IsDiagonalDirection(this Vector2 direction) {
            return direction.X.IsNotZero() && direction.Y.IsNotZero();
        }

        /// <summary>
        /// Normalizes a Vector3, handling diagonal directions for proper scaling.
        /// </summary>
        /// <param name="vector">The Vector3 to normalize.</param>
        /// <returns>A normalized Vector3, with special handling for diagonal directions.</returns>
        public static Vector3 NormalizeVector(this Vector3 vector) {
            Vector3 result = NormalizeDiagonalVector(vector);

            if (result.IsEqualApprox(vector))
                return vector.IsNormalized() ? vector : result.Normalized();

            return result;
        }

        /// <summary>
        /// Normalizes a Vector3, assuming it represents a diagonal direction and applying a specific scale factor.
        /// </summary>
        /// <param name="vector">The Vector3 representing a diagonal direction.</param>
        /// <returns>A normalized Vector3 considering the diagonal direction.</returns>
        public static Vector3 NormalizeDiagonalVector(this Vector3 vector) {
            if (IsDiagonalDirection(vector))
                return vector * Mathf.Sqrt(3f);

            return vector;
        }

        /// <summary>
        /// Checks if a Vector3 represents a diagonal direction (all X, Y, and Z components are non-zero).
        /// </summary>
        /// <param name="direction">The Vector3 to check.</param>
        /// <returns>True if the vector represents a diagonal direction, False otherwise.</returns>
        private static bool IsDiagonalDirection(this Vector3 direction) {
            return direction.X.IsNotZero() && direction.Y.IsNotZero() && direction.Z.IsNotZero();
        }

        /// <summary>
        /// Calculates the Manhattan distance between two 2D points.
        /// 
        /// The Manhattan distance is the sum of the absolute differences between the corresponding 
        /// coordinates of the two points.
        /// </summary>
        /// <param name="a">The first 2D point.</param>
        /// <param name="b">The second 2D point.</param>
        /// <returns>The Manhattan distance between the two points.</returns>
        public static float DistanceManhattanV2(this Vector2 a, Vector2 b) {
            return Mathf.Abs(a.X - b.X) + Mathf.Abs(a.Y - b.Y);
        }

        /// <summary>
        /// Calculates the Chebyshev distance between two 2D points.
        /// 
        /// The Chebyshev distance is the maximum absolute difference between the corresponding 
        /// coordinates of the two points.
        /// </summary>
        /// <param name="a">The first 2D point.</param>
        /// <param name="b">The second 2D point.</param>
        /// <returns>The Chebyshev distance between the two points.</returns>
        public static float DistanceChebysevV2(this Vector2 a, Vector2 b) {
            return Mathf.Max(Mathf.Abs(a.X - b.X), Mathf.Abs(a.Y - b.Y));
        }

        /// <summary>
        /// Calculates the Manhattan length of a 2D vector.
        /// 
        /// The Manhattan length of a vector is the sum of the absolute values of its components.
        /// This function is equivalent to `DistanceManhattanV2(a, Vector2.Zero)`.
        /// </summary>
        /// <param name="a">The 2D vector.</param>
        /// <returns>The Manhattan length of the vector.</returns>
        public static float LengthManhattanV2(this Vector2 a) {
            return Mathf.Abs(a.X) + Mathf.Abs(a.Y);
        }

        /// <summary>
        /// The Chebyshev length of a vector should be the maximum absolute value of its components.
        /// </summary>
        /// <param name="a">The 2D vector.</param>
        /// <returns>The Chebyshev length of the vector.</returns>
        public static float LengthChebysevV2(this Vector2 a) {
            return Mathf.Max(Mathf.Abs(a.X), Mathf.Abs(a.Y));
        }


        /// <summary>
        /// Finds the closest point on a line segment (defined by points `a` and `b`) to another point `c`, 
        /// clamping the result to lie within the segment.
        /// 
        /// This function calculates the projection of `c` onto the line segment defined by `a` and `b`. 
        /// The `Mathf.Clamp` function ensures the returned point lies on the segment between `a` and `b`.
        /// </summary>
        /// <param name="a">The first point defining the line segment.</param>
        /// <param name="b">The second point defining the line segment.</param>
        /// <param name="c">The point to find the closest point to on the line segment.</param>
        /// <returns>The closest point on the line segment to point `c`, clamped to lie within the segment.</returns>
        public static Vector2 ClosestPointOnLineClampedV2(this Vector2 a, Vector2 b, Vector2 c) {
            b = (b - a).Normalized();
            c -= a;

            return a + b * Mathf.Clamp(c.Dot(b), 0.0f, 1.0f);
        }

        /// <summary>
        /// Finds the closest point on a line segment (defined by points `a` and `b`) to another point `c`.
        /// 
        /// This function calculates the projection of `c` onto the line segment defined by `a` and `b`. 
        /// The returned point may lie outside the line segment itself.
        /// </summary>
        /// <param name="a">The first point defining the line segment.</param>
        /// <param name="b">The second point defining the line segment.</param>
        /// <param name="c">The point to find the closest point to on the line segment.</param>
        /// <returns>The closest point on the line segment to point `c`.</returns>
        public static Vector2 ClosestPointOnLineV2(this Vector2 a, Vector2 b, Vector2 c) {
            b = (b - a).Normalized();
            c -= a;

            return a + b * c.Dot(b);
        }

        /// <summary>
        /// Calculates the Manhattan distance between two 3D points.
        /// 
        /// The Manhattan distance is the sum of the absolute differences between the corresponding 
        /// coordinates of the two points.
        /// </summary>
        /// <param name="a">The first 3D point.</param>
        /// <param name="b">The second 3D point.</param>
        /// <returns>The Manhattan distance between the two points.</returns>
        public static float DistanceManhattanV3(this Vector3 a, Vector3 b) {
            return Mathf.Abs(a.X - b.X) + Mathf.Abs(a.Y - b.Y) + Mathf.Abs(a.Z - b.Z);
        }

        /// <summary>
        /// Calculates the Chebyshev distance between two 3D points.
        /// 
        /// The Chebyshev distance is the maximum absolute difference between the corresponding 
        /// coordinates of the two points.
        /// </summary>
        /// <param name="a">The first 3D point.</param>
        /// <param name="b">The second 3D point.</param>
        /// <returns>The Chebyshev distance between the two points.</returns>
        public static float DistanceChebysevV3(this Vector3 a, Vector3 b) {
            return Mathf.Max(Mathf.Abs(a.X - b.X), Mathf.Max(Mathf.Abs(a.Y - b.Y), Mathf.Abs(a.Z - b.Z)));
        }

        /// <summary>
        /// Calculates the Manhattan length of a 3D vector.
        /// 
        /// The Manhattan length of a vector is the sum of the absolute values of its components.
        /// </summary>
        /// <param name="a">The 3D vector.</param>
        /// <returns>The Manhattan length of the vector.</returns>
        public static float LengthManhattanV3(this Vector3 a) {
            return Mathf.Abs(a.X) + Mathf.Abs(a.Y) + Mathf.Abs(a.Z);
        }

        /// <summary>
        /// Calculates the Chebyshev length of a 3D vector.
        /// 
        /// The Chebyshev length of a vector is the maximum absolute value of its components.
        /// </summary>
        /// <param name="a">The 3D vector.</param>
        /// <returns>The Chebyshev length of the vector.</returns>
        public static float LengthChebysevV3(this Vector3 a, Vector3 b) {
            return Mathf.Max(Mathf.Abs(a.X), Mathf.Max(Mathf.Abs(a.Y), Mathf.Abs(a.Z)));
        }

        /// <summary>
        /// Calculates the normalized projection of a point `c` onto the direction vector representing the line segment defined by points `a` and `b`.
        /// 
        /// This function does not consider the actual endpoints of the line segment and simply calculates the projection of `c` onto the normalized direction vector `b - a`.
        /// The result is a scalar value representing the distance along the normalized line direction from point `a` to the projection of `c`.
        /// </summary>
        /// <param name="a">The first point defining the line segment.</param>
        /// <param name="b">The second point defining the line segment.</param>
        /// <param name="c">The point to project onto the line direction.</param>
        /// <returns>The projection of point `c` onto the line direction vector, representing the distance along the line from point `a`.</returns>
        public static Vector3 ClosestPointOnLineClampedV3(this Vector3 a, Vector3 b, Vector3 c) {
            b = (b - a).Normalized();
            c -= a;

            return a + b * Mathf.Clamp(c.Dot(b), 0.0f, 1.0f);
        }

        /// <summary>
        /// Calculates the normalized projection of a point `c` onto the direction vector representing the line segment defined by points `a` and `b`.
        /// 
        /// This function does not consider the actual endpoints of the line segment and simply calculates the projection of `c` onto the normalized direction vector `b - a`.
        /// The result is a scalar value representing the distance along the normalized line direction from point `a` to the projection of `c`.
        /// </summary>
        /// <param name="a">The first point defining the line segment.</param>
        /// <param name="b">The second point defining the line segment.</param>
        /// <param name="c">The point to project onto the line direction.</param>
        /// <returns>The projection of point `c` onto the line direction vector, representing the distance along the line from point `a`.</returns>
        public static Vector3 ClosestPointOnLineV3(this Vector3 a, Vector3 b, Vector3 c) {
            b = (b - a).Normalized();
            c -= a;

            return a + b * c.Dot(b);
        }

        /// <summary>
        /// Calculates the normalized projection of a point `c` onto the direction vector representing the line segment defined by points `a` and `b`.
        /// 
        /// This function does not consider the actual endpoints of the line segment and simply calculates the projection of `c` onto the normalized direction vector `b - a`.
        /// The result is a scalar value representing the distance along the normalized line direction from point `a` to the projection of `c`.
        /// </summary>
        /// <param name="a">The first point defining the line segment.</param>
        /// <param name="b">The second point defining the line segment.</param>
        /// <param name="c">The point to project onto the line direction.</param>
        /// <returns>The normalized projection of point `c` onto the line direction vector, representing the distance along the line from point `a`.</returns>
        public static float ClosestPointOnLineNormalizedV3(this Vector3 a, Vector3 b, Vector3 c) {
            b -= a;
            c -= a;

            return c.Dot(b.Normalized()) / b.Length();
        }

    }

}