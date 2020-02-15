using System.Collections.Generic;
using System.Runtime.Serialization;

[DataContract]
public class GameData
{
    public static GameData FromGame(Game game)
    {
        var data = new GameData();
        foreach(var ae in game.ActiveElements)
            data.ActiveElements.Add(ae.Name);
        foreach(var ue in game.UnlockedElements)
            data.UnlockedElements.Add(ue);
        
        return data;
    }

    [DataMember]
    public List<string> ActiveElements = new List<string>();

    [DataMember]
    public List<string> UnlockedElements = new List<string>();
}