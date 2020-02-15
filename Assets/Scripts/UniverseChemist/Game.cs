using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;

public class Game
{
    #region Properties

    public IEnumerable<IChemical> ActiveElements => _activeElements;
    private List<IChemical> _activeElements;

    public IEnumerable<string> UnlockedElements => _unlockedElements;
    private List<string> _unlockedElements;

    #endregion Properties

    #region Construction

    public static Game FromSaveFile(string file)
    {
        var serializer = new DataContractSerializer(typeof(GameData));
        using (var fs = new FileStream(file, FileMode.Open))
        {
            var gd = (GameData)serializer.ReadObject(fs);
            return Game.FromGameData(gd);
        }
    }

    public static Game FromGameData(GameData data)
    {
        var game = new Game();
        game._activeElements = data.ActiveElements.Select(e => (IChemical)new Chemical(e)).ToList();
        game._unlockedElements = data.UnlockedElements;
        return game;
    }

    public static Game New()
    {
        var game = new Game();
        game.AddElement(new Chemical("Fire"));
        game.AddElement(new Chemical("Water"));
        return game;
    }

    private Game()
    {
        _activeElements = new List<IChemical>();
        _unlockedElements = new List<string>();
    }

    #endregion Construction

    public void Combine(IEnumerable<IChemical> elements)
    {
        if (elements == null || elements.Any(e => e == null))
            throw new ArgumentNullException(nameof(elements));
        else
        {
            var newElements = elements.First().CombineWith(elements.Skip(elements.Count() - 1));
            if (newElements.Count() != 0)
            {
                // remove old elements
                foreach (var e in elements)
                    _activeElements.Remove(e);

                // add new elements
                foreach (var element in newElements)
                    AddElement(element);
            }
        }
    }

    public void Combine(IChemical element1, IChemical element2)
    {
        Combine(new[] { element1, element2 });
    }

    public void Disassemble(IChemical element)
    {
        if (element == null)
            throw new ArgumentNullException(nameof(element));
        else if (!ActiveElements.Contains(element))
            throw new ArgumentException(nameof(element));
        else
        {
            var newElements = element.Disassemble();
            if (newElements.Count() > 0)
            {
                // remove old element
                _activeElements.Remove(element);

                // add new elements
                foreach (var e in newElements)
                    AddElement(e);
            }
        }
    }

    public void AddElementToWorkspace(string name)
    {
        if (!UnlockedElements.Contains(name))
            throw new ArgumentException($"{name} is not unlocked", nameof(name));

        AddElement(new Chemical(name));
    }

    public void CleanUp()
    {
        _activeElements.RemoveAll(e => true);
    }

    public void Save(string file)
    {
        var serializer = new DataContractSerializer(typeof(GameData));
        using (var w = XmlWriter.Create(file, new XmlWriterSettings { Indent = true }))
            serializer.WriteObject(w, GameData.FromGame(this));
    }

    private void AddElement(IChemical element)
    {
        if (element == null)
            throw new ArgumentNullException(nameof(element));

        _activeElements.Add(element);
        if (!UnlockedElements.Contains(element.Name))
            _unlockedElements.Add(element.Name);
    }
}