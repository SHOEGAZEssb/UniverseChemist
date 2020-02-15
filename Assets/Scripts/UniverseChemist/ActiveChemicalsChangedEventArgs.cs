using System;
using System.Collections.Generic;
using System.Linq;

public class ActiveChemicalsChangedEventArgs : EventArgs
{
  public readonly IEnumerable<IChemical> NewChemicals;
  public readonly IEnumerable<IChemical> RemovedChemicals;

  public ActiveChemicalsChangedEventArgs(IEnumerable<IChemical> newChemicals, IEnumerable<IChemical> removedChemicals)
  {
    NewChemicals = newChemicals ?? Enumerable.Empty<IChemical>();
    RemovedChemicals = removedChemicals ?? Enumerable.Empty<IChemical>();
  }
}