using UnityEngine;
using UnityEngine.UI;

public class SliderImageRognage : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Image img1, img2, img3, img4, img5, img6, img7, img8, img9, img10;

    void Update()
    {
        if (slider != null)
        {
            float normalizedValue = slider.normalizedValue;

            Debug.Log("normalizedValue: " + normalizedValue);

            if (normalizedValue == 0)
            {
                img1.gameObject.SetActive(false);
                img2.gameObject.SetActive(false);
                img3.gameObject.SetActive(false);
                img4.gameObject.SetActive(false);
                img5.gameObject.SetActive(false);
                img6.gameObject.SetActive(false);
                img7.gameObject.SetActive(false);
                img8.gameObject.SetActive(false);
                img9.gameObject.SetActive(false);
                img10.gameObject.SetActive(false);
            }
            else if (normalizedValue < 0.1f)
            {
                img1.gameObject.SetActive(true);
                img2.gameObject.SetActive(false);
                img3.gameObject.SetActive(false);
                img4.gameObject.SetActive(false);
                img5.gameObject.SetActive(false);
                img6.gameObject.SetActive(false);
                img7.gameObject.SetActive(false);
                img8.gameObject.SetActive(false);
                img9.gameObject.SetActive(false);
                img10.gameObject.SetActive(false);
            }
            else if (normalizedValue < 0.2f)
            {
                img1.gameObject.SetActive(true);
                img2.gameObject.SetActive(true);
                img3.gameObject.SetActive(false);
                img4.gameObject.SetActive(false);
                img5.gameObject.SetActive(false);
                img6.gameObject.SetActive(false);
                img7.gameObject.SetActive(false);
                img8.gameObject.SetActive(false);
                img9.gameObject.SetActive(false);
                img10.gameObject.SetActive(false);
            }
            else if (normalizedValue < 0.3f)
            {
                img1.gameObject.SetActive(true);
                img2.gameObject.SetActive(true);
                img3.gameObject.SetActive(true);
                img4.gameObject.SetActive(false);
                img5.gameObject.SetActive(false);
                img6.gameObject.SetActive(false);
                img7.gameObject.SetActive(false);
                img8.gameObject.SetActive(false);
                img9.gameObject.SetActive(false);
                img10.gameObject.SetActive(false);
            }
            else if (normalizedValue < 0.4f)
            {
                img1.gameObject.SetActive(true);
                img2.gameObject.SetActive(true);
                img3.gameObject.SetActive(true);
                img4.gameObject.SetActive(true);
                img5.gameObject.SetActive(false);
                img6.gameObject.SetActive(false);
                img7.gameObject.SetActive(false);
                img8.gameObject.SetActive(false);
                img9.gameObject.SetActive(false);
                img10.gameObject.SetActive(false);
            }
            else if (normalizedValue < 0.5f)
            {
                img1.gameObject.SetActive(true);
                img2.gameObject.SetActive(true);
                img3.gameObject.SetActive(true);
                img4.gameObject.SetActive(true);
                img5.gameObject.SetActive(true);
                img6.gameObject.SetActive(false);
                img7.gameObject.SetActive(false);
                img8.gameObject.SetActive(false);
                img9.gameObject.SetActive(false);
                img10.gameObject.SetActive(false);
            }
            else if (normalizedValue < 0.6f)
            {
                img1.gameObject.SetActive(true);
                img2.gameObject.SetActive(true);
                img3.gameObject.SetActive(true);
                img4.gameObject.SetActive(true);
                img5.gameObject.SetActive(true);
                img6.gameObject.SetActive(true);
                img7.gameObject.SetActive(false);
                img8.gameObject.SetActive(false);
                img9.gameObject.SetActive(false);
                img10.gameObject.SetActive(false);
            }
            else if (normalizedValue < 0.7f)
            {
                img1.gameObject.SetActive(true);
                img2.gameObject.SetActive(true);
                img3.gameObject.SetActive(true);
                img4.gameObject.SetActive(true);
                img5.gameObject.SetActive(true);
                img6.gameObject.SetActive(true);
                img7.gameObject.SetActive(true);
                img8.gameObject.SetActive(false);
                img9.gameObject.SetActive(false);
                img10.gameObject.SetActive(false);
            }
            else if (normalizedValue < 0.8f)
            {
                img1.gameObject.SetActive(true);
                img2.gameObject.SetActive(true);
                img3.gameObject.SetActive(true);
                img4.gameObject.SetActive(true);
                img5.gameObject.SetActive(true);
                img6.gameObject.SetActive(true);
                img7.gameObject.SetActive(true);
                img8.gameObject.SetActive(true);
                img9.gameObject.SetActive(false);
                img10.gameObject.SetActive(false);
            }
            else if (normalizedValue < 0.9f)
            {
                img1.gameObject.SetActive(true);
                img2.gameObject.SetActive(true);
                img3.gameObject.SetActive(true);
                img4.gameObject.SetActive(true);
                img5.gameObject.SetActive(true);
                img6.gameObject.SetActive(true);
                img7.gameObject.SetActive(true);
                img8.gameObject.SetActive(true);
                img9.gameObject.SetActive(true);
                img10.gameObject.SetActive(false);
            }
            else
            {
                img1.gameObject.SetActive(true);
                img2.gameObject.SetActive(true);
                img3.gameObject.SetActive(true);
                img4.gameObject.SetActive(true);
                img5.gameObject.SetActive(true);
                img6.gameObject.SetActive(true);
                img7.gameObject.SetActive(true);
                img8.gameObject.SetActive(true);
                img9.gameObject.SetActive(true);
                img10.gameObject.SetActive(true);
            }
        }
    }
}
