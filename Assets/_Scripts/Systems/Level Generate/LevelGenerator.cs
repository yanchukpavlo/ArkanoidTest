using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] Block blockPrefab;

    [Space]
    [SerializeField] float width = 20;
    [SerializeField] float high = 12;
    [SerializeField] int colums = 10;
    [SerializeField] int rows = 7;
    [SerializeField] float xOffset = 0;
    [SerializeField] float yOffset = 0;
    [SerializeField][Range(0.3f, 0.75f)] float bloksSpacePercent = 0.7f;
    
    [Space]
    [SerializeField][Range(0f, 0.6f)] float spawnThreshold = 0.2f;

    Transform blockParent;

    private void Start() {
        Generate();
    }

    [ContextMenu("Generate")]
    void Generate()
    {
        LevelGenerateStrategy strategy = new GenerateStrategyNoise(spawnThreshold, bloksSpacePercent, width, high, colums, rows, xOffset, yOffset);

        if (blockParent)
        {
            if (Application.isPlaying) Destroy(blockParent.gameObject);
            else DestroyImmediate(blockParent.gameObject);
        }
        blockParent = new GameObject("Block Holder").transform;
        
        var data = strategy.GetSpawnPositions();

        foreach (var item in data)
        {
            CreateBlock(item.position, strategy.Scale).Setup(BlockType.Normal, item.color);
        }
    }

    Block CreateBlock(Vector3 position, Vector3 scale)
    {
        Block block = Instantiate(blockPrefab, position, Quaternion.identity);
        Transform blockTr = block.transform;

        blockTr.localScale = scale;
        blockTr.parent = blockParent;

        return block;
    }
}

public class GenerateData
{
    public Vector3 position;
    public Color color;

    public GenerateData(Vector3 position, Color color)
    {
        this.position = position;
        this.color = color;
    }
}