using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factorial : MonoBehaviour
{
    void Start()
    {
        Debug.Log(CountFactorial(0));
    }

    void Update()
    {

    }

    public int CountFactorial(int value)
    {
        if (value <= 0)
        {
            return value;
        }
        else
        {
            return (value * CountFactorial(value - 1));
        }
    }
}
