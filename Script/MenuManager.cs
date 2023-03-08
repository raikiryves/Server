using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static MenuManager instance;
    public Menu _menu;

    [Header("Game Object Menu")]
    public GameObject HomeMenu;

    public enum Menu
    {
        Loading = 1,
        Home,
        Register
    }

    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_menu)
        {
            case Menu.Loading:
                break;
            case Menu.Home:
                break;
            case Menu.Register:
                break;

        }
    }
}
