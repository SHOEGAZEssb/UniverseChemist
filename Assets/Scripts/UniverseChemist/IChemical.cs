using System.Collections.Generic;

/// <summary>
/// Interface for a chemical.
/// </summary>
public interface IChemical
{
  /// <summary>
  /// Name of the chemical.
  /// </summary>
  string Name { get; }

  /// <summary>
  /// Combines this chemical with the given <paramref name="chemicals"/>.
  /// </summary>
  /// <param name="chemicals">Chemicals to combine with.</param>
  /// <returns>Resulting chemicals.</returns>
  IEnumerable<IChemical> CombineWith(IEnumerable<IChemical> chemicals);

  /// <summary>
  /// Disassembles this chemical into its subparts.
  /// </summary>
  /// <returns>Subparts of this chemical.</returns>
  IEnumerable<IChemical> Disassemble();

  /// <summary>
  /// Clones this chemical.
  /// </summary>
  /// <returns>Clone of this chemical.</returns>
  IChemical Clone();
}