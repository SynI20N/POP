using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    private void Start()
    {
        
    }

    public void Close()
    {
        _panel.SetActive(false);
    }

    public void Open()
    {
        _panel.SetActive(true);
    }
}
