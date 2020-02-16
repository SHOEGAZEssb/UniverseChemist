using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

/// <summary>
/// Holds game data.
/// </summary>
public static class GameData
{
  #region Constants

  private const string RECIPESJSONFILE = "recipes.json";
  private const string DISASSEMBLEDATAJSONFILE = "disassemble.json";

  #endregion Constants

  #region Properties

  /// <summary>
  /// List of available chemical recipes (combinations).
  /// </summary>
  public static List<Recipe> Recipes { get; private set; }

  /// <summary>
  /// Data which elements disassembles into what subparts.
  /// </summary>
  public static Dictionary<string, List<string>> DisassembleData { get; private set; }

  #endregion Properties

  /// <summary>
  /// Creates the available chemical combinations.
  /// </summary>
  /// <returns>Chemical combinations.</returns>
  private static List<Recipe> CreateRecipes()
  {
    var list = new List<Recipe>();

    var il = new List<string>() { "Water", "Fire" };
    var ol = new List<string>() { "Ash" };
    list.Add(new Recipe(il, ol));

    il = new List<string>() { "Fire", "Fire" };
    ol = new List<string>() { "Oxygen" };
    list.Add(new Recipe(il, ol));

    return list;
  }

  /// <summary>
  /// Serializes the <see cref="Recipes"/> to the given <paramref name="jsonFile"/>.
  /// </summary>
  /// <param name="jsonFile">File to serialize to.</param>
  private static void WriteRecipesToJSON(string jsonFile)
  {
    string json = JsonConvert.SerializeObject(CreateRecipes(), Formatting.Indented);
    File.WriteAllText(jsonFile, json);
  }

  /// <summary>
  /// Loads the chemical combinations from the given <paramref name="jsonFile"/>.
  /// </summary>
  /// <param name="jsonFile">File to load combinations from.</param>
  private static void CreateRecipesFromJSON(string jsonFile)
  {
    Recipes = JsonConvert.DeserializeObject<List<Recipe>>(File.ReadAllText(jsonFile));
  }

  private static Dictionary<string, List<string>> CreateDisassembleData()
  {
    var dic = new Dictionary<string, List<string>>
    {
      { "Water", new List<string>() { "Oxygen", "Hydrogen" } }
    };

    return dic;
  }

  private static void WriteDisassembleDataToJSON(string jsonFile)
  {
    string json = JsonConvert.SerializeObject(CreateDisassembleData(), Formatting.Indented);
    File.WriteAllText(jsonFile, json);
  }

  private static void CreateDisassembleDataFromJSON(string jsonFile)
  {
    DisassembleData = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(File.ReadAllText(jsonFile));
  }

  /// <summary>
  /// Constructor.
  /// </summary>
  static GameData()
  {
    if (!File.Exists(RECIPESJSONFILE))
      WriteRecipesToJSON(RECIPESJSONFILE);
    CreateRecipesFromJSON(RECIPESJSONFILE);

    if (!File.Exists(DISASSEMBLEDATAJSONFILE))
      WriteDisassembleDataToJSON(DISASSEMBLEDATAJSONFILE);
    CreateDisassembleDataFromJSON(DISASSEMBLEDATAJSONFILE);
  }
}