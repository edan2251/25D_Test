using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform player;  // ���� �÷��̾�
    public float smoothSpeed = 5f;

    private float fixedY;
    private float fixedZ;

    void Start()
    {
        // ������ �� ī�޶��� Y, Z �� ����
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // �÷��̾��� X�� ����
        Vector3 targetPos = new Vector3(player.position.x, fixedY, fixedZ);

        // �ε巴�� �̵�
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
    }
}