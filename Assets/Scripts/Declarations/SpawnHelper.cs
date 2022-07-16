using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Random;

public class SpawnHelper : MonoBehaviour
{
    private const uint _initHeight = 5;
    private const float _initRadius = 0.75f;
    public static Vector3 PosInsideCircle(GameObject someObject)
    {
        Vector3 result = new Vector3();
        result.x = someObject.transform.position.x;
        result.y = _initHeight;
        result.z = someObject.transform.position.z;

        Vector2 insideCircle = insideUnitCircle * _initRadius;
        result.x += insideCircle.x;
        result.z += insideCircle.y;

        return result;
    }

    public static void ClearChildrenIn(Transform transform)
    {
        int i = 0;
        GameObject[] allChildren = new GameObject[transform.childCount];

        foreach (Transform child in transform)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }
        foreach (GameObject child in allChildren)
        {
            DestroyImmediate(child.gameObject);
        }
    }

    public static Item CopyComponent(Item original, GameObject destination)
    {
        Item copy = destination.AddComponent<Item>();
        copy.SetImage(original.GetImage());
        copy.Amount = new Amount(original.Amount);
        return copy;
    }
}
