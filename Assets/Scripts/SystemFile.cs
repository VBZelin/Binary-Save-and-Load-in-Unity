using UnityEngine;
using System.Collections;

[System.Serializable]
public class SystemFile
{
    // Number
    int number;

    public int Number
    {
        get { return number; }
        set { number = value; }
    }

    // Vector
    float x, y, z;

    private void Vector3Pos(Vector3 vector3)
    {
        x = vector3.x;
        y = vector3.y;
        z = vector3.z;
    }

    public Vector3 Position
    {
        get { return new Vector3(x, y, z); }
        set { Vector3Pos(value); }
    }
}
