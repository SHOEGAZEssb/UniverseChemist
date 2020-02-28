using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Assets.Scripts.SaveLoad
{
  /// <summary>
  /// Full save data for the game and it's display.
  /// </summary>
  [DataContract]
  class FullSaveData
  {
    /// <summary>
    /// The data of the active chemical behaviours.
    /// </summary>
    [DataMember]
    public List<ChemicalBehaviourData> ActiveChemicalBehaviours { get; private set; }

    /// <summary>
    /// The save data of the <see cref="Game"/>.
    /// </summary>
    [DataMember]
    public GameSaveData GameSaveData { get; private set; }

    #region Construction

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="activeChemicalBehaviours">The data of the active chemical behaviours.</param>
    /// <param name="gameSaveData">The save data of the <see cref="Game"/>.</param>
    public FullSaveData(List<ChemicalBehaviourData> activeChemicalBehaviours, GameSaveData gameSaveData)
    {
      ActiveChemicalBehaviours = activeChemicalBehaviours ?? throw new ArgumentNullException(nameof(activeChemicalBehaviours));
      GameSaveData = gameSaveData ?? throw new ArgumentNullException(nameof(gameSaveData));
    }

    #endregion Construction
  }
}