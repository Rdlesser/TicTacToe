using Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 2)]
public class GameData : ScriptableObject
{
    public GameMode GameMode;
    public GameState GameState = GameState.MainMenu;
}