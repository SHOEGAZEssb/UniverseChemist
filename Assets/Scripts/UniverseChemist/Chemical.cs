using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// A chemical.
/// </summary>
public class Chemical : IChemical
{
  #region Properties

  /// <summary>
  /// Name of the chemical.
  /// </summary>
  public string Name { get; private set; }

  #endregion Properties

  #region Construction

  /// <summary>
  /// Construction.
  /// </summary>
  /// <param name="name">Name of the chemical.</param>
  public Chemical(string name)
  {
    Name = name ?? throw new ArgumentNullException(nameof(name));
  }

  #endregion Construction

  /// <summary>
  /// Combines this chemical with the given <paramref name="chemicals"/>.
  /// </summary>
  /// <param name="chemicals">Chemicals to combine with.</param>
  /// <returns>Resulting chemicals.</returns>
  public IEnumerable<IChemical> CombineWith(IEnumerable<IChemical> elements)
  {
    var matchingRecipe = GameData.Recipes.FirstOrDefault(r => r.InputChemicals.ContainsOnly(new[] { Name }.Concat(elements.Select(e => e.Name))));
    return matchingRecipe == null ? Enumerable.Empty<IChemical>()
                                  : matchingRecipe.OutputChemicals.Select(o => new Chemical(o));
  }

  /// <summary>
  /// Disassembles this chemical into its subparts.
  /// </summary>
  /// <returns>Subparts of this chemical.</returns>
  public IEnumerable<IChemical> Disassemble()
  {
    return GameData.DisassembleData[Name].Select(e => new Chemical(e));
  }

  /// <summary>
  /// Clones this chemical.
  /// </summary>
  /// <returns>Clone of this chemical.</returns>
  public IChemical Clone()
  {
    return new Chemical(Name);
  }

  /// <summary>
  /// Gets the name of the chemical.
  /// </summary>
  /// <returns>Name of the chemical.</returns>
  public override string ToString()
  {
    return Name;
  }
}