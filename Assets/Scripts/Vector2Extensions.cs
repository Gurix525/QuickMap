using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class Vector2Extensions
{
    public static Vector2 Round(this Vector2 input, float precision = 1F)
    {
        return new Vector2(
            Mathf.Round(input.x / precision) * precision,
            Mathf.Round(input.y / precision) * precision);
    }
}