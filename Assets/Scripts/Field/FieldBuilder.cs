using UnityEngine;

[ExecuteInEditMode]
public class FieldBuilder : EnvironmentSpawner
{
    [SerializeField] private uint _height;
    [SerializeField] private uint _length;
    [SerializeField] private Cell _cellPrefab;

    public void Rebuild()
    {
        SpawnHelper.ClearChildrenIn(transform);
        for (int x = 0; x < _length; x++)
        {
            for (int z = 0; z < _height; z++)
            {
                CreateCell(x, z);
            }
        }
    }

    private Cell CreateCell(int x, int z)
    {
        Vector3 position = PosFromCoordinates(x, z);

        Cell cell = Instantiate(_cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.position = position;
        SpawnEnvironment(cell);
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

    public uint GetHeight()
    {
        return _height;
    }

    public uint GetLength()
    {
        return _length;
    }
}
