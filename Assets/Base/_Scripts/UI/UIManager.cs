using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIManager : Mono<UIManager>
{
    public int Money
    {
        get =>
            PlayerPrefs.GetInt("Money", 1);

        set =>
            PlayerPrefs.SetInt("Money", value);
    }

    public int Price
    {
        get => PlayerPrefs.GetInt("Price", 1);

        set
        {
            PlayerPrefs.SetInt("Price", value);
            priceText.text = value.ToString();
        }
    }

    public int Level
    {
        get => PlayerPrefs.GetInt("Level", 1);
        set
        {
            PlayerPrefs.SetInt("Level", value);
            levelText.text = "Level " + value.ToString();
        }
    }

    [SerializeField] private RectTransform nextLevelPanel;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private GameObject moneyFxGameobject;
    [SerializeField] private Transform moneyFxPosition;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private TMP_Text levelText;

    private void Awake()
    {
        UpdateMoney();

        priceText.text = Price.ToString();
        levelText.text = "Level " + Level.ToString();
    }

    private void UpdateMoney() => moneyText.text = Money.ToString();

    public void IncreaseMoney(int value)
    {
        Money += value;
        UpdateMoney();
        SpawnMoneyFX(value);
    }

    public void DecreaseMoney(int value)
    {
        Money -= value;
        UpdateMoney();
    }

    private void SpawnMoneyFX(int moneyValue)
    {
        GameObject spawnedFX = Instantiate(moneyFxGameobject, moneyFxPosition);

        spawnedFX.transform.localPosition = Vector3.zero;

        spawnedFX.transform.GetChild(0).GetComponent<MoneyFX>().SetText(moneyValue);
    }

    public void BuyCar()
    {
        if (Price > Money) return;

        CarManager.Instance.SpawnCar(CarManager.BuyIndex);
        DecreaseMoney(Price);

        Price++;
    }

    public void OpenLevelPanel()
    {
        nextLevelPanel.gameObject.SetActive(true);
        nextLevelPanel.DOScale(Vector3.zero, 0.2f).From().SetEase(Ease.InOutBack);
    }
    public void NextLevelButton()
    {
        Level++;
        if (Level <= 10)
            UnityEngine.SceneManagement.SceneManager.LoadScene(Level);

        else
        {
            PlayerPrefs.DeleteAll();
            UnityEngine.SceneManagement.SceneManager.LoadScene(Level);
        }
    }
}
