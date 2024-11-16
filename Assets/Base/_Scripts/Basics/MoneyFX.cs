using DG.Tweening;
using UnityEngine;

public class MoneyFX : MonoBehaviour
{
    private TMPro.TMP_Text _mText;

    private void Awake()
    {
        _mText = GetComponent<TMPro.TMP_Text>();

        transform.parent.DOMoveY(10, .5f).OnComplete(() => transform.parent.gameObject.SetActive(false));
    }

    public void SetText(int value)
    {
        _mText.text = "+" + value.ToString();
    }
}
