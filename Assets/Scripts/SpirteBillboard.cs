//using UnityEngine;

//public class SpirteBillboard : MonoBehaviour
//{
//    [SerializeField] bool freezeXZAxis = true;
//    private void Update()
//    {
//        if (freezeXZAxis)
//        {
//            transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
//        }
//        else
//        {
//            transform.rotation = Camera.main.transform.rotation;
//        }
//    }
//}
using UnityEngine;

public class SpirteBillboard : MonoBehaviour
{
    [SerializeField] bool freezeXZAxis = true;

    private void Update()
    {
        // 2D ����̸� ���� ����
        if (CameraToggle.use2D)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f); // ���� ����
            return;
        }

        // 3D ����� �� Billboard
        if (freezeXZAxis)
        {
            transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
        }
        else
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}