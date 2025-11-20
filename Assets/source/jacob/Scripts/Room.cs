using NUnit.Framework;
using UnityEngine.Assertions;
using UnityEngine;
using System.Numerics;
using System;
using UnityEngine.SceneManagement;

public class Room : MonoBehaviour
{
    [SerializeField] private string roomID;
    [SerializeField] private Vector2Int roomSize;
    private Vector2Int location;
    public GameObject LeftHall;
    public GameObject LeftDoor;
    public GameObject RightHall;
    public GameObject RightDoor;
    public GameObject TopHall;
    public GameObject TopDoor;
    public GameObject BottomHall;
    public GameObject BottomDoor;
    public bool HasTopHall = false;
    public bool HasBottomHall = false;
    public bool HasLeftHall = false;
    public bool HasRightHall = false;
	public string RoomID => roomID;
    public Vector2Int RoomSize => roomSize;
    public Vector2Int Location {get { return location; } set{ location = value; }}
}
