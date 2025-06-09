using UnityEngine;

public class SkipLaptopAndNextUI : MonoBehaviour
{
    public GameObject laptopObject;     // 씬에 떠 있는 laptopPrefab 인스턴스 (없어도 null 처리함)
    public GameObject panelIntro;       // 기존 안내 패널
    public GameObject panelNext;        // 다음 패널 (ex: 획득 완료 UI)
    public GameObject nextButton;       // 다음으로 넘어가는 버튼 (선택)

    public void OnClickSkip()
    {
        Debug.Log("강제로 랩탑 넘기기!");

        // 1. laptop 오브젝트 제거
        if (laptopObject != null)
        {
            Destroy(laptopObject);
            Debug.Log("랩탑 오브젝트 제거");
        }

        // 2. PlayerPrefs 등록 처리
        PlayerPrefs.SetInt("Laptop", 1);
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs: Laptop = 1 저장");

        // 3. UI 패널 전환
        if (panelIntro != null) panelIntro.SetActive(false);
        if (panelNext != null) panelNext.SetActive(true);
        if (nextButton != null) nextButton.SetActive(true);
        Debug.Log("UI 전환 완료");
    }
}
