using Enums;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Player", order = 1)]
public class Player : ScriptableObject
{
    public int PlayerId;
    public string PlayerName;
    public Sprite SeedImage;
    public PlayerController playerController;
}