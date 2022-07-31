using DG.Tweening;
using TMPro;
using UnityEngine;
using static UnityEngine.Vector3;

public class CellPanel : InventoryPanel
{
    protected override void ExtendedStart()
    {
        Cell.OnClickInventory += loadInventory;
    }

    protected override void ExtendedDestroy()
    {
        Cell.OnClickInventory -= loadInventory;
    }
}