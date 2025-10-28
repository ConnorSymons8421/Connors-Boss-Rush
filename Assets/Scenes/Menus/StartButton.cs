using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    public void OnButtonClick()
    {
        audio.Play();
        Invoke("SwitchScene", 0.5f);
    }

    void SwitchScene()
    {
        SceneManager.LoadScene("Boss_Select");
    }
}
