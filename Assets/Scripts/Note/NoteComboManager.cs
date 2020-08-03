using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComboCount
{
    MISS = 0,
    COOL = 1,
    GREAT = 3,
    PERFECT = 5,
}

public class NoteComboManager : MonoBehaviour
{
    [SerializeField] GameObject[] goComboImage = null;

    private ComboCount MISS, COOL, GREAT, PERFECT;

    NoteEffectManager noteEffectManager = null;
    StatusManager status = null;

    int currentCombo = 0;

    void Start()
    {
        noteEffectManager = FindObjectOfType<NoteEffectManager>();
        status = FindObjectOfType<StatusManager>();

        for (int i = 0; i < goComboImage.Length; i++)
        {
            goComboImage[i].SetActive(false);
        }
    }

    //콤보 숫자를 계산해서, Cool, Great, Perfect 나오게 함.
    public void IncreaseCombo(int p_num = 1)
    {
        try
        {
            currentCombo += p_num;

            goComboImage[0].SetActive(false);

            COOL = ComboCount.COOL;
            GREAT = ComboCount.GREAT;
            PERFECT = ComboCount.PERFECT;

            ComboCheck();
        }
        catch
        {
            Debug.Log("NoteComboManager.IncreaseCombo Error");
        }
        
    }

    private void ComboCheck()
    {
        if (currentCombo > (int)COOL && currentCombo <= (int)GREAT)
        {
            goComboImage[1].SetActive(true);
            noteEffectManager.NoteBounce();
        }
        if (currentCombo > (int)GREAT && currentCombo <= (int)PERFECT)
        {
            goComboImage[1].SetActive(false);
            goComboImage[2].SetActive(true);
            noteEffectManager.NoteBounce();
        }
        if (currentCombo > (int)PERFECT)
        {
            goComboImage[1].SetActive(false);
            goComboImage[2].SetActive(false);
            goComboImage[3].SetActive(true);
            noteEffectManager.NoteBounce();
        }
    }

    //Miss
    public void ResetCombo()
    {
        try
        {
            MISS = ComboCount.MISS;

            currentCombo = (int)MISS;
            for (int i = 0; i < goComboImage.Length; i++)
            {
                goComboImage[i].SetActive(false);
            }
            goComboImage[0].SetActive(true);

            noteEffectManager.NoteBounce();
        }
        catch
        {
            Debug.Log("NoteComboManager.ResetCombo Error");
        }
   
    }
}
