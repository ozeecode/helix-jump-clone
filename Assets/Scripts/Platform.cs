using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Platform : MonoBehaviour
{

    [SerializeField] float dragMultipler = 1f;
    [SerializeField] GameObject LayerZero;
    [SerializeField] GameObject LayerFinal;
    [SerializeField] GameObject[] Layers;
    [SerializeField] int layerCount = 10;


    List<GameObject> layers = new List<GameObject>();
    PlayerControls playerControls;
    bool isDragging;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }
    private void OnEnable()
    {
        playerControls.Game.Enable();
        playerControls.Game.Touch.performed += OnTouch;
        playerControls.Game.Touch.canceled += OnTouch;
        playerControls.Game.Drag.performed += OnDrag;
    }

    private void OnDisable()
    {
        playerControls.Game.Touch.performed -= OnTouch;
        playerControls.Game.Touch.canceled -= OnTouch;
        playerControls.Game.Drag.performed -= OnDrag;
        playerControls.Game.Disable();
    }

    public void Init()
    {
        //Clear old layers
        for (int i = layers.Count - 1; i >= 0; i--)
        {
            Destroy(layers[i]);
        }
        layers.Clear();


        Vector3 pos = Vector3.zero;
        layers.Add(Instantiate(LayerZero, pos, Quaternion.identity, transform));
        pos += Vector3.down * GameSettings.DISTANCE_BETWEEN_PLATFORMS;
        for (int i = 1; i < layerCount; i++)
        {
            layers.Add(Instantiate(Layers[Random.Range(0, Layers.Length)], pos, Quaternion.Euler(0, Random.Range(0, 360), 0), transform));
            pos += Vector3.down * GameSettings.DISTANCE_BETWEEN_PLATFORMS;
        }
        layers.Add(Instantiate(LayerFinal, pos, Quaternion.identity, transform));

    }
    private void OnDrag(InputAction.CallbackContext ctx)
    {
        if (GameManager.Instance.GameState != GameState.Play) return;
        if (!isDragging) return;

        //Fiziksel bir sürükleme söz konusu bu sebeple deltaTime kullanmasak iyi olur gibi??
        //transform.Rotate(new Vector3(0, -ctx.ReadValue<Vector2>().x, 0) * Time.deltaTime * dragMultipler); 
        transform.Rotate(new Vector3(0, -ctx.ReadValue<Vector2>().x, 0) * dragMultipler);
    }

    public void OnTouch(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Canceled)
        {
            isDragging = false;
        }
        else
        {
            isDragging = true;
        }
    }

}
