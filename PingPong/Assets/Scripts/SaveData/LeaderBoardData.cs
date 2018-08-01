using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int id;
    public string name;
    public int score;
}

[Serializable]
public class LeaderBoardData
{
    public List<PlayerData> dataList;
}
