using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// EventArgs for when the <see cref="Game.ActiveChemicals"/> change.
/// </summary>
public class ActiveChemicalsChangedEventArgs : EventArgs
{
  /// <summary>
  /// The newly added chemicals.
  /// </summary>
  public readonly IEnumerable<IChemical> NewChemicals;

  /// <summary>
  /// The removed chemicals.
  /// </summary>
  public readonly IEnumerable<IChemical> RemovedChemicals;

  /// <summary>
  /// Constructor.
  /// </summary>
  /// <param name="newChemicals">The newly added chemicals.</param>
  /// <param name="removedChemicals">The removed chemicals.</param>
  public ActiveChemicalsChangedEventArgs(IEnumerable<IChemical> newChemicals, IEnumerable<IChemical> removedChemicals)
  {
    NewChemicals = newChemicals ?? Enumerable.Empty<IChemical>();
    RemovedChemicals = removedChemicals ?? Enumerable.Empty<IChemical>();
  }
}