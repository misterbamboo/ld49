using Assets.Chemicals;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChemicalItem
{
    public ChemicalElements ChemicalElement { get; }

    void FlagAsAlreadyReact();

    bool HasAlreadyReact();
}

public class ChemicalItem : MonoBehaviour, IChemicalItem
{
    [SerializeField] private GameObject _chemicalMixturePrefab;

    [SerializeField] private ChemicalElements _chemicalElement;

    private ChemicalElements _selectedElement;

    public ChemicalElements ChemicalElement => _selectedElement;

    private bool _alreadyReact;
    public void FlagAsAlreadyReact()
    {
        _alreadyReact = true;
    }
    public bool HasAlreadyReact()
    {
        return _alreadyReact;
    }

    private void Start()
    {
        DefineSelectedElement();
    }

    private void DefineSelectedElement()
    {
        if (_chemicalElement == ChemicalElements.Random)
        {
            _selectedElement = GetRandomElement();
        }
        else
        {
            _selectedElement = _chemicalElement;
        }
    }

    private static ChemicalElements GetRandomElement()
    {
        var count = Enum.GetNames(typeof(ChemicalElements)).Length;
        var colorIndex = UnityEngine.Random.Range(1, count - 1);
        var element = (ChemicalElements)colorIndex;
        return element;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IChemicalItem otherChemical))
        {
            var currentChemical = GetComponent<IChemicalItem>();
            if (currentChemical.HasAlreadyReact() || otherChemical.HasAlreadyReact())
            {
                return;
            }

            if (React(currentChemical, otherChemical))
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }

    private bool React(IChemicalItem firstChemical, IChemicalItem secondChemical)
    {
        var reaction = ChemicalMixes.Mix(firstChemical.ChemicalElement, secondChemical.ChemicalElement);
        if (reaction == null)
        {
            Debug.LogWarning($"No reaction defined for chemical : {firstChemical} & {secondChemical}");
            return false;
        }

        firstChemical.FlagAsAlreadyReact();
        secondChemical.FlagAsAlreadyReact();

        if (reaction.HasInstantEffect())
        {

        }
        else
        {
            SpawChemicalMixture(reaction);
        }
        return true;
    }

    private void SpawChemicalMixture(IChemicalMixReaction reaction)
    {
        var mixtureItem = Instantiate(_chemicalMixturePrefab);
        IChemicalMixture chemicalMixture;
        if (!mixtureItem.TryGetComponent(out chemicalMixture))
        {
            Debug.LogError($"_chemicalMixturePrefab should have {nameof(IChemicalMixture)}");
            return;
        }
        chemicalMixture.InitReaction(reaction);
        mixtureItem.gameObject.transform.position = transform.position;
    }
}
