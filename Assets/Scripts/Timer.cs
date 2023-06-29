using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("Timer UI references :")] [SerializeField]
    private Image uiFillImage;

    [SerializeField] private Text uiText;

    public DeathManager deathManager;

    // Events --
    private UnityAction _onTimerBeginAction;
    private UnityAction<int> _onTimerChangeAction;
    private UnityAction _onTimerEndAction;
    private UnityAction<bool> _onTimerPauseAction;
    private int _remainingDuration;
    private ScoreManager scoreManager;
    private int Duration { get; set; }
    private bool IsPaused { get; set; }

    private void Awake()
    {
        ResetTimer();
    }

    private void Start()
    {
        scoreManager = GameObject.Find("ScoreText").GetComponent<ScoreManager>();
        UpdateUI(_remainingDuration);
        ResetTimer();
        //Se movio el timer para hacer pruebas de gameover
        SetDuration(180).Begin();
    }

    private void Update()
    {
        UpdateUI(_remainingDuration);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void ResetTimer()
    {
        uiText.text = "00:00";
        uiFillImage.fillAmount = 0f;

        Duration = _remainingDuration = 0;

        _onTimerBeginAction = null;
        _onTimerChangeAction = null;
        _onTimerEndAction = null;
        _onTimerPauseAction = null;

        IsPaused = false;
    }

    public void SetPaused(bool paused)
    {
        IsPaused = paused;

        if (_onTimerPauseAction != null)
            _onTimerPauseAction.Invoke(IsPaused);
    }


    public Timer SetDuration(int seconds)
    {
        Duration = _remainingDuration = seconds;
        return this;
    }

    //-- Events ----------------------------------
    public Timer OnBegin(UnityAction action)
    {
        _onTimerBeginAction = action;
        return this;
    }

    public Timer OnChange(UnityAction<int> action)
    {
        _onTimerChangeAction = action;
        return this;
    }

    public Timer OnEnd(UnityAction action)
    {
        _onTimerEndAction = action;
        return this;
    }

    public Timer OnPause(UnityAction<bool> action)
    {
        _onTimerPauseAction = action;
        return this;
    }

    public void AddTime(int seconds)
    {
        _remainingDuration += seconds;
        UpdateUI(_remainingDuration);
    }

    public void SubstractTime(int seconds)
    {
        _remainingDuration -= seconds;
        UpdateUI(_remainingDuration);
    }

    public void Begin()
    {
        _onTimerBeginAction?.Invoke();

        StopAllCoroutines();
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (_remainingDuration > 0)
        {
            if (!IsPaused)
            {
                _onTimerChangeAction?.Invoke(_remainingDuration);
                _remainingDuration--;
            }

            yield return new WaitForSeconds(1f);
        }

        End();
    }

    private void UpdateUI(int seconds)
    {
        uiText.text = $"{seconds / 60:D2}:{seconds % 60:D2}";
        uiFillImage.fillAmount = Mathf.InverseLerp(0, Duration, seconds);
    }

    private void End()
    {
        deathManager.Setup(scoreManager.getScore());
        _onTimerEndAction?.Invoke();
    }
}