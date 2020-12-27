using UnityEditor;
using UnityEngine;

public class VectorUtils
{
    //Calculate 2D (X,Z) distance between two positions, discard Y.
    public static float Vector2Distance(Vector3 position1, Vector3 position2)
    {
        var vectorToTarget = position1 - position2;
        vectorToTarget.y = 0;
        return vectorToTarget.magnitude;
    }
}
