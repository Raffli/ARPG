using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

	public GameObject hudPanel;
	private Slider xpBar;
	private Player player;

	// Use this for initialization
	void Start () {
		xpBar = hudPanel.transform.Find ("XpBar").GetComponent<Slider> ();
		player = GetComponent<Player> ();
		Debug.Log (((float) player.xp) / ((float) player.xpToLevel));
	}
	
	// Update is called once per frame
	void Update () {
		xpBar.value = ((float) player.xp) / ((float) player.xpToLevel);
	}
}
