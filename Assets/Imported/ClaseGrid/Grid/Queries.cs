﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Queries : MonoBehaviour
{
    public bool isBox;
    public float radius = 20f;
    public IEnumerable<GridEntity> selected = new List<GridEntity>();

    public bool showGizmos;
    public SpatialGrid _targetGrid;
    private float width;
    private float height;

    public Queries Initialize(SpatialGrid targetGrid)
    {
        _targetGrid = targetGrid;
        //width = _targetGrid.width;
        //height = _targetGrid.height;
        
        return this;
    }
    
    
    public IEnumerable<GridEntity> Query()
    {
        if (isBox)
        {
            var h = height * 0.5f;
            var w = width * 0.5f;
            //posicion inicial --> esquina superior izquierda de la "caja"
            //posición final --> esquina inferior derecha de la "caja"
            //como funcion para filtrar le damos una que siempre devuelve true, para que no filtre nada.
            return _targetGrid.Query(
                transform.position + new Vector3(-w, -h, 0),
                transform.position + new Vector3(w, h, 0),
                x => true);
        }
        else
        {
            //creo una "caja" con las dimensiones deseadas, y luego filtro segun distancia para formar el círculo
            return _targetGrid.Query(
                transform.position + new Vector3(-radius, 0, -radius),
                transform.position + new Vector3(radius,  0, radius),
                x => {
                    var position2d = x - transform.position;
                    position2d.y = 0;
                    return position2d.sqrMagnitude < radius * radius;
                });
        }
    }

    /*void OnDrawGizmos()
    {
        if (_targetGrid == null || !showGizmos) return;
             
        //Flatten the sphere we're going to draw
        Gizmos.color = Color.cyan;
        if (isBox)
            Gizmos.DrawWireCube(transform.position, new Vector3(width,  height,0));
        else
        {
            Gizmos.matrix *= Matrix4x4.Scale(Vector3.forward + Vector3.right);
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        if (Application.isPlaying)
        {
            selected = Query();
            var temp = FindObjectsOfType<GridEntity>().Where(x=>!selected.Contains(x));
            foreach (var item in temp)
            {
                item.isOnGrid = false;
            }
            foreach (var item in selected)
            {
                item.isOnGrid = true;
            }

        }
    }*/
    private void OnGUI() => GUI.Label( new Rect(0,0,20,20), "HOLA");
}
