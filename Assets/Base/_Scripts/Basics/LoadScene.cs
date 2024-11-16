using UnityEngine;

public class LoadScene : MonoBehaviour
{
    void Start() => LoadGame();

    public void LoadGame()
    {
        StartCoroutine(LoadingScene());
    }

    System.Collections.IEnumerator LoadingScene()
    {
        yield return new WaitForSeconds(.1f);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(PlayerPrefs.GetInt("Level", 1));
    }
}