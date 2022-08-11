using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelGenerateStrategy
{
    public abstract Vector3 Scale { get; set; }
    public abstract List<GenerateInfo> GetSpawnPositions();
}
