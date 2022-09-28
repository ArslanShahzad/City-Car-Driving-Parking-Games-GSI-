using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Threading.Tasks;

public class ClickEffect : MonoBehaviour
{
    public GameObject UpDownPanel, LeftRightPanel;
    public GameObject CanvasPanel;
    public async void ChangePanel(GameObject CanvasPanel)
    {
        await Task.Delay(200);
        LeftRightPanel.SetActive(false);
        LeftRightPanel.SetActive(true);
        CanvasPanel.SetActive(true);
    }
    public async void ClosePanel(GameObject PanelToClose)
    {
        UpDownPanel.SetActive(true);
        await Task.Delay(350);
        UpDownPanel.SetActive(false);
        PanelToClose.SetActive(false);

    }


}
