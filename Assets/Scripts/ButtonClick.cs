using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    void OnStart () {
        SceneManager.LoadScene(1);
    }
}
