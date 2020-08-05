using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Assign this script to the indicator prefabs.
/// </summary>
/// 

public enum IndicatorType
{
    BOX = 0,
    ARROW
}

public class Indicator : MonoBehaviour
{
    [SerializeField] private IndicatorType indicatorType = 0;
    private Image indicatorImage;
    private Text distanceText;

    /// <summary>
    /// Gets if the game object is active in hierarchy.
    /// </summary>
    public bool Active
    {
        get
        {
            return transform.gameObject.activeInHierarchy;
        }
    }

    /// <summary>
    /// Gets the indicator type
    /// </summary>
    public IndicatorType Type
    {
        get
        {
            return indicatorType;
        }
    }

    void Awake()
    {
        try
        {
            indicatorImage = transform.GetComponent<Image>();
            distanceText = transform.GetComponentInChildren<Text>();
        }
        catch
        {
            Debug.Log("Indicator.Awake Error");
        }
    }

    /// <summary>
    /// Sets the image color for the indicator.
    /// </summary>
    /// <param name="color"></param>
    public void SetImageColor(Color color)
    {
        try
        {
            indicatorImage.color = color;
        }
        catch
        {
            Debug.Log("Indicator.SetImageColor Error");
        }
    }

    /// <summary>
    /// Sets the distance text for the indicator.
    /// </summary>
    /// <param name="value"></param>
    public void SetDistanceText(float value)
    {
        try
        {
            distanceText.text = value >= 0 ? Mathf.Floor(value) + " m" : "";
        }
        catch
        {
            Debug.Log("Indicator.SetDistanceText Error");
        }
    }

    /// <summary>
    /// Sets the distance text rotation of the indicator.
    /// </summary>
    /// <param name="rotation"></param>
    public void SetTextRotation(Quaternion rotation)
    {
        try
        {
            distanceText.rectTransform.rotation = rotation;
        }
        catch
        {
            Debug.Log("Indicator.SetTextRotation Error");
        }
    }

    /// <summary>
    /// Sets the indicator as active or inactive.
    /// </summary>
    /// <param name="value"></param>
    public void Activate(bool value)
    {
        try
        {
            transform.gameObject.SetActive(value);
        }
        catch
        {
            Debug.Log("Indicator.Activate Error");
        }
    }
}
