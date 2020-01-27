//タイトル画面に遷移するクラス

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleLoad : MonoBehaviour {
	
	// Update is called once per frame
	public void goTitle () {
        SceneManager.LoadScene("title");
	}
}
