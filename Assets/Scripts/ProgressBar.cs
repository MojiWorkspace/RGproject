using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProgressBar : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image image;
    public LaneBtn[] buttons;

    [Header("Properties")]
    public int value;
    public int maxValue;
    private bool isCorrectlyConfigured = false;
    public bool isActive = false;
    public float isActiveAfterTime;
    public float activeTime;

    //Валидация конфигурации (тест)
    private void Awake()
    {
        if (image.type == Image.Type.Filled & image.fillMethod == Image.FillMethod.Vertical & maxValue > 0)
        {
            isCorrectlyConfigured = true;
        }
        else
        {
            Debug.Log("{GameLog} => {ProgressBar} - {<color=red>Error</color>} -> Components Parameters are configurred incorrectly \n" +
                     "Required type: Filled \n" +
                     "Required FillMethod: Vertical \n" +
                     "Required maxValue: > 0");
        }
    }

    void Start()
    {
        GetComponent<Transparency>().defaultTransparency = 0;

        foreach (var item in buttons) item.isActive = false;
    }

    void Update()
    {
        if (!isCorrectlyConfigured) return;

        if (isActive)
        {
            //планируется добавить полноценную логику с обратным отсчетом
            isActiveAfterTime -= Time.deltaTime;

            GetComponent<Transparency>().changeSpeed = 0.5f;

            if (isActiveAfterTime <= 0)
            {
                foreach (var item in buttons) item.isActive = true;

                if (Input.GetKeyDown("x") || Input.GetKeyDown("c")) value++;

                image.fillAmount = (float) value / maxValue;

                activeTime -= Time.deltaTime;

                // В дальнейшем планируется реализация: if fillAmount != 1 и activeTime <= 0 тогда конец игры
                if (image.fillAmount == 1 || activeTime <= 0) Destroy(gameObject);
            }
        }
    }

    public void SetValue(int value) => this.value = value;
    public void SetMaxValue(int maxValue) => this.maxValue = maxValue;
}
