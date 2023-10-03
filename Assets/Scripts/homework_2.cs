using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class MyClass
{
    public int i;
    public int j;
}

public class MyList<T>
{
    private T[] m_array;
    public int Count { get; private set; }

    public int Capacity
    {
        get { return m_array.Length; } // берем текущий массив
        set // если значение, что равно 4, больше кол-ву элементов в массиве, то урезаем массив
        {
            if (value > m_array.Length)
            {
                Array.Resize(ref m_array, value);
            }
        }
    }
    public MyList() // присваиваем массиву размер
    {
        m_array = new T[4];
    }

    public MyList(int capacity) // перезаписываем новый массив с величиной Capacity
    {
        m_array = new T[capacity];
    }

    public T this[int index]
    {
        get
        {
            CheckIndexRange(index);
            return m_array[index];
        }
        set
        {
            CheckIndexRange(index);
            m_array[index] = value;
        }
    }
    private bool CheckIndexRange(int index) // проверка на пределы массива
    {
        if (index < 0 || index >= Count) // если вышел за пределы, меньше нуля или больше чем эл. в массиве
            throw new ArgumentOutOfRangeException(); // то выдаем сообщение

        return true;
    }
    private void IncreaseCapacityIfNeed() //метод, что вызывется при необходимости увеличить развер памяти под массив (2^n)
    {
        if (Count == Capacity)
        {
            Capacity *= 2;
        }
    }

    public void Add(T item) // добавоение элементов
    {
        IncreaseCapacityIfNeed(); // проверка, что добавленные цисла поместятся
        m_array[Count] = item; // в конец добавляем число
        Count++;
    }

    public void Insert(int index, T item) // добавлнеие числа в определенный индекс
    {
        if (index < 0 || index > Count) // проверка, что число не ушло за рамки дозволенного
        {
            throw new ArgumentOutOfRangeException();
        }

        IncreaseCapacityIfNeed(); // проверка, что есть куда вставлять число

        if (index < Count) // если индекс числа меньше, чем кол-во элементов в массиве
        {
            Array.Copy(m_array, index, m_array, index + 1, Count - index); // отодвигаем массив с указанного индекса вправо (+1) до разницы кол-ва элементов в массиве и индекса. 
        }

        Count++;
        m_array[index] = item;
    }

    public int IndexOf(T item) // поиск числа item в массиве 
    {
        for (int i = 0; i < Count; i++)
        {
            //if (m_array[i] == item)
            
                return i; // true
            
        }
        return -1; // false
    }

    public bool Remove(T item) // удаление числа по числу
    {
        int index = IndexOf(item); // проверка, что такое число есть и присвоить его индекс
        if (index >= 0) // если не -1, то заходим
        {
            RemoveAt(index); // обращаемся к методу удаление по индексу 
            return true;
        }

        return false;
    }

    public void RemoveAt(int index) // удаление числа по индексу 
    {
        CheckIndexRange(index); // проверка,что заданный индекс не выл за границы
        Count--; // уменьшаем массив
        if (index < Count) // если заданный индекс не является последним...
        {
            Array.Copy(m_array, index + 1, m_array, index, Count - index); // то смещаем массив вправо начиная от след числа (i+1) (не включая удаляемый индекс)и копируем в другой начиная с указанного индекса.
        } // Тем самым перезаписываем удаляемый индекс
    }

    public bool Contains(T item) // булевая проверка, что значение есть в массиве
    {
        return IndexOf(item) >= 0;
    }

    public void Clear() // очистка массива. (какой массив, с какого индекса, до какого индекса). Назначаем Count 0, ибо массив без элементов.
    {
        Array.Clear(m_array, 0, m_array.Length);
        Count = 0;
    }
}


public class homework_2 : MonoBehaviour
{
    public void Start()
    {
        // начало дженерик листа
        MyList<MyClass> myList = new MyList<MyClass>();
        Stopwatch sw = Stopwatch.StartNew();

        for (int i = 0; i < 1000000; i++)
        {
            myList.Add(new MyClass());
        }
        UnityEngine.Debug.Log($"Operation add for Generic List: {sw.ElapsedMilliseconds} ms");
        //myList.Capacity = 4;
        if (myList.Contains(new MyClass()) == true)
        {
            myList.Remove(new MyClass());
        }
        myList.Remove(new MyClass());
        myList.RemoveAt(500);
        myList.Insert(1, new MyClass());
        myList.Clear();
        sw.Stop();
        UnityEngine.Debug.Log($"Execution time for Generic List: {sw.ElapsedMilliseconds} ms");
        // конец дженерик листа

        // начало обычного листа
        List<MyClass> standartList = new List<MyClass>();
        sw = Stopwatch.StartNew();

        for (int i = 0; i < 1000000; i++)
        {
            standartList.Add(new MyClass());
        }
        UnityEngine.Debug.Log($"Operation add for Standart List: {sw.ElapsedMilliseconds} ms");
        //standartList.Capacity = 4; //error выход за границы массива
        if (standartList.Contains(new MyClass()) == true)
        {
            standartList.Remove(new MyClass());
        }
        standartList.Remove(new MyClass());
        standartList.RemoveAt(500);
        standartList.Insert(1, new MyClass());
        standartList.Clear();
        sw.Stop();
        UnityEngine.Debug.Log($"Execution time for Standart List: {sw.ElapsedMilliseconds} ms");
        // конец обычного листа
    }
}
