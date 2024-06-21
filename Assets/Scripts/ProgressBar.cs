using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProgressBar : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image image;

    [Header("Properties")]
    public int value;
    public int maxValue;
    public float assignedTime;
    private bool isCorrectlyConfigured = false;
    private bool active = false;

    //Валидация конфигурации (тест)
    private void Awake()
    {
        if (image.type == Image.Type.Filled & image.fillMethod == Image.FillMethod.Vertical & maxValue > 0) {
            isCorrectlyConfigured = true;
        } else {
            Debug.Log("{GameLog} => {ProgressBar} - {<color=red>Error</color>} -> Components Parameters are configurred incorrectly \n" +
                     "Required type: Filled \n" +
                     "Required FillMethod: Vertical \n" +
                     "Required maxValue: > 0");
        }
    }

    void Start()
    {

    }

    void Update()
    {
        assignedTime -= Time.deltaTime;
        if (assignedTime <= 0.0f)
        {
            active = true;
        }
    }

    private void LateUpdate()
    {
        if (!isCorrectlyConfigured) return;
        if (active) {
            transform.localPosition = new Vector3(0, 0, 0);
            if (Input.GetKeyDown("x") || Input.GetKeyDown("c")) {
                value++;
            }
            image.fillAmount = (float) value / maxValue;

            if (image.fillAmount == 1) Destroy(gameObject);
        }

    }

    public void SetValue(int value) => this.value = value;
    public void SetMaxValue(int maxValue) => this.maxValue = maxValue;
}
