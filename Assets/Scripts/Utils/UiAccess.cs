using TMPro;
using UnityEngine;

public class UiAccess : MonoBehaviour
{
    [SerializeField] private TMP_Text _coins;
    
    public static UiAccess Get;

    private void Awake()
    {
        Get = this;
    }

    public void SetCoins(int amount)
    {
        _coins.text = amount.ToString();
    }
}