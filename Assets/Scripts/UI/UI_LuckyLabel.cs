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

public class UI_LuckyLabel : MonoBehaviour {

    CanvasGroup canvasGroup;
    public Text explanationText;
    Text myText;

	// Use this for initialization
	void OnEnable () {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        myText = GetComponent<Text>();
        
        StartCoroutine(FadeInOut());
	}
	
    public void SetLevel(int level)
    {
        explanationText.text = "You got a free level " + (level + 1) + " block";
    }

	// Update is called once per frame
	void Update () {
        
    }

    IEnumerator FadeInOut()
    {
        canvasGroup.alpha = 0;
        float t = 0;
        while (t < 1.0f)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, t);
            myText.color = new HSBColor(t, 1, 1).ToColor();
            yield return null;
            t += Time.deltaTime;
        }

        canvasGroup.alpha = 1;

        while (t > 0.0f)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, t);
            myText.color = new HSBColor(t, 1, 1).ToColor();
            yield return null;
            t -= Time.deltaTime;
        }

        canvasGroup.alpha = 0;

        gameObject.SetActive(false);
    }
}
