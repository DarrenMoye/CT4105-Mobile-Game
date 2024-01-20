using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManagerScript : MonoBehaviour
{

    public GameObject scoreText;

    // Start is called before the first frame update
    void Start()
    {  

        scoreText.GetComponent<Text>().text = "0";

    }

    // Update is called once per frame
    void Update()
    {


    }

}
