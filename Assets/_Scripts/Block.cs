using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Normal,
    Shielded,
    Powerup
}

[RequireComponent(typeof(Rigidbody))]
public class Block : MonoBehaviour
{
    public static List<Block> blocks { get; private set; } = new List<Block>();

    [field: SerializeField] public BlockType blockType { get; private set; }
    [field: SerializeField] public Renderer visualRenderer { get; private set; }
    [field: SerializeField] public Powerup powerupPrefab { get; private set; }

    byte maxHp;
    byte hp;

    private void Awake()
    {
        blocks.Add(this);
    }

    // private void OnDestroy()
    // {
    // }

    public void Setup(BlockType newBlockType, Color color)
    {
        this.blockType = newBlockType;

        switch (newBlockType)
        {
            case BlockType.Normal:
                maxHp = 1;
                visualRenderer.material.color = color;
                break;

            case BlockType.Shielded:
                maxHp = 2;
                visualRenderer.material.color = Color.red;
                break;

            case BlockType.Powerup:
                maxHp = 1;
                visualRenderer.material.color = Color.green;
                break;

            default:
                Debug.LogWarningFormat("Default state. Block setup  like - {0}", BlockType.Normal);
                Setup(BlockType.Normal, Color.black);
                break;
        }

        hp = maxHp;
    }

    public void TakeHit(byte damage)
    {
        hp -= damage;
        if (hp == 0)
        {
            if (blockType == BlockType.Powerup) Spawner.CreateOnTemplate<Powerup>(powerupPrefab, transform.position);
            Crash();
        }
    }

    public Color GetColor()
    {
        return visualRenderer.material.color;
    }

    protected virtual void Crash()
    {
        blocks.Remove(this);
        EventsManager.BlockDestroy(blockType);
        if (blocks.Count == 0) EventsManager.ChangeGameState(GameState.Win);
        Destroy(gameObject);
    }

    public SaveData GetData()
    {
        return new SaveData(transform.position, transform.localScale, blockType, GetColor());
    }

    [System.Serializable]
    public struct SaveData
    {
        public readonly Vector3 position;
        public readonly Vector3 scale;
        public readonly BlockType type;
        public readonly Color color;

        public SaveData(Vector3 position, Vector3 scale, BlockType type, Color color)
        {
            this.position = position;
            this.scale = scale;
            this.type = type;
            this.color = color;
        }
    }
}
