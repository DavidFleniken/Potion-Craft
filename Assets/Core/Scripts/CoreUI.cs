using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoreUI : MonoBehaviour
{
    static CoreUI singleton; void Awake() { singleton = this; }

    // Core UI Elements
    [SerializeField] private TextMeshProUGUI _countdownTxt;
    public static TextMeshProUGUI CountdownText => singleton._countdownTxt;
    [SerializeField] private Slider _timer;
    public static Slider Timer => singleton._timer;

}
