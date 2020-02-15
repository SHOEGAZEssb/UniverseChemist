using System;
using System.Collections.Generic;
using System.Linq;

public class Chemical : IChemical
{
    public string Name { get; private set; }

    public Chemical(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public IEnumerable<IChemical> CombineWith(IEnumerable<IChemical> elements)
    {
        var matchingRecipe = Data.Recipes.FirstOrDefault(r => r.InputElements.ContainsOnly(new[] { Name }.Concat(elements.Select(e => e.Name))));
        if(matchingRecipe == null)
            return Enumerable.Empty<IChemical>();
        else
            return matchingRecipe.OutputElements.Select(o => new Chemical(o));
    }

    public IEnumerable<IChemical> Disassemble()
    {
        return Data.DisassembleData[Name].Select(e => new Chemical(e));
    }

    public IChemical Clone()
    {
        return new Chemical(Name);
    }
}