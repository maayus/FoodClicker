using TMPro;
using UnityEngine;

//singleton pattern - only one instance of this class can exist
//can be accessed from any other script without creating an instance of it
public class UiManager : MonoBehaviour
{
    //step 1: create a static instance of the class
    public static UiManager instance;

    public TextMeshProUGUI clickText;
    public TextMeshProUGUI speedText;
    public int clicks = 0;

    //ste 2: method that executes before Start() method
    private void Awake()
    {
        //step 3: check if no instance exists
        if(instance == null)
        {
            //step 4: assign this instance to the static instance
            instance = this;
        }
        else
        {
            //step 5: destroy this instance if another instance exists
            Destroy(gameObject);
            Debug.LogWarning("Another instance of UiManager exists");
        }
    }

    public void Update()
    {
        speedText.text = clicks.ToString();
    }

    public void UpdateClicks(int ccc)
    {
        clickText.text = ccc.ToString();
    }

    public void Count()
    {
        if (clicks > 0)
        {
            clicks--;
        }
    }
}
