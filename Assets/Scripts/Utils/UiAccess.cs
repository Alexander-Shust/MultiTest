using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiAccess : MonoBehaviour
{
    [SerializeField] private TMP_Text _coins;

    [SerializeField] private Image _healthBar;
    
    public static UiAccess Get;

    private void Awake()
    {
        Get = this;
    }

    public void SetCoins(int amount)
    {
        _coins.text = amount.ToString();
    }

    public void SetHealth(float amount)
    {
        _healthBar.fillAmount = amount / 100.0f;
    }
}