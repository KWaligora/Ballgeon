using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HighscoreDisplay : MonoBehaviour
{
    public Text HighscoreText;
    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        HighscoreText.text = HighscoreManager.Instance.GetHighscoreString();
    }
}
