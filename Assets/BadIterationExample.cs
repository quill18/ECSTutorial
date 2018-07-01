using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadIterationExample : MonoBehaviour {

    // This is an array of contiguous pointers to objects
    // scattered randomly throughout the heap
    GameObject[] ArrayOfReferences;
	
	void ReferenceLoop () {

        for (int i = 0; i < ArrayOfReferences.Length-1; i++)
        {
            for (int j = i+1; j < ArrayOfReferences.Length; j++)
            {
                // We need to constantly read from two completely random and
                // unrelated locations in heap memory.
                // Cache misses are frequent and SLOW
                bool isHigher = ArrayOfReferences[i].transform.position.y > ArrayOfReferences[j].transform.position.y;
            }
        }

    }
}
