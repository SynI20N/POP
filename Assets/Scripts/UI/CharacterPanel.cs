using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanel : InventoryPanel
{
    protected override void ExtendedStart()
    {
        Character.OnCharacterClick += loadInventory;
    }

    protected override void ExtendedDestroy()
    {
        Character.OnCharacterClick -= loadInventory;
    }
}
