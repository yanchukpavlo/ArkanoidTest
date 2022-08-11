using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour, ISaveable
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
    [SerializeField][Range(0f, 0.5f)] float specialBlockPercent = 0.1f;

    Transform blockParent;

    private void Awake()
    {
        EventsManager.OnGameStateChange += GameStateChanged;
    }

    private void OnDestroy()
    {
        EventsManager.OnGameStateChange -= GameStateChanged;
    }

    void GameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.GameStart:
                Generate();
                break;

            case GameState.MainMenu:
                DestroyBlocksParent();
                break;

            case GameState.Win:
                Generate(1f);
                break;
        }
    }

    [ContextMenu("Generate")]
    void Generate()
    {
        LevelGenerateStrategy strategy = new GenerateStrategyNoise(spawnThreshold, bloksSpacePercent, width, high, colums, rows, xOffset, yOffset);
        SetBlocksParent();

        var data = strategy.GetSpawnPositions();

        foreach (var item in data)
        {
            BlockType blockType = BlockType.Normal;
            if (Random.value < specialBlockPercent)
            {
                if(Random.value < 0.3f) blockType = BlockType.Powerup;
                else blockType = BlockType.Shielded;
            }

            CreateBlock(item.position, strategy.Scale).Setup(blockType, item.color);
        }
    }

    void Generate(float timer)
    {
        StartCoroutine(WaitCoroutine());

        IEnumerator WaitCoroutine()
        {
            yield return new WaitForSeconds(timer);
            Generate();
        }
    }    

    void Generate(Block.SaveData[] blocksData)
    {
        SetBlocksParent();

        foreach (var item in blocksData)
        {
            CreateBlock(item.position, item.scale).Setup(item.type, item.color);
        }
    }

    void SetBlocksParent()
    {
        DestroyBlocksParent();
        blockParent = new GameObject("Block Holder").transform;
    }

    void DestroyBlocksParent()
    {
        if (blockParent)
        {
            if (Application.isPlaying) Destroy(blockParent.gameObject);
            else DestroyImmediate(blockParent.gameObject);
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

    public object CaptureState()
    {
        var blocks = Block.blocks;
        Block.SaveData[] blocksData = new Block.SaveData[blocks.Count];
        for (int i = 0; i < blocksData.Length; i++)
        {
            blocksData[i] = blocks[i].GetData();
        }

        return blocksData;
    }

    public void RestoreState(object state)
    {
        Block.SaveData[] blocksData = (Block.SaveData[])state;
        Generate(blocksData);
    }
}

public class GenerateInfo
{
    public Vector3 position;
    public Color color;

    public GenerateInfo(Vector3 position, Color color)
    {
        this.position = position;
        this.color = color;
    }
}