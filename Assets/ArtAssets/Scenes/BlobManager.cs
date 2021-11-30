using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobManager : MonoBehaviour
{
    private int _stackCount;
    private int _stackIndex;
    [SerializeField ]private float distance;
    public GameObject[] _stackContainer;

    private void Start()
    {
        _stackCount = transform.childCount;//stack i�indeki eleman say�s�
        _stackContainer = new GameObject[_stackCount];


        for (int i = 0; i < _stackCount; i++)
        {
            _stackContainer[i] = transform.GetChild(i).gameObject;
        }
        _stackIndex = _stackCount - 1;

    }

    private void Update()
    {
        TransferStack();
    }

    private void TransferStack()
    {


        if (_stackIndex == _stackCount)
        {
            _stackIndex = 0;

        }
        if (Input.GetMouseButtonDown(0))
        {
            _stackContainer[_stackIndex].transform.localPosition = new Vector3(0, 0, _stackContainer[_stackIndex].transform.position.z +distance);
            _stackIndex += 1;

        }


    }
}
