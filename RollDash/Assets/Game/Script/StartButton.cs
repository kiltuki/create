using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartButton : MonoBehaviour {

	// Update is called once per frame
	public void LoadScene () {
        SceneManager.LoadScene("Game");
	}
}
