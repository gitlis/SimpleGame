using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    private bool _enabled = false;

    void Awake()
    {
        On();
    }

    public bool Enabled
    {
        get { return _enabled; }
        private set { _enabled = value; }
    }

    /// <summary>
    /// Включение контролллера
    /// </summary>
    public virtual void On()
    {
        _enabled = true;
    }

    /// <summary>
    /// Выключение контроллера
    /// </summary>
    public virtual void Off()
    {
        _enabled = false;
    }
}
