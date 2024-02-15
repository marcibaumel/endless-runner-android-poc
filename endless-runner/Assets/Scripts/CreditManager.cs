using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditManager : MonoBehaviour
{
   [SerializeField] GameObject backButton;

    public void Back(){
        SceneManager.LoadScene(0);
    }
}
