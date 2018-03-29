using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimeNumber : MonoBehaviour
{
    int amount = 100;

    void Start()
    {
        for (int i = 0; i < amount; ++i)
        {
            CheckPrime(3);
        }
    }

    bool CheckPrime(int value)
    {
        if (CheckPrime(value))
        {
            Debug.Log(value + "is a prime number.");
            return CheckPrime(value + 1);
        }
        else
        {
            Debug.Log(value + "is not a prime number.");
            return false;
        }

    }
}
