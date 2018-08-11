// Copyright (C) 2018 Maarten Engels, thedreamweb.eu

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Hurry : MonoBehaviour {

    public CubeSpawner spawner;
    public Text text;
    public RectTransform progressBarRectTransform;

    Vector2 originalSize;

	// Use this for initialization
	void Start () {
        originalSize = progressBarRectTransform.sizeDelta;
	}
	
	// Update is called once per frame
	void Update () {
        progressBarRectTransform.sizeDelta = new Vector2(Mathf.Lerp(originalSize.x, 0, spawner.cubeSpawnTimeRemaining / spawner.cubeSpawnDelay), originalSize.y);
        text.text = Mathf.FloorToInt(spawner.cubeSpawnTimeRemaining) + "s";
	}

    public void HurryButtonClicked() {
        spawner.cubeSpawnTimeRemaining -= 1.0f;
        Simulation.Instance.Clicks += 1;
    }

}
