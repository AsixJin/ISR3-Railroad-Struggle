using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelShow : MonoBehaviour {

    public Image mPanel;

	public void ShowHide()
    {
        if(mPanel.gameObject.activeSelf)
        {
            mPanel.gameObject.SetActive(false);
        }
        else
        {
            mPanel.gameObject.SetActive(true);
        }
    }
}
