using UnityEngine;
using System;
using System.Collections;

namespace ObjectAttribute
{
    public enum Voltage { None, Positive, Ground };

    [Serializable]
    public class SystemFile
    {
        Voltage volt = Voltage.None;

        public Voltage Volt
        {
            get { return volt; }
            set { volt = value; }
        }

        // Number
        int number;

        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        // Vector arrays
        float[] x = new float[2];
        float[] y = new float[2];
        float[] z = new float[2];

        private void Vector3Pos1(Vector3 vector3)
        {
            x[0] = vector3.x;
            y[0] = vector3.y;
            z[0] = vector3.z;
        }

        private void Vector3Pos2(Vector3 vector3)
        {
            x[1] = vector3.x;
            y[1] = vector3.y;
            z[1] = vector3.z;
        }

        public Vector3 Position1
        {
            get { return new Vector3(x[0], y[0], z[0]); }
            set { Vector3Pos2(value); }
        }

        public Vector3 Position2
        {
            get { return new Vector3(x[1], y[1], z[1]); }
            set { Vector3Pos1(value); }
        }
    }
}
