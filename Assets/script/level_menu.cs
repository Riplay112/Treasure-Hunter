using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class level_menu : MonoBehaviour
{
    public void level_1()
    {
        SceneManager.LoadScene("level_1");
    }   

    public void level_2()
        {
            SceneManager.LoadScene("level_2");
        }   
    
        public void level_3()
        {
            SceneManager.LoadScene("level_3");
    }
    public void exit()
    {
        SceneManager.LoadScene("Main_menu");
    }
}
