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
    public int cubeLevel = 0;

    Rigidbody rigidBody;
    Collider collider;
    Renderer renderer;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        renderer = GetComponent<Renderer>();
        SetColor();
	}

    public void IncreaseCubeLevel()
    {
        cubeLevel += 1;
        SetColor();
    }

    void SetColor()
    {
        Material material = renderer.material;
        material.color = new HSBColor((float)((cubeLevel * 3) % 16) / 16.0f, 1, 1).ToColor();
        renderer.material = material;
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
                }
            } else
            {
                // we hit nothing
                MakeRigidBodyDraggable(false);
            }

        }

        
    }
}
