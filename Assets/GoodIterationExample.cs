using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodIterationExample : MonoBehaviour {

    // This is an array of contiguous Vector3 values
    Vector3[] ArrayOfValues;

    void ValueLoop()
    {

        for (int i = 0; i < ArrayOfValues.Length - 1; i++)
        {
            for (int j = i + 1; j < ArrayOfValues.Length; j++)
            {
                // Each fetch will grab a BUNCH of this array at a time.
                // This means fewer cache misses and MUCH faster processing.
                bool isHigher = ArrayOfValues[i].y > ArrayOfValues[j].y;

                // Modern CPUs also have a lot of optimization for sequential
                // memory access like this and will often pre-fetch blocks in
                // anticipation that you will want them in the future.
            }
        }

    }

}
