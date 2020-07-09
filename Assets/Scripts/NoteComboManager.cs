using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComboCount
{
    Miss = 0,
    Cool = 1,
    Great = 5,
    Perfect = 9,
}

public class NoteComboManager : MonoBehaviour
{
    [SerializeField] GameObject[] goComboImage = null;

    private ComboCount Miss, Cool, Great, Perfect;

    NoteEffectManager noteEffectManager = null;

    int currentCombo = 0;


    void Start()
    {
        noteEffectManager = FindObjectOfType<NoteEffectManager>();

        for (int i = 0; i < goComboImage.Length; i++)
        {
            goComboImage[i].SetActive(false);
        }
    }

    public void IncreaseCombo(int p_num = 1)
    {
        currentCombo += p_num;

        goComboImage[0].SetActive(false);

        Cool = ComboCount.Cool;
        Great = ComboCount.Great;
        Perfect = ComboCount.Perfect;

        if (currentCombo > (int)Cool && currentCombo <= (int)Great)
        {
            goComboImage[1].SetActive(true);
            noteEffectManager.NoteBounce();
        }
        if (currentCombo > (int)Great && currentCombo <= (int)Perfect)
        {
            goComboImage[1].SetActive(false);
            goComboImage[2].SetActive(true);
            noteEffectManager.NoteBounce();
        }
        if (currentCombo > (int)Perfect)
        {
            goComboImage[1].SetActive(false);
            goComboImage[2].SetActive(false);
            goComboImage[3].SetActive(true);
            noteEffectManager.NoteBounce();
        }
    }

    public void ResetCombo()
    {
        Miss = ComboCount.Miss;

        currentCombo = (int)Miss;
        for (int i = 0; i < goComboImage.Length; i++)
        {
            goComboImage[i].SetActive(false);
        }
        goComboImage[0].SetActive(true);

        noteEffectManager.NoteBounce();
    }
}
