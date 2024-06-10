using System.Collections;
using UnityEngine;

public class RandomAnimationTrigger : MonoBehaviour
{
    public Animator animator; // Animator postaci
    public string animationTriggerName = "StartAnimation"; // Nazwa triggera w Animatorze
    public float minTime = 1.0f; // Minimalny czas w sekundach
    public float maxTime = 5.0f; // Maksymalny czas w sekundach

    [SerializeField] private GameObject body; // Referencja do obiektu cia³a
    private Material bodyMaterial;
    private Collider colider;

    private void Start()
    {
        bodyMaterial = body.GetComponent<SkinnedMeshRenderer>().material;
        colider = GetComponent<Collider>();
        StartCoroutine(AnimationCycle());
    }

    private IEnumerator AnimationCycle()
    {
        while (true)
        {
            float randomTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(randomTime);

            // Uruchomienie animacji
            animator.SetTrigger(animationTriggerName);

            // Czekaj na zakoñczenie animacji
            yield return new WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName(animationTriggerName));
        }
    }

    public void OnAnimationStartTrigger()
    {
        StartCoroutine(ChangeTransparency(1.0f)); // Pojawienie siê postaci
    }

    public void OnAnimationEndTrigger()
    {
        StartCoroutine(ChangeTransparency(0.0f)); // Znikniêcie postaci
    }

    public IEnumerator ChangeTransparency(float target)
    {
        if (target == 1)
        {
            colider.enabled = true;
        }
        else
        {
            colider.enabled = false;
        }

        float fadeTime = 2f;
        float time = 0f;
        float startDither = bodyMaterial.GetFloat("_DitherThreshold");

        while (time < fadeTime)
        {
            time += Time.deltaTime;
            float dither = Mathf.SmoothStep(startDither, target, time / fadeTime);
            bodyMaterial.SetFloat("_DitherThreshold", dither);
            yield return null;
        }

        bodyMaterial.SetFloat("_DitherThreshold", target);
    }
}
