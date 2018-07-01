using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Structs are 99.9% like Classes in C#.
// The thing that matters to us today is that structs are VALUE objects, not REFERENCE
public struct UnitPositionData
{
    public Vector3 position;
    public Quaternion rotation;
}

// What does this have to do with ECS anyway?
// Hint... We could have called this class "EntityManager" instead.
public static class UnitManager
{
    // Various bookeeping for our arrays
    static int currentCapacity = 0;
    static int firstEmptyIndex = 0;

    // We need some way to keep track of which entities are active/inactive.
    // A boolean like this is great for when we're iterating through a list...
    static bool[] isAlive;
    // ...but using only this would lead to fragmentation as things die and are created.
    // We would just grow our array, despite most of it being full of dead things!
    // So a queue of dead unit indexes lets use reuse parts of our array.
    static Queue<int> deadUnits;

    // Various bits of info that a unit might have.
    // You could almost call these values COMPONENTS that belong to an ENTITY
    static int[] hitpoints;
    static int[] armor;

    // Since things like position and rotation are almost always used together,
    // we can consider grouping them into a single struct to organize our data.
    // Unlike an array of classes (which is an array of pointers to RANDOM memory),
    // an array of structs is CONTIGUOUS and fast to iterate through!
    static UnitPositionData[] positionData;

    // Wouldn't it be **extremely** convenient to have all these various types
    // of array be in some kind of structure like a dictionary that we set/access
    // using generics and whatnot?
    //  HINT 1: You can copy code we wrote in the Events & Callbacks tutorial to do this.
    //  HINT 2: This is basically what the ECS system does for us!

    static public int CreateUnit()
    {
        if(currentCapacity <= firstEmptyIndex)
        {
            // We've reached the end of the array -- can we recycle empty spots?
            if (deadUnits == null || deadUnits.Count == 0)
            {
                GrowUnitArray();
            }
            else
            {
                // Recycle the first dead unit spot
                int deadSpot = deadUnits.Dequeue();
                isAlive[deadSpot] = true;
                return deadSpot;
            }
        }

        isAlive[firstEmptyIndex] = true;
        return firstEmptyIndex++;
    }

    static public void KillUnit(int i)
    {
        // Flag as dead so we skip in our iterations
        isAlive[i] = false;

        // Add to our dead list to facilitate recycling
        deadUnits.Enqueue(i);
    }

    // We need some way for the rest of the program to interact
    // with our "entity's" "component" values. Here's an example.
    // Again: A system that uses generic would be a lot nicer than
    //        writing a ton of read/write accessors!
    static public void SetUnitHitpoints(int unitIndex, int h)
    {
        if (unitIndex < 0 || unitIndex >= currentCapacity)
            return;

        hitpoints[unitIndex] = h;
    }

    const int INITIAL_ARRAY_SIZE = 10;
    const int GROW_FACTOR = 2;

    static void GrowUnitArray()
    {
        // Again, a great case for using a dictionary of generic arrays instead of
        // having to update this function every time we come up with a 
        // new "component" for our "entities" to have!

        if (currentCapacity == 0)
        {
            currentCapacity = INITIAL_ARRAY_SIZE;
            isAlive      = new bool[currentCapacity];
            hitpoints    = new int[currentCapacity];
            armor        = new int[currentCapacity];
            positionData = new UnitPositionData[currentCapacity];
            return;
        }

        currentCapacity = currentCapacity * GROW_FACTOR;

        Array.Resize(ref isAlive, currentCapacity);
        Array.Resize(ref hitpoints, currentCapacity);
        Array.Resize(ref armor, currentCapacity);
        Array.Resize(ref positionData, currentCapacity);
    }

    // Hint... since this function only operates on a single piece
    // of data (or "component") and other functions might operate
    // on other, completely independent pieces of data...this is
    // a good candidate for offloading to a separate job thread!
    static public void AnUpdateExample()
    {
        // isAlive[] is a contiguous block of data.
        // positionData[] (and its .position field) is a contigious block of data
        // This function should be EXTREMELY cache friendly, and therefore very fast!


        for (int i = 0; i < currentCapacity - 1; i++)
        {
            if (isAlive[i] == false)
                continue;

            for (int j = i + 1; j < currentCapacity; j++)
            {
                if (isAlive[j] == false)
                    continue;

                
                bool isHigher = positionData[i].position.y > positionData[j].position.y;

            }
        }

    }

}