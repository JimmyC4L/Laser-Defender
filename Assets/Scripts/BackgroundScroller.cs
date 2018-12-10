using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BackgroundScroller : MonoBehaviour
{
    [FormerlySerializedAs("backgroundScrollSpeed")] [SerializeField]
    private float _backgroundScrollSpeed = 0.5f;

    private Material _material;
    private Vector2 _offSet;

    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        _offSet = new Vector2(0, _backgroundScrollSpeed);
    }

    private void Update()
    {
        _material.mainTextureOffset += _offSet * Time.deltaTime;
    }
}