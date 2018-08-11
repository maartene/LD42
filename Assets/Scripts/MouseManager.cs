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

public class MouseManager : MonoBehaviour {

    Camera mainCamera;

    Cubie currentCubie;

	// Use this for initialization
	void Start () {
        mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Cubie c = hit.collider.GetComponent<Cubie>();
                if (c != null)
                {
                    // We hit a cube!
                    if (currentCubie == c)
                    {
                        // we are already dragging this cube
                        c.OnDrag(Input.mousePosition);
                    } else if (currentCubie == null)
                    {
                        // this is a new drag
                        currentCubie = c;
                        c.OnDragStart(Input.mousePosition);
                    } else
                    {
                        // t.b.d.
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (currentCubie == null)
            {
                return;
            } else
            {
                currentCubie.OnDragEnd(Input.mousePosition);
                currentCubie = null;
            }


        }
	}
}
