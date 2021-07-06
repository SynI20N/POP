using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPanel : MonoBehaviour
{
    private GameObject _panel;

    private void Start()
    {
        _panel = gameObject;

        Close();
        Cell.onPointerClick += Open;
    }

    private void OnDestroy()
    {
        Cell.onPointerClick -= Open;
    }

    public void Close()
    {
        _panel.SetActive(false);
    }

    public void Open(Cell cell)
    {
        _panel.SetActive(true);
    }
}
