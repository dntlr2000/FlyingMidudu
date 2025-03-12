using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStage : Stage
{
    public bool goNext = false;
    public GameObject greenBox;
    public GameObject bossPointer;

    protected override void Start()
    {
        StartCoroutine(StagePhase1());

        text = "미두두 어드벤처 튜토리얼";
        Text.gameObject.SetActive(true);
        StartCoroutine(Text.StageTextAnimation(text));

        BGM_Script = FindObjectOfType<BGMController>();
        BGM_Script.PlayBGM();
    }

    protected override IEnumerator StagePhase1()
    {
        yield return new WaitForSeconds(7f);
        text = "안녕하세요.\n \"플라잉 미두두\"를 플레이 해주셔서 감사합니다.";
        TextAnimation(text);

        yield return new WaitForSeconds(7f);
        text = "이 게임은 3D 탄막 슈팅 게임으로, \n 수평, 수직 이동을 모두 활용하여 탄막을 \n 피하면 되는 게임입니다.";
        TextAnimation(text);

        yield return new WaitForSeconds(7f);
        text = "ESC를 눌러 일시정지를 하실 수 있으며,\n 설정 창에서 감도, 볼륨을 조정하실 수 있습니다.";
        TextAnimation(text);

        yield return new WaitForSeconds(7f);
        text = "우선, WASD를 눌러 연두색 공간까지 이동해주세요. \n Tab 키를 누르면 멀리서 바라볼 수 있습니다.";
        TextAnimation(text);

        Vector3 GreenBoxPosition = new Vector3(30, 0, 0);
        Instantiate(greenBox, GreenBoxPosition, Quaternion.identity);

        yield return new WaitForSeconds(7f);
        int loopCount = 0;
        while (goNext == false)
        {
            yield return new WaitForSeconds(2f);
            loopCount++;

            if (loopCount == 8)
            {
                text = "어서 들어가주세요.";
                TextAnimation(text);
                yield return new WaitForSeconds(7f);
            }

            if (loopCount == 16)
            {
                text = "화면 아래 방위표 기준 서쪽에 있습니다";
                TextAnimation(text);
                yield return new WaitForSeconds(7f);
            }

            if (loopCount == 24)
            {
                text = "이 이후로는 기다려도 안 나와요. 그냥 들어가요.";
                TextAnimation(text);
                yield return new WaitForSeconds(7f);
            }
        }

        StartCoroutine(StagePhase2());
    }

    protected override IEnumerator StagePhase2()
    {
        goNext = false;
        text = "잘 하셨습니다! \n 참고로 Shift키를 사용하면 한층 더 빠르게 이동이 가능합니다.";
        TextAnimation(text);

        yield return new WaitForSeconds(7f);
        text = "Space 바를 누르시면 상승, \n Ctrl 키를 누르시면 하강이 가능합니다.";
        TextAnimation(text);

        yield return new WaitForSeconds(7f);
        text = "다시, 연두색 공간까지 이동해주세요.";
        TextAnimation(text);

        Vector3 GreenBoxPosition = new Vector3(-30, -30, -30);
        Instantiate(greenBox, GreenBoxPosition, Quaternion.identity);

        yield return new WaitForSeconds(7f);

        int loopCount = 0;
        while (goNext == false)
        {
            yield return new WaitForSeconds(2f);
            loopCount++;
            

            if (loopCount == 8)
            {
                text = "어서 들어가주세요.";
                TextAnimation(text);
                yield return new WaitForSeconds(7f);
            }

            if (loopCount == 16)
            {
                text = "화면 아래 방위표.\n 동쪽, 아래.";
                TextAnimation(text);
                yield return new WaitForSeconds(7f);
            }

            if (loopCount == 24)
            {
                text = "초선.";
                TextAnimation(text);
                yield return new WaitForSeconds(7f);
            }
            
            if (loopCount == 30)
            {
                text = "웁똥딸.";
                TextAnimation(text);
                yield return new WaitForSeconds(7f);
            }
            if (loopCount == 33)
            {
                text = "그.. 사실은 웁순이가 좋으면서 싫어하는 척 하지 마요...";
                TextAnimation(text);
                yield return new WaitForSeconds(7f);
            }

            if (loopCount == 36)
            {
                //text = "장뇌삼.";
                text = "이제 없어요.";
                TextAnimation(text);
                yield return new WaitForSeconds(7f);
            }

        }
        StartCoroutine(StagePhase3());
    }

    private IEnumerator StagePhase3()
    {
        text = "잘 하셨습니다! \n Shift키를 누른 상태에서는 상승과 하강이 빨라지니 \n 알아두시면 좋습니다.";
        TextAnimation(text);

        yield return new WaitForSeconds(7f);
        text = "다음은 F를 눌러 슈터를 활성화하거나 \n비활성화가 가능합니다.";
        TextAnimation(text);

        yield return new WaitForSeconds(7f);
        text = "웁님을 향해 공격하는 방개를 향해 슈터로\n공격해보도록 합시다.";
        TextAnimation(text);

        SpawnEnemy(Enemy[0], 0, 0, -50);

        yield return new WaitForSeconds(7f);
        while (CheckEnemyExist())
        {
            yield return new WaitForSeconds(2f);
        }
        StartCoroutine(StagePhase4());
    }

    private IEnumerator StagePhase4()
    {
        text = "잘 하셨습니다! 역시 웁님입니다.";
        TextAnimation(text);

        yield return new WaitForSeconds(7f);
        text = "다음은 Q를 눌러 궁극기를 사용할 수 있습니다.\n 궁극기는 좌측 하단 Bomb이 1개 이상일 때\n사용 가능하며 사용 시 일정시간 무적이 됩니다.";
        TextAnimation(text);

        yield return new WaitForSeconds(7f);
        text = "마지막으로 다시 한 번 방개들을 공격해주세요.";
        TextAnimation(text);

        SpawnEnemy(Enemy[0], 0, 30, -50);
        SpawnEnemy(Enemy[0], -30, 0, -50);
        SpawnEnemy(Enemy[0], 30, 0, -50);

        yield return new WaitForSeconds(7f);
        while (CheckEnemyExist())
        {
            yield return new WaitForSeconds(2f);
        }
        StartCoroutine(StagePhase5());
    }

    private IEnumerator StagePhase5()
    {
        text = "잘 하셨습니다.";
        TextAnimation(text);

        yield return new WaitForSeconds(7f);
        text = "화면 하단에 표시되는 방위표가 적의 위치를 \n알려주기 때문에 필요할 때 보시면 \n도움이 될 것입니다.";
        TextAnimation(text);

        yield return new WaitForSeconds(7f);
        text = "보스급 개체의 경우에는 방위표에 표시되진 않으며,\n 대신 웁님 주변에 포인터가 활성화되어 \n보스를 가리킵니다.";
        if (bossPointer != null) bossPointer.SetActive(true);
        TextAnimation(text);

        yield return new WaitForSeconds(7f);
        text = "이상으로 튜토리얼을 마치겠습니다. \n 감사합니다.";
        TextAnimation(text);

        yield return new WaitForSeconds(7f);
        loader.LoadSceneAsync("MainMenu");
    }


    private void TextAnimation(string text)
    {

        Text.gameObject.SetActive(true);
        StartCoroutine(Text.StageTextAnimation(text));
    }
}
