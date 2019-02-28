using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public float Volume = 1f;
    public int levelReached = 1;
    public List<StageData> Stage = new List<StageData>();
}

[Serializable]
public class StageData
{
    public int monstersKilled;
    public int Attempts;
}

