using System;
using UnityEngine;
using static UnityEngine.Random;

[ExecuteAlways]
public class Field : MonoBehaviour
{
    [SerializeField] private uint _height;
    [SerializeField] private uint _length;
    [SerializeField] private Cell _cellPrefab;

    private const int _default = -100;

    private Cell[,] _field;
    private Material _fieldMaterial;

    private void Start()
    {
        _fieldMaterial = gameObject.GetComponent<Terrain>().materialTemplate;
        _fieldMaterial.EnableKeyword("POS");

        Cell.onPointerClick += Light;
        Timer.spawnSpawner += SpawnSpawner;
    }

    private void OnDestroy()
    {
        Cell.onPointerClick -= Light;
        Timer.spawnSpawner -= SpawnSpawner;
    }

    public void Rebuild()
    {
        DeleteCells();
        _field = new Cell[_length, _height];
        for (int x = 0; x < _field.GetLength(0); x++)
        {
            for (int z = 0; z < _field.GetLength(1); z++)
            {
                _field[x, z] = CreateCell(x, z);
            }
        }
    }

    private void DeleteCells()
    {
        Cell[] field = FindObjectsOfType<Cell>();
        for (int x = 0; x < field.Length; x++)
        {
            DestroyImmediate(field[x].gameObject);
        }
    }

    private Cell CreateCell(int x, int z)
    {
        Vector3 position = PosFromCoordinates(x, z);

        Cell cell = Instantiate(_cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        return cell;
    }

    private Vector3 PosFromCoordinates(int x, int z)
    {
        Vector3 position = new Vector3();

        position.x = x * (CellMetrics.innerRadius * 2f);
        position.x = (x + z * 0.5f) * (CellMetrics.innerRadius * 2f);
        position.x = (x + z * 0.5f - z / 2) * (CellMetrics.innerRadius * 2f);
        position.x += 1.56f;
        position.y = 0f;
        position.z = z * (CellMetrics.outerRadius * 1.5f);

        return position;
    }

    private Cell ChooseRandomCell()
    {
        int x = Range(0, _field.GetLength(0));
        int z = Range(0, _field.GetLength(1));

        if (_field[x, z] != null && _field[x, z].CheckSpawn())
        {
            return _field[x, z];
        }
        return null;
    }

    private void AddResourceSpawner(Cell cell)
    {
        if (cell.CheckSpawn())
        {
            cell.gameObject.AddComponent<ResourceSpawner>();
        }
    }

    private void SpawnSpawner()
    {
        Cell cell = ChooseRandomCell();
        if (cell != null)
        {
            AddResourceSpawner(cell);
        }
    }

    private void Light(Cell cell)
    {
        Vector3 position = cell.GetTransform().position;
        Vector4 shaderPos = new Vector4(position.x, position.z, 0, 0);
        _fieldMaterial.SetVector("_Pos", shaderPos);
    }

    public void Unlight()
    {
        Vector4 position = new Vector4(_default, _default, 0, 0);
        _fieldMaterial.SetVector("_Pos", position);
    }
}