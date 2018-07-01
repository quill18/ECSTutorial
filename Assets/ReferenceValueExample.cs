using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MyClass
{
    public int MyInt;
}

public class ReferenceValueExample : MonoBehaviour {

	void Start () {

        // a gets allocated on Start()'s stack
        int a = 1;
        PassedByValue(a);
        Debug.Log(a); // Is still 1

        // b (a pointer value) is instantiated on Start()'s stack,
        // while an instance of MyClass is created on the heap.
        MyClass b = new MyClass();
        b.MyInt = 2;
        PassedByReference(b);
        Debug.Log(b.MyInt); // b is unchanged, but b.MyInt is now 200!
    }

    void PassedByValue( int i )
    {
        // The VALUE of "1" was placed on our PassedByValue()'s stack, 
        // and "i" is linked to it.

        i = 100;    // This only changes the value of i on PassedByValue()'s stack
    }

    void PassedByReference( MyClass c )
    {
        // NOTE: Technically this is being bassed by "Value" as well,
        //       it's just that the "Value" is a pointer to an object
        //       on the heap.

        // This changes the value of Start().b's MyInt, because b and c
        // both contain a pointer to the same object on the heap!!
        c.MyInt = 200;

        // This has no effect whatsoever on Start().b -- it's just
        // that our c now contains a pointer to a different object on the heap
        // (this is the same as when we did i = 100 in PassedByValue()
        c = new MyClass(); 
    }


	
}
