using System.Collections.Generic;

public class Recipe
{
    public List<string> InputElements { get; private set; }
    public List<string> OutputElements {get; private set; }

    public Recipe(List<string> inputElements, List<string> outputElements)
    {
        InputElements = inputElements;
        OutputElements = outputElements;
    }
}