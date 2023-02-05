using System;

public static class Utils
{
    public static T[] SubArray<T>(this T[] data, int index, int length)
    {
        if (index < 0 || length < 0 || index + length > data.Length)
        {
            throw new ArgumentException("Invalid index or length.");
        }

        T[] result = new T[length];
        Array.Copy(data, index, result, 0, length);
        return result;
    }

    public static int[] GetTwoRandomNetworks(NeuralNetwork[] array)
    {
        Random random = new Random();
        int[] randomPositions = new int[2];
        do {
            randomPositions[0] = random.Next(0, array.Length);
            randomPositions[1] = random.Next(0, array.Length);
        }while(array[randomPositions[0]] == null || array[randomPositions[1]] == null);
        return randomPositions;
    }

    public static int[] GetTwoRandomNumbers(int a, int b)
    {
        Random random = new Random();
        int num1 = random.Next(a, b + 1);
        int num2 = random.Next(a, b + 1);

        return new int[] { num1, num2 };
    }
}