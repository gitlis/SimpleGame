using UnityEngine.EventSystems;
using UnityEngine;

public class MouseInputController : BaseController
{
    private UIManager uiManager;

    private Camera camera2D;
    private float rayDistance = 10;
    private FigureController underMouseFigure;
    private FigureController catchedFigure;
    private Vector2 lastMousePosition;

    private float lastClickTime;
    private bool IsDoubleClick;
    private float delayForDoubleClick;

    public delegate void ScoreEvent();
    public ScoreEvent SquareGotTarget;
    public delegate void Event(GameObject figure);
    public Event SquareDeleted;

    enum MouseButton
    {
        LeftButton,
        RightButton
    }

    private void Awake()
    {
        camera2D = Camera.main;

        lastMousePosition = new Vector3();
        delayForDoubleClick = 0.5f;
        uiManager = GameObject.FindObjectOfType<UIManager>();
        SquareDeleted += uiManager.gameController.ReplaceFigure;
    }

    public override void On()
    {
        base.On();      

        underMouseFigure = null;
        catchedFigure = null;
        lastClickTime = 0;
        IsDoubleClick = false;
    }

    void Update()
    {
        if (!Enabled) return;

#if MOBILE_INPUT
        TouchControll();
#else
        MouseControll();
#endif
    }

    private void MouseControll()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        Ray ray = camera2D.ScreenPointToRay(currentMousePosition);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
        RaycastHit hit;
        Collider collider = null;

        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (Physics.Raycast(ray, out hit, rayDistance)) collider = hit.collider;

            underMouseFigure = SearchFigureUnderMouse(collider);
            CheckMouseClick();
        }

        MoveFigureUnderPointer(camera2D.ScreenToWorldPoint(currentMousePosition));
    }
    private void TouchControll()
    {
        if (Input.touchCount > 0)
        {
            Vector3 currentFingerTouchPosition = Input.GetTouch(0).position;
            Ray ray = camera2D.ScreenPointToRay(currentFingerTouchPosition);
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
            RaycastHit hit;
            Collider collider = null;

            if (Physics.Raycast(ray, out hit, rayDistance)) collider = hit.collider;
            underMouseFigure = SearchFigureUnderMouse(collider);
            CheckTouchClick();

            if (Input.GetTouch(0).phase == TouchPhase.Moved) MoveFigureUnderPointer(camera2D.ScreenToWorldPoint(currentFingerTouchPosition));
        }
    }

    private FigureController SearchFigureUnderMouse(Collider collider)
    {
        if (collider != null)
        {
            var figureUnderMouse = collider.GetComponent<FigureController>();
            if (figureUnderMouse != null) return figureUnderMouse;
            else return null;
        }
        else return null;
    }

    private void CheckMouseClick()
    {
        if (Input.GetMouseButtonDown((int)MouseButton.LeftButton)) PushDown();
        if (Input.GetMouseButtonUp((int)MouseButton.LeftButton)) PushUp();
    }
    private void CheckTouchClick() 
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began) PushDown();
        if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled) PushUp();
    }

    private void PushDown()
    {
        if (Time.time - lastClickTime > delayForDoubleClick) IsDoubleClick = false;
        else if (underMouseFigure == catchedFigure) IsDoubleClick = true;

        if (underMouseFigure != null)
        {
            if (IsDoubleClick)
            {
                DoubleClickOnFigure(underMouseFigure);
                if (Application.platform == RuntimePlatform.WindowsEditor) Debug.Log("Двойной щелчок на " + underMouseFigure.TypeFigure);
            }
            else
            {
                ClickOnFigure(underMouseFigure);
                lastClickTime = Time.time;
                if (Application.platform == RuntimePlatform.WindowsEditor) Debug.Log("Щелчок на " + underMouseFigure.TypeFigure);
            }
        }
        else
        {
            ClickOnUI();
        }
    }
    private void PushUp()
    {
        if (catchedFigure != null)
        {
            if (underMouseFigure == null) ClickOnUI();
            else if (underMouseFigure != catchedFigure) ClickOnFigure(underMouseFigure);
        }
    }

    private void ClickOnFigure(FigureController clickedFigure)
    {
        switch (clickedFigure.TypeFigure)
        {
            case FigureController.FigureType.Square:
                {
                    if (catchedFigure != null)
                    {
                       if (catchedFigure != clickedFigure) ClickOnUI();
                    }
                    else SelectFigure(clickedFigure);
                    break;
                }
            case FigureController.FigureType.Circle:
                {
                    if (catchedFigure != null)
                    {
                        if (catchedFigure.Color == clickedFigure.Color)
                        {
                            SquareDeleted.Invoke(catchedFigure.gameObject);
                            SquareGotTarget.Invoke();
                            DeselectFigure();

                        }
                        else ClickOnUI();
                    }
                    break;
                }
        }
    }
    private void ClickOnUI()
    {
        if (catchedFigure != null)
        {
            var droppedFigure = catchedFigure;
            DeselectFigure();
            droppedFigure.ReturnToHome();
        }

        if (Application.platform == RuntimePlatform.WindowsEditor) Debug.Log("Щелчок на пустом поле");   
    }
    private void DoubleClickOnFigure(FigureController clickedFigure)
    {
        SquareDeleted.Invoke(clickedFigure.gameObject);
    }

    private void SelectFigure(FigureController figure)
    {
        catchedFigure = figure;
        catchedFigure.IsCatched = true;
        catchedFigure.spriteRenderer.sortingOrder++;
    }
    private void DeselectFigure()
    {
        catchedFigure.spriteRenderer.sortingOrder--;
        catchedFigure.IsCatched = false;
        catchedFigure = null;
    }

    private void MoveFigureUnderPointer(Vector2 currentMousePosition)
    {
        if (lastMousePosition != currentMousePosition && catchedFigure != null)
        {
            catchedFigure.transform.position = new Vector3(currentMousePosition.x, currentMousePosition.y - 7, catchedFigure.transform.position.z);
            lastMousePosition = currentMousePosition;
        }
    }
}
