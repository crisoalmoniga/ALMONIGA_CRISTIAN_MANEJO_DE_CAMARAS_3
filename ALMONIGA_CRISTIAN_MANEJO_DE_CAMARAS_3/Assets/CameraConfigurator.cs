using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraConfigurator : MonoBehaviour
{
    public Camera mainCamera; // Asignar la cámara principal desde el inspector
    public GameObject perspectiveControls; // Contenedor de controles de FOV
    public GameObject orthographicControls; // Contenedor de controles de Size
    public Slider fovSlider; // Slider para FOV
    public Slider sizeSlider; // Slider para tamaño ortográfico
    public Slider nearClipSlider; // Slider para Near Clip Plane
    public Slider farClipSlider; // Slider para Far Clip Plane
    public GameObject frustumGizmo; // GameObject para el gizmo del frustum

    private void Awake()
    {
        // Asignar la cámara principal automáticamente si no se asignó desde el inspector
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Asigna automáticamente la cámara principal
        }
    }

    private void Start()
    {
        Debug.Log(mainCamera != null ? "mainCamera asignada" : "mainCamera es NULL");
        Debug.Log(fovSlider != null ? "fovSlider asignado" : "fovSlider es NULL");

        // Verificar que todas las referencias estén asignadas
        if (mainCamera == null || fovSlider == null || sizeSlider == null || nearClipSlider == null || farClipSlider == null)
        {
            Debug.LogError("Faltan referencias en el script CameraConfigurator. Revisa las asignaciones en el Inspector.");
            return; // Evitar ejecutar el código si faltan referencias
        }

        // Inicializar sliders con los valores actuales de la cámara
        fovSlider.value = mainCamera.fieldOfView;
        sizeSlider.value = mainCamera.orthographicSize;
        nearClipSlider.value = mainCamera.nearClipPlane;
        farClipSlider.value = mainCamera.farClipPlane;

        // Configurar límites
        fovSlider.maxValue = 180;
        sizeSlider.maxValue = 100;
        nearClipSlider.maxValue = 100;
        farClipSlider.maxValue = 1000;

        UpdateUI(); // Asegurarse de que los controles correctos se muestren
    }

    public void ToggleProjection()
    {
        // Cambiar entre perspectiva y ortográfica
        mainCamera.orthographic = !mainCamera.orthographic;
        UpdateUI();
    }

    public void UpdateFOV()
    {
        // Actualizar el FOV
        if (mainCamera != null)
        {
            mainCamera.fieldOfView = fovSlider.value;
        }
    }

    public void UpdateSize()
    {
        // Actualizar el tamaño ortográfico
        if (mainCamera != null)
        {
            mainCamera.orthographicSize = sizeSlider.value;
        }
    }

    public void UpdateClippingPlanes()
    {
        // Actualizar Near y Far Clip Planes
        if (mainCamera != null)
        {
            mainCamera.nearClipPlane = nearClipSlider.value;
            mainCamera.farClipPlane = farClipSlider.value;
        }
    }

    private void UpdateUI()
    {
        // Mostrar controles según el tipo de proyección
        if (mainCamera == null) return;

        if (mainCamera.orthographic)
        {
            perspectiveControls?.SetActive(false);
            orthographicControls?.SetActive(true);
        }
        else
        {
            perspectiveControls?.SetActive(true);
            orthographicControls?.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        // Dibujar el frustum de la cámara en amarillo
        if (mainCamera == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.matrix = mainCamera.transform.localToWorldMatrix;
        Gizmos.DrawFrustum(Vector3.zero, mainCamera.fieldOfView, mainCamera.farClipPlane, mainCamera.nearClipPlane, mainCamera.aspect);
    }

    public void TestFunction()
    {
        Debug.Log("Button Clicked!");
    }
}