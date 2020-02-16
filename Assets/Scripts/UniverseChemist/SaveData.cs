using System.Collections.Generic;
using System.Runtime.Serialization;

/// <summary>
/// Save data to save / load a <see cref="Game"/>.
/// </summary>
[DataContract]
public class SaveData
{
  /// <summary>
  /// Creates the save data for the given <paramref name="game"/>.
  /// </summary>
  /// <param name="game">Game to create save data from.</param>
  /// <returns>Created save data.</returns>
  public static SaveData FromGame(Game game)
  {
    var data = new SaveData();
    foreach (var ae in game.ActiveChemicals)
      data.ActiveElements.Add(ae.Name);
    foreach (var ue in game.UnlockedChemicals)
      data.UnlockedElements.Add(ue);

    return data;
  }

  /// <summary>
  /// Currently active chemicals.
  /// </summary>
  [DataMember]
  public List<string> ActiveElements = new List<string>();

  /// <summary>
  /// Currently unlocked chemicals.
  /// </summary>
  [DataMember]
  public List<string> UnlockedElements = new List<string>();
}