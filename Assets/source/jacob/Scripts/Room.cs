using NUnit.Framework;
using UnityEngine.Assertions;
using UnityEngine;
using System.Numerics;
using System;

public class Room : MonoBehaviour
{
    [SerializeField] private string roomID;
    [SerializeField] private Vector2Int roomSize;
    private Vector2Int location;
    public GameObject LeftHall;
    public GameObject RightHall;
    public GameObject UpHall;
    public GameObject BottomHall;

    public string RoomID => roomID;
    public Vector2Int RoomSize => roomSize;
    public Vector2Int Location {get { return location; } set{ location = value; }}
}
