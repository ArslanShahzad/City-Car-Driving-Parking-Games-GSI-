using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LevelHandler", menuName = "LEVELHANDLER")]
public class LevelHandler : ScriptableObject {

	public int CurrentLevel;
	public int LevelsUnlocked;
}
