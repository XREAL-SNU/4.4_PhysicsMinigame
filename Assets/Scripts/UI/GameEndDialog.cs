using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameEndDialog : MonoBehaviour {
    private bool inited = false;

    public TextMeshProUGUI reachedt, altitudet, highreachedt, shotst, scoret, highscoret;
    private Action endAnimation = () => { };

    void Update() {
        if (!inited) return;
        if (Input.GetMouseButtonDown(0)) {
            StopAllCoroutines();
            endAnimation();
        }
    }

    public void Show(int reached, int altitude, int shots, int score) {
        inited = true;
        //todo
        StartCoroutine(Tally(reachedt, "m", reached, 0, 0.5f));
        highreachedt.transform.parent.position += Vector3.right * ("" + reached).Length * 40f;
        StartCoroutine(Slam(highreachedt, "m", ScoreModule.highReached, 0.5f, 0.2f));
        StartCoroutine(Tally(altitudet, "m", altitude, 0.9f, 0.7f));

        endAnimation = () => {
            End(reachedt, "m", reached);
            End(highreachedt, "m", ScoreModule.highReached);
            End(altitudet, "m", altitude);
        };
    }

    private void End(TextMeshProUGUI text, string format, int score) {
        text.text = score + format;
        text.transform.parent.localScale = Vector3.one;

        CanvasGroup cg = null;
        if (text.TryGetComponent(out CanvasGroup cg2)) cg = cg2;
        else if(text.transform.parent.TryGetComponent(out CanvasGroup cg1)) cg = cg1;
        if (cg != null) {
            cg.alpha = 1f;
            cg.transform.localScale = Vector3.one;
        }
    }

    private IEnumerator Tally(TextMeshProUGUI text, string format, int score, float wait, float duration) {
        text.text = "0" + format;
        yield return new WaitForSeconds(wait);
        float t = 0;
        while (t < duration) {
            t += Time.deltaTime;
            text.text = (int)((t / duration) * score) + format;
            yield return null;
        }
        text.text = score + format;
    }

    private IEnumerator Slam(TextMeshProUGUI text, string format, int score, float wait, float duration) {
        text.text = score + format;
        CanvasGroup cg = null;
        if (text.TryGetComponent(out CanvasGroup cg2)) cg = cg2;
        if(cg == null && text.transform.parent != null) cg = text.transform.parent.GetComponent<CanvasGroup>();
        if(cg == null) {
            yield break;//nope
        }
        cg.alpha = 0f;
        cg.transform.localScale = Vector3.one * 2f;
        yield return new WaitForSeconds(wait);
        float t = 0;
        while (t < duration) {
            t += Time.deltaTime;
            cg.alpha = (t / duration);
            cg.transform.localScale = Vector3.one * (2.0f - cg.alpha);
            yield return null;
        }
        cg.transform.localScale = Vector3.one;
        cg.alpha = 1f;
    }
}
