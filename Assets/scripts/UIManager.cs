using System.Collections;
using System.Collections.Generic;
//using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    //[SerializeField]
    //private TextMeshProUGUI textScore;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void UpdateTextScore(int totalScore)
    {
        //textScore.text = "Score " + totalScore;
    }

    // Start is called before the first frame update
    void Start()
    {
        //textScore.text = "Score: 0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
