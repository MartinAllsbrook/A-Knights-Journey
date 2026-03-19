using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DashSlider : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI readyText;
    [SerializeField] Image fillImage;


    public void SetFill(float fillAmount)
    {
        slider.value = fillAmount;
        if (fillAmount >= 1f)
        {
            readyText.gameObject.SetActive(true);
            fillImage.color = Color.white;
        }
        else
        {
            readyText.gameObject.SetActive(false);
            fillImage.color = Color.gray;
        }
    }
}
