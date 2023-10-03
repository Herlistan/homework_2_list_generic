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
        get { return m_array.Length; } // ����� ������� ������
        set // ���� ��������, ��� ����� 4, ������ ���-�� ��������� � �������, �� ������� ������
        {
            if (value > m_array.Length)
            {
                Array.Resize(ref m_array, value);
            }
        }
    }
    public MyList() // ����������� ������� ������
    {
        m_array = new T[4];
    }

    public MyList(int capacity) // �������������� ����� ������ � ��������� Capacity
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
    private bool CheckIndexRange(int index) // �������� �� ������� �������
    {
        if (index < 0 || index >= Count) // ���� ����� �� �������, ������ ���� ��� ������ ��� ��. � �������
            throw new ArgumentOutOfRangeException(); // �� ������ ���������

        return true;
    }
    private void IncreaseCapacityIfNeed() //�����, ��� ��������� ��� ������������� ��������� ������ ������ ��� ������ (2^n)
    {
        if (Count == Capacity)
        {
            Capacity *= 2;
        }
    }

    public void Add(T item) // ���������� ���������
    {
        IncreaseCapacityIfNeed(); // ��������, ��� ����������� ����� ����������
        m_array[Count] = item; // � ����� ��������� �����
        Count++;
    }

    public void Insert(int index, T item) // ���������� ����� � ������������ ������
    {
        if (index < 0 || index > Count) // ��������, ��� ����� �� ���� �� ����� ������������
        {
            throw new ArgumentOutOfRangeException();
        }

        IncreaseCapacityIfNeed(); // ��������, ��� ���� ���� ��������� �����

        if (index < Count) // ���� ������ ����� ������, ��� ���-�� ��������� � �������
        {
            Array.Copy(m_array, index, m_array, index + 1, Count - index); // ���������� ������ � ���������� ������� ������ (+1) �� ������� ���-�� ��������� � ������� � �������. 
        }

        Count++;
        m_array[index] = item;
    }

    public int IndexOf(T item) // ����� ����� item � ������� 
    {
        for (int i = 0; i < Count; i++)
        {
            //if (m_array[i] == item)
            
                return i; // true
            
        }
        return -1; // false
    }

    public bool Remove(T item) // �������� ����� �� �����
    {
        int index = IndexOf(item); // ��������, ��� ����� ����� ���� � ��������� ��� ������
        if (index >= 0) // ���� �� -1, �� �������
        {
            RemoveAt(index); // ���������� � ������ �������� �� ������� 
            return true;
        }

        return false;
    }

    public void RemoveAt(int index) // �������� ����� �� ������� 
    {
        CheckIndexRange(index); // ��������,��� �������� ������ �� ��� �� �������
        Count--; // ��������� ������
        if (index < Count) // ���� �������� ������ �� �������� ���������...
        {
            Array.Copy(m_array, index + 1, m_array, index, Count - index); // �� ������� ������ ������ ������� �� ���� ����� (i+1) (�� ������� ��������� ������)� �������� � ������ ������� � ���������� �������.
        } // ��� ����� �������������� ��������� ������
    }

    public bool Contains(T item) // ������� ��������, ��� �������� ���� � �������
    {
        return IndexOf(item) >= 0;
    }

    public void Clear() // ������� �������. (����� ������, � ������ �������, �� ������ �������). ��������� Count 0, ��� ������ ��� ���������.
    {
        Array.Clear(m_array, 0, m_array.Length);
        Count = 0;
    }
}


public class homework_2 : MonoBehaviour
{
    public void Start()
    {
        // ������ �������� �����
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
        // ����� �������� �����

        // ������ �������� �����
        List<MyClass> standartList = new List<MyClass>();
        sw = Stopwatch.StartNew();

        for (int i = 0; i < 1000000; i++)
        {
            standartList.Add(new MyClass());
        }
        UnityEngine.Debug.Log($"Operation add for Standart List: {sw.ElapsedMilliseconds} ms");
        //standartList.Capacity = 4; //error ����� �� ������� �������
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
        // ����� �������� �����
    }
}
