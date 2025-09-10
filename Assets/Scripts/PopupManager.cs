using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public RawImage[] popups;    // Raw Image 4��
    public GameObject panel;     // Panel ������Ʈ
    private int currentIndex = 0;

    void Start()
    {
        ShowPanel(); // ���� �� Panel �ѱ�
    }

    void Update()
    {
        if (panel == null || popups == null || popups.Length == 0)
            return;

        // ���� �̹���
        if (Input.GetKeyDown(KeyCode.M) || Input.GetMouseButtonDown(0))
        {
            ShowNext();
        }

        // ���� �̹���
        if (Input.GetKeyDown(KeyCode.N))
        {
            ShowPrevious();
        }

        // Panel ���
        if (Input.GetKeyDown(KeyCode.B))
        {
            TogglePanel();
        }
    }

    void ShowNext()
    {
        if (currentIndex < 0 || currentIndex >= popups.Length)
            return;

        popups[currentIndex].enabled = false;
        currentIndex++;

        if (currentIndex >= popups.Length)
        {
            panel.SetActive(false); // ���̸� Panel ����
            currentIndex = popups.Length - 1; // �����ϰ� ������ �ε��� ����
            return;
        }

        popups[currentIndex].enabled = true;
    }

    void ShowPrevious()
    {
        if (currentIndex <= 0)
        {
            currentIndex = 0; // ù �������� �ƹ� ���� ����
            return;
        }

        popups[currentIndex].enabled = false;
        currentIndex--;
        popups[currentIndex].enabled = true;
    }

    void ShowPanel()
    {
        if (panel == null || popups == null || popups.Length == 0)
            return;

        panel.SetActive(true);

        // ��� �̹��� ����
        for (int i = 0; i < popups.Length; i++)
        {
            if (popups[i] != null)
                popups[i].enabled = false;
        }

        currentIndex = 0;

        // ù �̹��� �ѱ�
        if (popups[0] != null)
            popups[0].enabled = true;
    }

    void TogglePanel()
    {
        if (panel == null)
            return;

        bool isActive = panel.activeSelf;
        if (isActive)
        {
            panel.SetActive(false); // ���������� ����
        }
        else
        {
            ShowPanel(); // ���������� �ѱ� + ù �̹����� �ʱ�ȭ
        }
    }
}
