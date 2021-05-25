using System.Collections;
using System.Collections.Generic;
public class Utility 
{
    public static T[] ShuffleArray<T>(T[] array, int seed)
    {
        System.Random prng = new System.Random(seed);

        for (int i = 0; i < array.Length - 1; i++)
        {
            int randomIndex = prng.Next(i, array.Length);
            T _tempItem = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = _tempItem;
        }
        return array;
    }

    public static T GetRandomElementFromQueue<T>(Queue<T> queue)
    {
        T elem = queue.Dequeue();
        int seed = UnityEngine.Random.Range(0, int.MaxValue);
        queue = new Queue<T>(Utility.ShuffleArray(queue.ToArray(), seed));
        queue.Enqueue(elem);
        return elem;
    }
}
