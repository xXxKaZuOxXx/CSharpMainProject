using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFirstScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int N = 5;
        Debug.Log(SUM(N));
        Debug.Log(PROIZVED(N));

            
    }

    int SUM( int N)
    {
        int summa = 0;
        for (int i = 1; i <= N; i++)
        {
            summa += i;
        }
        return summa;
    }

    int PROIZVED(int N)
    {
        int summa = 1;
        for(int i = 1;i <= N;i++)
        {
            summa *= i;
        }
        return summa;

    }

    

  
}
