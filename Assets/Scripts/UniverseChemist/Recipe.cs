using System;
using System.Collections.Generic;

/// <summary>
/// Defines a possible chemical combination.
/// </summary>
public class Recipe
{
  #region Properties

  /// <summary>
  /// The chemicals to combine.
  /// </summary>
  public List<string> InputChemicals { get; private set; }

  /// <summary>
  /// The output chemicals of the combination.
  /// </summary>
  public List<string> OutputChemicals { get; private set; }

  #endregion Properties

  #region Construction

  /// <summary>
  /// Construction.
  /// </summary>
  /// <param name="inputChemicals">The chemicals to combine.</param>
  /// <param name="outputChemicals">The output chemicals of the combination.</param>
  public Recipe(List<string> inputChemicals, List<string> outputChemicals)
  {
    InputChemicals = inputChemicals ?? throw new ArgumentNullException(nameof(inputChemicals));
    OutputChemicals = outputChemicals ?? throw new ArgumentNullException(nameof(outputChemicals));
  }

  #endregion Construction
}