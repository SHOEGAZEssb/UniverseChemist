using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Assets.Scripts.SaveLoad
{
  /// <summary>
  /// Save data of an active chemical display.
  /// </summary>
  [DataContract]
  class ChemicalBehaviourData
  {
    /// <summary>
    /// Name of the active chemical.
    /// </summary>
    [DataMember]
    public string ChemicalName { get; private set; }

    /// <summary>
    /// Position of the chemical.
    /// </summary>
    [DataMember]
    public Vector2 Position { get; private set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="chemicalName">Name of the active chemical.</param>
    /// <param name="position">Position of the chemical.</param>
    public ChemicalBehaviourData(string chemicalName, Vector2 position)
    {
      ChemicalName = chemicalName ?? throw new ArgumentNullException(nameof(chemicalName));
      Position = position;
    }
  }
}