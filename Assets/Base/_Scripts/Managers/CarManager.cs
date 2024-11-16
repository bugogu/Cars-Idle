using System.Collections.Generic;
using UnityEngine;

public class CarManager : Mono<CarManager>
{
    [SerializeField] private GameObject progressParent;
    [SerializeField] private Transform carSpawnPosition;
    [SerializeField] private ParticleSystem newSpawnFX;
    public GameObject[] carsPrefab;
    public List<GameObject> activeCars = new List<GameObject>();

    public static int BuyIndex
    {
        get => PlayerPrefs.GetInt("BuyIndex", 0);
        set => PlayerPrefs.SetInt("BuyIndex", value);
    }

    public void SpawnCar(int carIndex, GameObject car1 = null, GameObject car2 = null)
    {
        if (carIndex % 3 == 0)
            BuyIndex = carIndex;

        for (int i = 0; i < carIndex + 1; i++)
            progressParent.transform.GetChild(i).GetComponent<UnityEngine.UI.Image>().color = Color.green;

        GameObject spawnedCar = Instantiate(carsPrefab[carIndex], carSpawnPosition);

        spawnedCar.transform.localPosition = Vector3.zero;

        activeCars.Remove(car1); activeCars.Remove(car2);

        if (car1 != null && car2 != null)
        {
            Destroy(car1); Destroy(car2);
        }

        activeCars.Add(spawnedCar);

        newSpawnFX.Play();
    }

    public string saveKeyPrefix = "Number_"; // PlayerPrefs'te kullanılacak anahtar öneki

    void Start()
    {
        if (PlayerPrefs.HasKey(saveKeyPrefix + "0"))
            LoadNumbers();
    }

    // Sayıları PlayerPrefs'e kaydet
    void SaveNumbers()
    {
        for (int i = 0; i < activeCars.Count - 1; i++)
        {
            PlayerPrefs.SetInt(saveKeyPrefix + i, activeCars[i].GetComponent<Car>().carLevel);
        }
        PlayerPrefs.Save();
    }

    // Sayıları PlayerPrefs'ten yükle
    void LoadNumbers()
    {
        StartCoroutine(SpawnInitialCars());
    }

    private System.Collections.IEnumerator SpawnInitialCars()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(1f);
            if (PlayerPrefs.HasKey(saveKeyPrefix + i))
            {
                SpawnCar(PlayerPrefs.GetInt(saveKeyPrefix + i));
            }
            else
                break;
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        SaveNumbers();
    }
}
