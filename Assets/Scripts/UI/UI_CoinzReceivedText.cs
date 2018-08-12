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

public class UI_CoinzReceivedText : MonoBehaviour {

    RectTransform rectTransform;
    CanvasGroup canvasGroup;

    public AudioClip[] coinSounds;
    AudioSource audioSource;

    public float disappearsAfter = 2f;

    float t = 0;

    private void Start()
    {
        audioSource.pitch = Random.Range(0.75f, 1.25f);
        audioSource.PlayOneShot(coinSounds[Random.Range(0, coinSounds.Length)]);
    }

    public void Setup(float coinz, Vector3 startPosition)
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        audioSource = GetComponent<AudioSource>();

        RectTransform canvasRect = GameObject.FindObjectOfType<Canvas>().GetComponent<RectTransform>();

        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(startPosition);
        Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));

        rectTransform.anchoredPosition = WorldObject_ScreenPosition;

        GetComponent<Text>().text = "+" + UI_DisplaySimulationStatistics.FormatNumber(coinz);
        Destroy(gameObject, disappearsAfter);
    }    

    // Update is called once per frame
    void Update () {
        t += Time.deltaTime;
        rectTransform.anchoredPosition += Vector2.up * 10 * Time.deltaTime;
        canvasGroup.alpha = Mathf.Lerp(1, 0, t / disappearsAfter);
	}
}
