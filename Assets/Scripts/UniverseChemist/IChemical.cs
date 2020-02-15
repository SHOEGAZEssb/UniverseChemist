using System;
using System.Collections.Generic;

public interface IChemical
{
    string Name { get; }
    IEnumerable<IChemical> CombineWith(IEnumerable<IChemical> elements);
    IEnumerable<IChemical> Disassemble();
    IChemical Clone();
}