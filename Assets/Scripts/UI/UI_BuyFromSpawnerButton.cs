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

public class UI_BuyFromSpawnerButton : MonoBehaviour {
    const float BASE_SPAWN_PRICE = 10;
    const float INCREASE_PER_BUY_FACTOR = 1.53345f;
    const float UNDER_SPAWNER_UNITS = 16f;

    public CubeSpawner spawner;
    Button button;
    Text text;

    RectTransform canvasRect;
    Camera mainCamera;

    int buyCount = 0;

    float SpawnPrice { get {
            return BASE_SPAWN_PRICE * Mathf.Pow(3, spawner.baseSpawnLevel) * Mathf.Pow(INCREASE_PER_BUY_FACTOR, buyCount);       
        }
    }

    private void Start()
    {
        canvasRect = GameObject.FindObjectOfType<Canvas>().GetComponent<RectTransform>();
        mainCamera = Camera.main;

        // move to correct position
        RectTransform rt = GetComponent<RectTransform>();
        rt.anchoredPosition = uiAnchoredPosition();

        button = GetComponent<Button>();
        text = GetComponentInChildren<Text>();
    }

    public Vector2 uiAnchoredPosition()
    {
        
        Vector2 ViewportPosition = mainCamera.WorldToViewportPoint(spawner.transform.position + Vector3.down * UNDER_SPAWNER_UNITS);
        Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));


        return WorldObject_ScreenPosition;
    }

    // Update is called once per frame
    void Update () {
        text.text = "Buy \n(" + UI_DisplaySimulationStatistics.FormatNumber(SpawnPrice) + ")";
        if (spawner.SpawnCount > 0 && SpawnPrice <= Simulation.Instance.Gold && spawner.SpawnCount <= spawner.MAX_CUBES) {
            button.interactable = true;
        } else {
            button.interactable = false;
        }
	}

    public void BuyButtonClicked() {
        if (SpawnPrice <= Simulation.Instance.Gold) {
            Simulation.Instance.Gold -= SpawnPrice;
            spawner.SpawnCube(spawner.baseSpawnLevel);
            buyCount += 1;
        }
    }
}
