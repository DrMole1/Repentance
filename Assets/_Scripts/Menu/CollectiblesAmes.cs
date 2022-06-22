using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectiblesAmes : MonoBehaviour
{

    public TextMeshProUGUI jaunestxt;
    public TextMeshProUGUI bleuestxt;

    private int _amesJaunes;
    private int _amesBleues;

    // Start is called before the first frame update
    void Start()
    {
        _amesJaunes = PlayerPrefs.GetInt("AmesJaunes");
        _amesBleues = PlayerPrefs.GetInt("AmesBleues");
    }

    // Update is called once per frame
    void Update()
    {

        _amesJaunes = PlayerPrefs.GetInt("AmesJaunes");
        jaunestxt.text = _amesJaunes.ToString();

        _amesBleues = PlayerPrefs.GetInt("AmesBleues");
        bleuestxt.text = _amesBleues.ToString();



    }
}
