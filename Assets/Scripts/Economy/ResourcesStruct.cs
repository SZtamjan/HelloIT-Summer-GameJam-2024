using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[System.Serializable]
public class ResourcesStruct
{
    [HideInInspector] public UnityEvent updatedResources;
    
    [SerializeField] private int cash;

    public int Cash
    {
        get
        {
            return cash;
        }
        set
        {
            cash = value;
            PropertySetter();
        }
    }

    private void PropertySetter()
    {
        UpdateUI();
        InvokeUpdatedResources();
    }
    
    private void UpdateUI()
    {
        UIController.Instance.EconomyUpdateResources(this);
    }

    private void InvokeUpdatedResources()
    {
        if(updatedResources != null) updatedResources.Invoke();
    }
    
    
    //Constructors
    public ResourcesStruct(int cash)
    {
        Cash = cash;
    }

    public ResourcesStruct()
    {
        
    }
}
