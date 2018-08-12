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
    public Text highestCubeLevelLabel;
    public Text activityLabel;

    public static string FormatNumber(float number) {
        //TODO: add "pretty numbers"
        int numberOfZeros = (int)Mathf.Log10(number);
        if (numberOfZeros >= 12) {
            return (number / Mathf.Pow(10, 12)).ToString("f2") + "t";
        } else if ((numberOfZeros >= 9))
        {
            return (number / Mathf.Pow(10, 9)).ToString("f2") + "b";
        }
        else if ((numberOfZeros >= 6))
        {
            return (number / Mathf.Pow(10, 6)).ToString("f2") + "m";
        }
        else if ((numberOfZeros >= 3))
        {
            return (number / Mathf.Pow(10, 3)).ToString("f2") + "k";
        }
        else { return number.ToString("f0"); }
    } 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        coinzLabel.text = "Coinz: " + FormatNumber(Simulation.Instance.Gold);
        coinzIncreaseLabel.text = FormatNumber(Simulation.Instance.GoldPerSecond) + " / second";
        highestCubeLevelLabel.text = "Highest cube level: " + (Simulation.Instance.highestCubeLevel + 1);
        activityLabel.text = FormatNumber(Simulation.Instance.Clicks) + " Clicks / " + FormatNumber(Simulation.Instance.Swipes) + " Swipes";
	}
}
