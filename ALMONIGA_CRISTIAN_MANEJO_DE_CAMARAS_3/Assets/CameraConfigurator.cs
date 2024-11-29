using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraConfigurator : MonoBehaviour
{
    public Camera mainCamera; // Asignar la c�mara principal desde el inspector
    public GameObject perspectiveControls; // Contenedor de controles de FOV
    public GameObject orthographicControls; // Contenedor de controles de Size
    public Slider fovSlider; // Slider para FOV
    public Slider sizeSlider; // Slider para tama�o ortogr�fico
    public Slider nearClipSlider; // Slider para Near Clip Plane
    public Slider farClipSlider; // Slider para Far Clip Plane
    public GameObject frustumGizmo; // GameObject para el gizmo del frustum

    private void Awake()
    {
        // Asignar la c�mara principal autom�ticamente si no se asign� desde el inspector
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Asigna autom�ticamente la c�mara principal
        }
    }

    private void Start()
    {
        Debug.Log(mainCamera != null ? "mainCamera asignada" : "mainCamera es NULL");
        Debug.Log(fovSlider != null ? "fovSlider asignado" : "fovSlider es NULL");

        // Verificar que todas las referencias est�n asignadas
        if (mainCamera == null || fovSlider == null || sizeSlider == null || nearClipSlider == null || farClipSlider == null)
        {
            Debug.LogError("Faltan referencias en el script CameraConfigurator. Revisa las asignaciones en el Inspector.");
            return; // Evitar ejecutar el c�digo si faltan referencias
        }

        // Inicializar sliders con los valores actuales de la c�mara
        fovSlider.value = mainCamera.fieldOfView;
        sizeSlider.value = mainCamera.orthographicSize;
        nearClipSlider.value = mainCamera.nearClipPlane;
        farClipSlider.value = mainCamera.farClipPlane;

        // Configurar l�mites
        fovSlider.maxValue = 180;
        sizeSlider.maxValue = 100;
        nearClipSlider.maxValue = 100;
        farClipSlider.maxValue = 1000;

        UpdateUI(); // Asegurarse de que los controles correctos se muestren
    }

    public void ToggleProjection()
    {
        // Cambiar entre perspectiva y ortogr�fica
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
        // Actualizar el tama�o ortogr�fico
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
        // Mostrar controles seg�n el tipo de proyecci�n
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
        // Dibujar el frustum de la c�mara en amarillo
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