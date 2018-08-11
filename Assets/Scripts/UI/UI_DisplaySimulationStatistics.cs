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


public class UI_DisplaySimulationStatistics : MonoBehaviour {

    public Text coinzLabel;
    public Text coinzIncreaseLabel;
    public Text activityLabel;

    public static string FormatNumber(float number) {
        //TODO: add "pretty numbers"
        return number.ToString();
    } 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        coinzLabel.text = "Coinz: " + FormatNumber(Simulation.Instance.Gold);
        coinzIncreaseLabel.text = FormatNumber(Simulation.Instance.GoldPerSecond) + " / second";
        activityLabel.text = FormatNumber(Simulation.Instance.Clicks) + " Clicks / " + FormatNumber(Simulation.Instance.Swipes) + " Swipes";
	}
}
