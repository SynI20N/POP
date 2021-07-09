using System;
using UnityEngine;
using UnityEngine.EventSystems;


//to add Spawner use /*AddResourceSpawner(_field[x, z]);*/
public class Field : MonoBehaviour
{
    [SerializeField] private int _hight;
    [SerializeField] private int _length;

    [SerializeField] private Cell _cellPrefab;   

    private Cell[,] _field;

    private Material _fieldMaterial;

    private void Awake()
    {
        _field = new Cell[_length, _hight];
        CreateField();
    }

    private void Start()
    {
        _fieldMaterial = gameObject.GetComponent<Terrain>().materialTemplate;
        _fieldMaterial.EnableKeyword("POS");
        Cell.OnClick += Light;
        Timer.spawnSpawner += SpawnSpawner;
    }

    private void CreateField()
    {
        for (int x = 0; x < _length; x += 1)
        {
            for (int z = 0; z < _hight; z += 1)
            {
                _field[x, z] = CreateCell(x, z);
            }
        }
    }

    private Cell CreateCell(int x, int z)
    {
        Vector3 position = FromCellCoordinates(x, z);

        Cell cell = Instantiate<Cell>(_cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        return cell;
    }

    private Vector3 FromCellCoordinates(int x, int z)
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
        int x = 0;
        int z = 0;       
        
        x = UnityEngine.Random.Range(0, _length);
        z = UnityEngine.Random.Range(0, _hight);

        if (_field[x, z].CheckSpawn())
        {
            return _field[x, z];
        }
        else
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
        if(cell != null)
        {
            AddResourceSpawner(cell);
        }
    }

    private void Light(float x, float z)
    {
        Vector4 position = new Vector4(x, z, 0, 0);
        _fieldMaterial.SetVector("_Pos", position);
    }
}