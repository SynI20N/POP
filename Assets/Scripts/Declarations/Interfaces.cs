using UnityEngine;

public interface ISpawnable
{
    public bool CheckSpawn();
    public void SetAbility(bool ability);
}
public interface ICellObjects
{
    public void AddObject(GameObject gameObject);
}

public interface ISpawner
{
    public void Spawn(Cell cell);
}