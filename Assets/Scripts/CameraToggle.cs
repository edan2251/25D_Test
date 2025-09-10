using UnityEngine;

public class CameraToggle : MonoBehaviour
{
    public GameObject camera2D;           // 2D ī�޶�
    public GameObject freeLookCamera;     // FreeLook ī�޶� (Cinemachine Virtual Camera ����)

    public static bool use2D = true;

    void Start()
    {
        // ������ 2D ī�޶��
        camera2D.SetActive(true);
        freeLookCamera.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) // ���ϴ� Ű ����
        {
            use2D = !use2D;
            camera2D.SetActive(use2D);
            freeLookCamera.SetActive(!use2D);
        }
    }
}