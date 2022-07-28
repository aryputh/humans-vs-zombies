using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GoToScene : MonoBehaviour
{
    public int sceneToChangeTo;

    public void ChangeScene()
	{
		SceneManager.LoadScene(sceneToChangeTo);
	}
}
