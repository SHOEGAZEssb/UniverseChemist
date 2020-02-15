using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

public static class Data
{
    #region Constants

    private const string RECIPESJSONFILE = "recipes.json";
    private const string DISASSEMBLEDATAJSONFILE = "disassemble.json";

    #endregion Constants

    public static List<Recipe> Recipes { get; private set; }
    public static Dictionary<string, List<string>> DisassembleData { get; private set; }

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

    private static void WriteRecipesToJSON(string jsonFile)
    {
        string json = JsonConvert.SerializeObject(CreateRecipes(), Formatting.Indented);
        File.WriteAllText(jsonFile, json);
    }

    private static void CreateRecipesFromJSON(string jsonFile)
    {
        Recipes = JsonConvert.DeserializeObject<List<Recipe>>(File.ReadAllText(jsonFile));
    }

    private static Dictionary<string, List<string>> CreateDisassembleData()
    {
        var dic = new Dictionary<string, List<string>>();
        dic.Add("Water", new List<string>() { "Oxygen", "Hydrogen" });

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

    static Data()
    {
        if (!File.Exists(RECIPESJSONFILE))
            WriteRecipesToJSON(RECIPESJSONFILE);
        CreateRecipesFromJSON(RECIPESJSONFILE);

        if (!File.Exists(DISASSEMBLEDATAJSONFILE))
            WriteDisassembleDataToJSON(DISASSEMBLEDATAJSONFILE);
        CreateDisassembleDataFromJSON(DISASSEMBLEDATAJSONFILE);
    }
}