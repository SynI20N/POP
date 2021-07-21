using UnityEngine;
using static UnityEngine.Random;

[RequireComponent(typeof(FieldBuilder))]
public class Field : MonoBehaviour
{
    private const int _default = -100;

    private Cell[,] _field;
    private Material _fieldMaterial;
    private FieldBuilder _fieldBuilder;

    private void Start()
    {
        _fieldMaterial = gameObject.GetComponent<MeshRenderer>().material;
        _fieldBuilder = GetComponent<FieldBuilder>();
        _fieldMaterial.EnableKeyword("POS");

        Cell.onPointerClick += Light;
        Timer.spawnSpawner += AddResourceSpawner;

        FindField();
    }

    private void OnDestroy()
    {
        Cell.onPointerClick -= Light;
        Timer.spawnSpawner -= AddResourceSpawner;
    }

    private void FindField()
    {
        uint length = _fieldBuilder.GetLength();
        uint height = _fieldBuilder.GetHeight();
        _field = new Cell[length, height];
        Cell[] field = FindObjectsOfType<Cell>();
        int i = 0;
        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < height; y++)
            {
                _field[x, y] = field[i];
                i++;
            }
        }
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

    private void AddResourceSpawner()
    {
        Cell cell = ChooseRandomCell();
        if (cell.CheckSpawn())
        {
            cell.gameObject.AddComponent<ResourceSpawner>();
        }
    }

    private void Light(Cell cell)
    {
        Vector3 position = cell.GetPosition();
        Vector4 shaderPos = new Vector4(position.x, position.z, 0, 0);
        _fieldMaterial.SetVector("_Pos", shaderPos);
    }

    public void Unlight()
    {
        Vector4 position = new Vector4(_default, _default, 0, 0);
        _fieldMaterial.SetVector("_Pos", position);
    }
}