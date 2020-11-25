using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Interfaces are similar to abstract classes in that they force implementations, while an abstract class can be thought of as a partial template, Interfaces can be see as a contract
// Can implement as many interfaces as you like to fake inheritance
// the REAL reason interfaces are cool is it can be used as an identifier like the unity tag system.  Check out Main.cs to see

public interface ISelectable<T>
{
    //GameObject storyBlock { get; set; }

    T GetStoryGO();
}