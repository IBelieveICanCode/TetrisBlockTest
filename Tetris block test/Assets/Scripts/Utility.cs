using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility 
{
    public static T[] ShuffleArray<T>(T[] array, int seed)
    {
        System.Random _prng = new System.Random(seed);

        for (int i = 0; i < array.Length - 1; i++)
        {
            int randomIndex = _prng.Next(i, array.Length);
            T _tempItem = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = _tempItem;
        }
        return array;
    }
}
