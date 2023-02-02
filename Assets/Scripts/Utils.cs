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
}