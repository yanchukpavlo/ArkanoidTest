using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] Block blockPrefab;
    [SerializeField] float width = 20;
    [SerializeField] float high = 12;
    [SerializeField] int colums = 10;
    [SerializeField] int rows = 7;
    [SerializeField] float xOffset = 0;
    [SerializeField] float yOffset = 0;
    [SerializeField] float freeSpacePercent = 0.3f;


}
