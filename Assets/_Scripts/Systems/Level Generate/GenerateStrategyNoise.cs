using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateStrategyNoise : LevelGenerateStrategy
{
    float width = 20;
    float high = 12;
    int colums = 10;
    int rows = 7;
    float xOffset = 0;
    float yOffset = 0;
    float bloksSpacePercent = 0.7f;
    float spawnThreshold = 0.2f;

    public override Vector3 Scale { get; set; }

    public GenerateStrategyNoise(float spawnThreshold, float bloksSpacePercent, float width, float high, int colums, int rows, float xOffset, float yOffset)
    {
        this.spawnThreshold = spawnThreshold;
        this.bloksSpacePercent = bloksSpacePercent;
        this.width = width;
        this.high = high;
        this.colums = colums;
        this.rows = rows;
        this.xOffset = xOffset;
        this.yOffset = yOffset;

        float xDelta = width / colums;
        float yDelta = high * bloksSpacePercent / rows;
        Scale = new Vector3(xDelta - xOffset, yDelta - yOffset, 1);
    }

    public override List<GenerateData> GetSpawnPositions()
    {
        float xDelta = width / colums;
        float yDelta = high * bloksSpacePercent / rows;

        List<GenerateData> spawnData = new List<GenerateData>();
        Vector3 point = new Vector3(-width * 0.5f + xDelta * 0.5f, high - yDelta * 0.5f, 0);
        float[,] noise = Get2DNoise(rows, colums, new Vector2(Random.Range(0, 10000), Random.Range(0, 10000)));

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < colums; j++)
            {
                float noiseValue = noise[i, j];
                if (noiseValue > spawnThreshold) spawnData.Add(new GenerateData(point, GetNoiseColor(noiseValue)));//CreateBlock(point, scale).Setup(BlockType.Normal, GetNoiseColor(noiseValue));
                point.x += xDelta;
            }

            point.x = -width * 0.5f + xDelta * 0.5f;
            point.y -= yDelta;
        }

        return spawnData;
    }

    float[,] Get2DNoise(int width, int high, Vector2 offset, float scale = 0.5f)
    {
        float[,] values = new float[width, high];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < high; y++)
            {
                float samplePosX = (float)x * scale + offset.x;
                float samplePosY = (float)y * scale + offset.y;
                values[x, y] = Mathf.PerlinNoise(samplePosX, samplePosY);
            }
        }

        return values;
    }

    Color GetNoiseColor(float value)
    {
        return new Color(value, value, value);
    }

    Texture2D GetTextureFromNoise(float[,] noise)
    {
        int xLength = noise.GetUpperBound(0) + 1;
        int yLength = noise.GetUpperBound(1) + 1;
        Texture2D texture = new Texture2D(xLength, yLength);

        for (int x = 0; x < xLength; x++)
        {
            for (int y = 0; y < yLength; y++)
            {
                float value = noise[x, y];
                texture.SetPixel(x, y, GetNoiseColor(value));
            }
        }
        texture.filterMode = FilterMode.Point;
        texture.Apply();
        return texture;
    }
}
