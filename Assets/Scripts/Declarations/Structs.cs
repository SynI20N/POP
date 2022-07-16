using UnityEngine;

public struct Plane
{
    public Vector3 Forward;
    public Vector3 Backward;
    public Vector3 Right;
    public Vector3 Left;
}

public struct TouchInfo
{
    public bool Pressed;
    public Vector2 Direction { get => (CurrentPos - InitPos).normalized; }
    public float Distance { get => (CurrentPos - InitPos).magnitude; }
    public Vector2 InitPos;
    public Vector2 CurrentPos;
}

public struct CellMetrics
{
    public const float outerRadius = 1.75f;
    public const float innerRadius = outerRadius * 0.866025404f;
}

public struct ItemNotification
{
    public Item Item;
    public Vector3 Position;

    public ItemNotification(Item someItem, Vector3 somePosition)
    {
        Item = someItem;
        Position = somePosition;
    }
}