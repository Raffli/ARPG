using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest  {

	public string name { get; set; }
	public string task { get; set; }
	public bool active { get; set; }
	public bool done { get; set; }
	public int uiSlot { get; set; }
	public int xp { get; set; }

	public Quest (string name, string task, int xp) {
		this.name = name;
		this.task = task;
		this.xp = xp;
		done = false;
		active = false;
	}

}
