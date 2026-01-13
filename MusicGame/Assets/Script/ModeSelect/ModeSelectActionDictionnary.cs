using System;
using System.Collections.Generic;
public class ModeSelectActionDictionnary : ModeSelect.IActionDictionary
{
    public Dictionary<int, Action> ActionDic { get; private set; }
    public ModeSelectActionDictionnary()
    {
        ActionDic = new();
    }
    public void AddDic(int id, Action action)
    {
        ActionDic.Add(id, action);
    }
}

