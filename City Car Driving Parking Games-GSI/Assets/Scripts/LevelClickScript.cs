
using UnityEngine;
using UnityEngine.EventSystems;
public class LevelClickScript : MonoBehaviour
{

   public void LevelClick()
    {
        LevelSelectionManager.Instance.SelectLvl(int.Parse(EventSystem.current.currentSelectedGameObject.name));
       
    }
}
