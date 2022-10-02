using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    public static float SoftCurrency;
    public static float HardCurrency;

    public int Energy;

    [SerializeField] private float _maxEnergy = 80;

    [SerializeField] private TextMeshProUGUI _softCurrency;
    [SerializeField] private TextMeshProUGUI _hardCurrency;
    [SerializeField] private TextMeshProUGUI _energyCurrency;

    public Currency Currency;

    private double _lastTime;
    private double _jsonTime;
    private Coroutine _timerEnergy;

    private void Awake() { }

    private void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            return;
        }

        SaveCurrency();
    }

    public void InitCurrency()
    {
        SoftCurrency = Currency.SoftCurrency;
        HardCurrency = Currency.HardCurrency;

        _lastTime = Currency.LastTime;

        if (Currency.Energy >= 0 && !Currency.ChekStatrOneGame)
        {
            Energy                    = 80;
            Currency.ChekStatrOneGame = true;
            SaveCurrency();
        }
        else
        {
            Energy = Currency.Energy;
        }

        CurrencyUpdateText();
        SetEnergy();
    }

    private void SaveCurrency()
    {
        Currency.SoftCurrency = SoftCurrency;
        Currency.HardCurrency = HardCurrency;

        Currency.LastTime = (int) GetCurrentTimeSec();
        Currency.Energy   = Energy;
    }

    public void CurrencyUpdateText()
    {
        _softCurrency.text = SoftCurrency.ToString();
        _hardCurrency.text = HardCurrency.ToString();
    }

    public double GetCurrentTimeSec()
    {
        var epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return (DateTime.UtcNow - epochStart).TotalSeconds;
    }

    public void EnergyUpdate()
    {
        _energyCurrency.text = Energy + "/" + _maxEnergy;
    }

    private void SetEnergy()
    {
        for (int i = 0; i < _maxEnergy; i++)
            if ((int) (GetCurrentTimeSec() - _lastTime) >= 60)
            {
                _lastTime += 60;
                if (Energy < _maxEnergy)
                {
                    Energy++;
                    EnergyUpdate();
                }
            }
            else
            {
                _timerEnergy = StartCoroutine(TimerEnergy());
                EnergyUpdate();
                return;
            }
    }

    private IEnumerator TimerEnergy()
    {
        yield return new WaitForSeconds(5);

        if ((int) (GetCurrentTimeSec() - _lastTime) >= 60)
        {
            _lastTime = GetCurrentTimeSec();
            if (Energy < _maxEnergy)
            {
                Energy++;
            }

            EnergyUpdate();
        }

        _timerEnergy = StartCoroutine(TimerEnergy());
    }
}

[Serializable]
public class Currency
{
    public float SoftCurrency;
    public float HardCurrency;
    public float TimerEnegry;
    public int LastTime;
    public int Energy;

    public bool ChekStatrOneGame;
}