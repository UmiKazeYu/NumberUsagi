using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip selected;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetLevel1ButtonDown();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GetLevel2ButtonDown();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GetLevel3ButtonDown();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            GetHelpButtonDown();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            GetBackButtonDown();
        }
    }

    public void GetLevel1ButtonDown()
    {
        audioSource.PlayOneShot(selected, 0.5f);
        SceneManager.LoadScene("Level1Scene");
    }

    public void GetLevel2ButtonDown()
    {
        audioSource.PlayOneShot(selected, 0.5f);
        SceneManager.LoadScene("Level2Scene");
    }

    public void GetLevel3ButtonDown()
    {
        audioSource.PlayOneShot(selected, 0.5f);
        SceneManager.LoadScene("Level3Scene");
    }

    public void GetHelpButtonDown()
    {
        audioSource.PlayOneShot(selected, 0.5f);
        SceneManager.LoadScene("DescriptionScene");
    }

    public void GetBackButtonDown()
    {
        audioSource.PlayOneShot(selected, 0.5f);
        SceneManager.LoadScene("TitleScene");
    }
}
