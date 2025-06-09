using System.Collections;
using UnityEngine;

public class DeliverSeniorSpawner : GPSBasedARSpawner
{
    [Header("🎯 전달 미션 UI")]
    public GameObject deliverMissionPanel;

    [Header("🎉 캐릭터 생성 알림 팝업")]

    private Coroutine popupCoroutine;

    /// <summary>
    /// Scene 2 - 미션 대화창을 표시
    /// </summary>
    public void ShowDeliverDialogue()
    {
        if (instructionPanel != null)
            instructionPanel.SetActive(false);

        if (deliverMissionPanel != null)
            deliverMissionPanel.SetActive(true);

        if (characterSpawnPopup != null)
            characterSpawnPopup.SetActive(false);
    }

    /// <summary>
    /// Scene 2 - 미션 대화창을 숨김
    /// </summary>
    public void HideDeliverDialogue()
    {
        if (deliverMissionPanel != null)
            deliverMissionPanel.SetActive(false);

        if (instructionPanel != null)
            instructionPanel.SetActive(false);

        if (characterSpawnPopup != null)
            characterSpawnPopup.SetActive(false);
    }

    /// <summary>
    /// Scene 2 - 미션 완료 시 처리
    /// </summary>
    public void CompleteMission()
    {
        Debug.Log("🎉 전달 미션 완료!");
        HideDeliverDialogue();
        // 필요한 추가 미션 완료 처리 가능
    }

    /// <summary>
    /// 팝업 자동 숨기기용 코루틴 (필요시 호출)
    /// </summary>
    private IEnumerator HideSpawnPopupAfterDelay(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        if (characterSpawnPopup != null)
            characterSpawnPopup.SetActive(false);
    }
}