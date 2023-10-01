using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Homework_2_generic : MonoBehaviour
{
    public void Start()
    {
        MyGenericList();
        MyArrayList();
    }
    static void MyGenericList()
    {
        List<int> list = new List<int>();
        Stopwatch sw = Stopwatch.StartNew();
        for (int i = 0; i < 1000000; i++)
        {
            list.Add(i);
        }
        sw.Stop();
        UnityEngine.Debug.Log($"Execution time for Generic List: {sw.ElapsedMilliseconds} ms" );
    }
    static void MyArrayList()
    {
        ArrayList arraylist = new ArrayList();
        Stopwatch sw = Stopwatch.StartNew();
        for (int i = 0; i < 1000000; i++)
        {
            arraylist.Add(i);
        }
        sw.Stop();
        UnityEngine.Debug.Log($"Execution time for Array List: {sw.ElapsedMilliseconds} ms");
    }
}
