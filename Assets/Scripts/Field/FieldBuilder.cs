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

    //Batching sucks big big dick!!!
    /*public void BatchCells()
    {
        Mesh mesh1 = new Mesh();
        Mesh mesh2 = new Mesh();
        Vector3[] cachedVertices = new Vector3[7000];
        foreach (Transform child in transform)
        {
            MeshFilter[] meshFilters = child.GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];

            GameObject empty = new GameObject();
            empty.transform.SetParent(child);
            empty.AddComponent<MeshFilter>();
            empty.AddComponent<MeshRenderer>();

            int i = 0;
            if (meshFilters.Length > 0)
            {
                empty.GetComponent<MeshRenderer>().sharedMaterial = meshFilters[0].gameObject.GetComponent<MeshRenderer>().sharedMaterial;
                while (i < meshFilters.Length)
                {
                    combine[i].mesh = meshFilters[i].sharedMesh;
                    combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                    DestroyImmediate(meshFilters[i].gameObject.GetComponent<MeshRenderer>());
                    DestroyImmediate(meshFilters[i].gameObject.GetComponent<MeshFilter>());

                    i++;
                }
                if (meshFilters.Length == 1)
                {
                    empty.GetComponent<MeshFilter>().sharedMesh = new Mesh();
                    empty.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine);
                    empty.gameObject.SetActive(true);
                    cachedVertices = empty.GetComponent<MeshFilter>().sharedMesh.vertices;
                    if (mesh1.vertexCount == empty.GetComponent<MeshFilter>().sharedMesh.vertexCount)
                    {
                        DestroyImmediate(empty.GetComponent<MeshFilter>().sharedMesh);
                        empty.GetComponent<MeshFilter>().sharedMesh = new Mesh();
                        empty.GetComponent<MeshFilter>().sharedMesh.vertices = cachedVertices;
                        empty.GetComponent<MeshFilter>().sharedMesh.triangles = mesh1.triangles;
                        empty.GetComponent<MeshFilter>().sharedMesh.normals = mesh1.normals;
                        empty.GetComponent<MeshFilter>().sharedMesh.colors = mesh1.colors;
                        empty.GetComponent<MeshFilter>().sharedMesh.tangents = mesh1.tangents;
                        empty.GetComponent<MeshFilter>().sharedMesh.uv = mesh1.uv;
                        empty.GetComponent<MeshFilter>().sharedMesh.UploadMeshData(true);
                    }
                    else
                    {
                        mesh1 = empty.GetComponent<MeshFilter>().sharedMesh;
                    }
                }
                if (meshFilters.Length == 2)
                {
                    empty.GetComponent<MeshFilter>().sharedMesh = new Mesh();
                    empty.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine);
                    empty.gameObject.SetActive(true);
                    cachedVertices = empty.GetComponent<MeshFilter>().sharedMesh.vertices;
                    if (mesh2.vertexCount == empty.GetComponent<MeshFilter>().sharedMesh.vertexCount)
                    {
                        DestroyImmediate(empty.GetComponent<MeshFilter>().sharedMesh);
                        empty.GetComponent<MeshFilter>().sharedMesh = new Mesh();
                        empty.GetComponent<MeshFilter>().sharedMesh.vertices = cachedVertices;
                        empty.GetComponent<MeshFilter>().sharedMesh.triangles = mesh2.triangles;
                        empty.GetComponent<MeshFilter>().sharedMesh.normals = mesh2.normals;
                        empty.GetComponent<MeshFilter>().sharedMesh.colors = mesh2.colors;
                        empty.GetComponent<MeshFilter>().sharedMesh.tangents = mesh2.tangents;
                        empty.GetComponent<MeshFilter>().sharedMesh.uv = mesh2.uv;
                        empty.GetComponent<MeshFilter>().sharedMesh.UploadMeshData(true);
                    }
                    else
                    {
                        mesh2 = empty.GetComponent<MeshFilter>().sharedMesh;
                    }
                }
            }
            else
            {
                DestroyImmediate(empty);
            }
            combine = null;
            meshFilters = null;
        }
    }*/

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
