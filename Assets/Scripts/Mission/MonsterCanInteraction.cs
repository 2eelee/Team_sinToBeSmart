using UnityEngine;

public class MonsterCanInteraction : MonoBehaviour
{
    public GameObject oldText;
    public GameObject newText;
    public GameObject actionButton;

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("터치 감지됨");

            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            // 디버그용 Ray 시각화 (Scene 뷰에서 확인 가능)
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);

            // UI 레이어 제외한 레이어 마스크
            int layerMask = ~(1 << LayerMask.NameToLayer("UI"));

            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                Debug.Log("Ray가 맞은 대상: " + hit.transform.name);

                // 방법 1: 이름으로 판별 (기존 방식 유지)
                bool isMonsterByName = hit.transform.name.ToLower().Contains("monstercan");

                // 방법 2: 태그로 명확하게 구분 (더 안전한 방식)
                bool isMonsterByTag = hit.transform.CompareTag("Collectable");

                if (isMonsterByName || isMonsterByTag)
                {
                    Debug.Log("몬스터캔 클릭 성공!");

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
