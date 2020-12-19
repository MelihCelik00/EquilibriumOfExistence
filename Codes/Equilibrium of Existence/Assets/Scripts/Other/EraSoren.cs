using System;
using UnityEngine;

namespace Eoe.Other {
    public static class Ext {
        public static int Sign(float sign)
        {
            //Mathf.Sign gibi, ama 0 da var
            if (sign > 0f) return 1;
            else if (sign < 0f)  return -1;
            else return 0;
        }

        public static float Clamp(float value, float float1, float float2)
        {
            //Mathf.Clamp, ama hangi değerin min, hangisinin max olduğunu kendi buluyor
            return Mathf.Clamp(value,Mathf.Min(float1,float2),Mathf.Max(float1,float2));
        }

        public static Vector3 Lengthdir (float length, float direction)
        {
            return new Vector3(Mathf.Cos(Mathf.Deg2Rad * direction),Mathf.Sin(Mathf.Deg2Rad * direction)).normalized * length;
        }

        public static void CircularMovement(this Rigidbody2D rb, Vector3 pos, Vector3 center, float moveSpeed)
        {
            Quaternion q = Quaternion.AngleAxis (moveSpeed, Vector3.forward);
            rb.MovePosition (q * (pos - center) + center);
        }

        public static float Format(float number, int digits)
        {
            int divide = 1;
            Fnd.Repeat(digits,() => divide = divide * 10);
            return Mathf.Round(number * divide) / divide;
        }
    }

    public static class Fnd 
    {

        public static void Repeat(int times, Action ExecuteActions)
        {
            for (int i = 0; i < times; i++) {
                ExecuteActions();
            }
        }
    }
}
