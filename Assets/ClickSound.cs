using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSoundControl : MonoBehaviour
{
    public AudioSource audioData;
    

    public void startMusic(int size)
    {
        audioData.Play();
        StartCoroutine(clicksound(size));
    }
IEnumerator clicksound(int size){
    if (audioData!=null){
            while (audioData.isPlaying)yield return new WaitForSeconds(0.2f);
        DontDestroyOnLoad(gameObject);
        PlayerPrefs.SetInt("difficultyX", (int)size/10);
        PlayerPrefs.SetInt("difficultyY", size%10);
        //FindObjectOfType<LoadMenu>().LoadScene();
        //Destroy(this); 
    }
}
    // {
    //     DontDestroyOnLoad(this.gameObject);

    //     if (instance == null)
    //     {
    //             instance = this;
    //     }
        
    //}
}
