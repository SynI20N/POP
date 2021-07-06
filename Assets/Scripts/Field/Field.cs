using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Field : Observer
{
    [SerializeField] private int _hight;
    [SerializeField] private int _length;

    [SerializeField] private Cell _cellPrefab;

    private Cell[,] _field;

    private void Awake()
    {
        _field = new Cell[_length, _hight];
        CreateField();
    }

    private void Start()
    {
        foreach (var cell in )
        {
            cell.RegisterObserver(this);
        }
    }

    private void CreateField()
    {
        for (int x = 0; x < _length; x+=1)
        {
            for (int z = 0; z < _hight; z+=1)
            {
                CreateCell(x, z);
            }
        }
    }

    private void CreateCell(int x, int z)
    {
        Vector3 position = FromCellCoordinates(x, z);

        Cell cell = _field[x , z] = Instantiate<Cell>(_cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
    }

    private Vector3 FromCellCoordinates(int x, int z)
    {
        Vector3 position = new Vector3();

        position.x = x * (CellMetrics.innerRadius * 2f);
        position.x = (x + z * 0.5f) * (CellMetrics.innerRadius * 2f);
        position.x = (x + z * 0.5f - z / 2) * (CellMetrics.innerRadius * 2f);

        position.y = 0f;

        position.z = z * (CellMetrics.outerRadius * 1.5f);

        return position;
    }

    public override void OnNotify(object value, NotificationType notificationType)
    {
        if(notificationType == NotificationType.CellPressed)
        {

        }
    }
}
