using UnityEngine;

public class MonsterCanInteraction : MonoBehaviour
{
    public GameObject oldText;
    public GameObject newText;
    public GameObject actionButton;

    void Update()
    {
        // 1. 터치가 시작됐는지 확인
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("터치 감지됨");

            // 2. 화면 터치 지점을 기준으로 Ray 쏘기
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            // 3. UI 레이어 제외하는 마스크 설정
            int layerMask = ~(1 << LayerMask.NameToLayer("UI"));

            // 4. 실제로 무언가를 맞췄는지 확인
            if (Physics.Raycast(ray, out hit, 10f, layerMask))
            {
                Debug.Log("Ray가 맞은 대상: " + hit.transform.name);

                // 5. 이 오브젝트가 몬스터캔인지 확인 (넉넉한 이름 비교)
                if (hit.transform.name.Contains("Monstercan"))
                {
                    Debug.Log("몬스터캔 클릭 성공!");

                    // 6. 몬스터캔 제거 및 UI 전환
                    Destroy(hit.transform.gameObject);
                    oldText.SetActive(false);
                    newText.SetActive(true);
                    actionButton.SetActive(true);
                }
                else
                {
                    Debug.Log("맞긴 했지만 몬스터캔은 아님: " + hit.transform.name);
                }
            }
            else
            {
                Debug.Log("Ray가 아무것도 못 맞췄어");
            }
        }
    }
}





