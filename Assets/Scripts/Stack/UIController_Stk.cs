using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController_Stk : MonoBehaviour
{
    [Header("Main")]
    [SerializeField]
    private GameObject      mainPannel;

    [Header("InGame")]
    [SerializeField]
    private TextMeshProUGUI textCurrentScore;
    [SerializeField]
    private TextMeshProUGUI currentComboCount;

    //[SerializeField]
    //private float duration = 0.8f;
    
    [Header("CombotextEffect")]
    [SerializeField]
    private float startSize;
    [SerializeField]
    private float endSize;
    [SerializeField]
    private float resizeTime;
    
    [Header("GameOver")]
    [SerializeField]
    private GameObject      textNewRecord;
    [SerializeField]
    private GameObject      imageCrown;
    [SerializeField]
    private TextMeshProUGUI textHighScore;
    [SerializeField]
    private TextMeshProUGUI textHighScoreText;
    [SerializeField]
    private GameObject      textTouchToRestart;

    private AudioSource     _audioSource;
    
   //private float fadeTime = 3.5f;

    [SerializeField]
    private AudioClip _gameOverSound;
    [SerializeField]
    private AudioClip _bestRecordSound;
    

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void GameStart()
    {
        mainPannel.SetActive(false);
        
        textCurrentScore.gameObject.SetActive(true);
    }

    public void UpdateCombo(int combo)
    {
        currentComboCount.text = $"{combo.ToString()} Combo!";
        currentComboCount.gameObject.SetActive(true);
        StartCoroutine("ComboTextEffect");
    }

    public void CombeFail()
    {
        currentComboCount.gameObject.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        textCurrentScore.text = score.ToString();
    }

    public void GameOver(bool isNewRecord)
    {
        if (isNewRecord == true)
        {
            textNewRecord.SetActive(true);
            _audioSource.clip = _bestRecordSound;
            _audioSource.Play();
        }
        else
        {
            _audioSource.clip = _gameOverSound;
            _audioSource.Play();
            imageCrown.SetActive(true);

            textHighScore.text = PlayerPrefs.GetInt("HighScore").ToString();
            textHighScore.gameObject.SetActive(true);
            textHighScoreText.gameObject.SetActive(true);
        }
        currentComboCount.gameObject.SetActive(false);
        textTouchToRestart.SetActive(true);
    }
    
    private IEnumerator ComboTextEffect()
    {
        yield return StartCoroutine(Resize(startSize, endSize, resizeTime));
            
        yield return StartCoroutine(Resize(endSize, startSize, resizeTime));
    }
    
    private IEnumerator Resize(float start, float end, float time)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            currentComboCount.fontSize = Mathf.Lerp(start, end, percent);

            yield return null;
        }
    }

}
