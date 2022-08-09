using UnityEngine;

public enum BlockType
{
    Normal,
    Shielded
}

[RequireComponent(typeof(Rigidbody))]
public class Block : MonoBehaviour
{
    [field: SerializeField] public BlockType blockType { get; private set; }
    [field: SerializeField] public Renderer visualRenderer { get; private set; }

    byte maxHp;
    byte hp;

    public void Setup(BlockType blockType, Color color)
    {
        switch (blockType)
        {
            case BlockType.Normal:
                maxHp = 1;
                hp = maxHp;
                visualRenderer.material.color = color;
                break;

            case BlockType.Shielded:
                maxHp = 2;
                hp = maxHp;
                visualRenderer.material.color = Color.red;
                break;

            default:
                Debug.LogWarningFormat("Default state. Block setup  like - {0}", BlockType.Normal);
                Setup(BlockType.Normal, Color.black);
                break;
        }
    }

    public void TakeHit(byte damage)
    {
        hp -= damage;
        if (hp == 0) Crash();
    }

    protected virtual void Crash()
    {
        EventsManager.BlockDestroy(blockType);
        Destroy(gameObject);
    }
}
