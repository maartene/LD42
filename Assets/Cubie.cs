// Ludum Dare 42 entry
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
using UnityEngine.EventSystems;

public class Cubie : MonoBehaviour
{
    public static readonly float[] PITCHES = { 264, 277.2f, 297, 311.1f, 330, 352, 370.0f, 396, 415.3f, 440f, 466.2f, 495, 528 };
    public static readonly float[] CHORDS = { 0, 4, 7, 10 };

    public int cubeLevel = 0;

    Rigidbody rigidBody;
    Collider collider;
    Renderer renderer;
    AudioSource audioSource;

    public Material normalMaterial;
    public Material highLightMaterial;

    Material m_normalMaterial;
    Material m_highLightMaterial;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        renderer = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();

        m_normalMaterial = new Material(normalMaterial);
        m_highLightMaterial = new Material(highLightMaterial);

        SetColor();
        SetPitch();

    }

    public void PlayNote(float length = 1f) 
    {
        StartCoroutine(PlayNoteCR(length));
    }

    IEnumerator PlayNoteCR(float length)
    {
        audioSource.Play();
        HighLight(true);
        yield return new WaitForSeconds(length);
        HighLight(false);
        audioSource.Stop();
    }

    void HighLight(bool state)
    {
        if (state)
        {
            renderer.material = m_highLightMaterial;
        }
        else
        {
            renderer.material = m_normalMaterial;
        }
    }

    public void IncreaseCubeLevel()
    {
        cubeLevel += 1;
        SetColor();
        SetPitch();
    }

    void SetColor()
    {    
        Color color = new HSBColor((float)((cubeLevel * 3) % 16) / 16.0f, 1, 1).ToColor();

        m_normalMaterial.color = color;
        m_highLightMaterial.color = color;
        m_highLightMaterial.SetColor("_EmissionColor", color);
        
        renderer.material = m_normalMaterial;        
    }

    void SetPitch()
    {
        float octave = 1 + (cubeLevel / 4) ;
        

        float pitch = (octave * PITCHES[cubeLevel % 4]) / PITCHES[0];
        audioSource.pitch = pitch;
    }

	// Update is called once per frame
	void Update () {
		
	}

    public void MakeRigidBodyDraggable(bool state)
    {
        rigidBody.isKinematic = state;
        collider.isTrigger = state;
    }

    public void OnDragStart(Vector3 mousePosition)
    {
        Debug.Log("Begin drag");
        MakeRigidBodyDraggable(true);
    }

    public void OnDrag(Vector3 mousePosition)
    {

        Plane plane = new Plane(Vector3.back, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        float distance;
        plane.Raycast(ray, out distance);

        Vector3 targetPosition = Camera.main.transform.position + ray.direction * distance;

        targetPosition = new Vector3(0, targetPosition.y, 0);

        transform.position = targetPosition;
    }

    public void OnDragEnd(Vector3 mousePosition)
    {
        // first we need to determine whether we are above another cube
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        
        // because we are always also hitting ourselves, we need to find all colliding cubes
        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in hits)
        {
            Cubie c = hit.collider.GetComponent<Cubie>();
            if (c == this)
            {
                // we hit ourselves, don't do anything.
            } else if (c != null)
            {
                // we hit another cubie
                // is this other cubie of the same "level"?
                if (c.cubeLevel == this.cubeLevel)
                {
                    // we are on the level
                    // we will update the other cubie
                    c.IncreaseCubeLevel();
                    c.MakeRigidBodyDraggable(false);
                    // and we will destroy ourselves
                    Destroy(gameObject);
                } else
                {
                    // go back to the top
                    transform.position = transform.parent.position;
                    MakeRigidBodyDraggable(false);
                }
            } else
            {
                // we hit nothing
                MakeRigidBodyDraggable(false);
            }

        }

        
    }
}
