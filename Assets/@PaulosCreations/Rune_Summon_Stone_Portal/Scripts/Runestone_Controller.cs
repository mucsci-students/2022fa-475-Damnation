using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runestone_Controller : MonoBehaviour
{
    //assigned in Inspector
    [SerializeField] private Gradient emissionColor;
    [SerializeField] private Renderer runeStoneRenderer;
    [SerializeField] private Transform rocksBaseTF;
    [SerializeField] private ParticleSystem coreParticles, sparksParticles;
    [SerializeField] private Light portalLight;
    [SerializeField] private AudioSource implodeAudio, forceFieldAudio, cracklingAudio;
    [SerializeField] private Transform[] runes = new Transform[2];
    [SerializeField] private AnimationCurve rockAnimCurve;

    private float maxVolForcefield = 1, maxVolCrackling = 1, maxIntPortalLight = 2;
    private float transitionSpeed = 0.5f;

    //assigned when Awake
    private Transform myTF, coreParticlesTF;
    private Transform[] rocks = new Transform[4];
    private SpriteRenderer[] runeRenderers = new SpriteRenderer[2];
    private bool inTransition, activated, animating;
    private Material rocksMat;
    private Color runesWantedeColor;
    private float fadeFloat;

    private Coroutine transitionCor, animateCor;

    private void Awake()
    {
        Setup();      
    }

    //Call this function to activate or deactivate the effects
    public void ToggleRuneStone(bool _activate)
    {
        if (inTransition)
            return;

        if (_activate && !activated)//toggle on
        {
            implodeAudio.Play();

            activated = true;

            transitionCor = StartCoroutine(TransitionSequence());

            coreParticles.Play();
            sparksParticles.Play();

            forceFieldAudio.Play();
            cracklingAudio.Play();
        }
        else if (!_activate && activated)//toggle off
        {
            implodeAudio.Play();

            activated = false;            

            if (animating)
            {
                StopCoroutine(animateCor);
                animating = false;
            }

            transitionCor = StartCoroutine(TransitionSequence());
        }
    }

    private IEnumerator TransitionSequence()
    {
        inTransition = true;

        float rocksCurrentHeight = rocksBaseTF.localPosition.y;
        Vector3 rocksWantedPosition = rocksBaseTF.localPosition;

        while (inTransition)
        {
            if (activated)//transition to on
            {
                fadeFloat = Mathf.MoveTowards(fadeFloat, 1f, Time.deltaTime * transitionSpeed);

                rocksWantedPosition.y = fadeFloat * 1.5f;

                if (fadeFloat >= 1f)//transition finished
                {
                    inTransition = false;

                    animating = true;
                    animateCor = StartCoroutine(AnimateActiveEffects());//start active "animation"
                }
            }
            else //transition to off
            {
                fadeFloat = Mathf.MoveTowards(fadeFloat, 0f, Time.deltaTime * transitionSpeed);

                rocksWantedPosition.y = fadeFloat * rocksCurrentHeight;

                if (fadeFloat <= 0f)//transition finished
                {
                    inTransition = false;

                    coreParticles.Stop();
                    sparksParticles.Stop();

                    forceFieldAudio.Stop();
                    cracklingAudio.Stop();
                }
            }

            //fade in/out
            forceFieldAudio.volume = maxVolForcefield * fadeFloat;
            cracklingAudio.volume = maxVolCrackling * fadeFloat;

            if (fadeFloat <= 0.7f)
            {
                runesWantedeColor.a = fadeFloat;
                runeRenderers[0].color = runesWantedeColor;
                runeRenderers[1].color = runesWantedeColor;
            }

            runes[0].Rotate(myTF.forward, Time.deltaTime * -120f, Space.Self);
            runes[1].Rotate(myTF.forward, Time.deltaTime * 40f, Space.Self);

            rocksMat.SetColor("_EmissionColor", emissionColor.Evaluate(fadeFloat));

            rocksBaseTF.localPosition = rocksWantedPosition;
            rocksBaseTF.Rotate(myTF.up, Time.deltaTime * (fadeFloat * -120f));

            rocks[0].Rotate(myTF.forward, Time.deltaTime * (fadeFloat *  220f), Space.Self);
            rocks[1].Rotate(myTF.forward, Time.deltaTime * (fadeFloat * -280f), Space.Self);
            rocks[2].Rotate(myTF.forward, Time.deltaTime * (fadeFloat *  340f), Space.Self);
            rocks[3].Rotate(myTF.forward, Time.deltaTime * (fadeFloat * -300f), Space.Self);

            portalLight.intensity = maxIntPortalLight * fadeFloat;

            coreParticlesTF.localScale = new Vector3(fadeFloat, fadeFloat, fadeFloat);

            yield return null;
        }
    }

    private IEnumerator AnimateActiveEffects()
    {
        Vector3 rocksWantedPosition = rocksBaseTF.localPosition;
        float randIntencity = maxIntPortalLight;
        float evalFloat = 0;

        while (animating)
        {
            //animate rocks
            evalFloat = Mathf.MoveTowards(evalFloat, 5f, Time.deltaTime * transitionSpeed);

            if (evalFloat == 5)//length of the curve is 5
                evalFloat = 0;

            rocksWantedPosition.y = rockAnimCurve.Evaluate(evalFloat) + 1.5f;
            rocksBaseTF.localPosition = rocksWantedPosition;

            rocksBaseTF.Rotate(myTF.up, Time.deltaTime * -120f, Space.Self);

            rocks[0].Rotate(myTF.forward, Time.deltaTime *  220f, Space.Self);
            rocks[1].Rotate(myTF.forward, Time.deltaTime * -280f, Space.Self);
            rocks[2].Rotate(myTF.forward, Time.deltaTime *  340f, Space.Self);
            rocks[3].Rotate(myTF.forward, Time.deltaTime * -300f, Space.Self);


            runes[0].Rotate(myTF.forward, Time.deltaTime * -120f, Space.Self);
            runes[1].Rotate(myTF.forward, Time.deltaTime *   40f, Space.Self);

            //flicker the light
            if (portalLight.intensity == randIntencity)
                randIntencity = Random.Range(-0.5f, 0.5f) + maxIntPortalLight;
            portalLight.intensity = Mathf.MoveTowards(portalLight.intensity, randIntencity, Time.deltaTime * 1.5f);

            yield return null;
        }
    }

    private void Setup()
    {
        //Getting al references
        myTF = transform;
        coreParticlesTF = coreParticles.transform;

        rocksMat = runeStoneRenderer.material;

        for (int i = 0; i < rocksBaseTF.childCount; i++)
        {
            rocks[i] = rocksBaseTF.GetChild(i);
            rocksBaseTF.GetChild(i).GetComponent<MeshRenderer>().material = rocksMat;
        }

        for (int i = 0; i < runes.Length; i++)
        {
            runeRenderers[i] = runes[i].GetComponent<SpriteRenderer>();
        }

        //Deactivate effects on Start
        rocksMat.SetColor("_EmissionColor", emissionColor.Evaluate(0));

        runesWantedeColor = runeRenderers[0].color;
        runesWantedeColor.a = 0f;
        runeRenderers[0].color = runesWantedeColor;
        runeRenderers[1].color = runesWantedeColor;

        forceFieldAudio.volume = 0f;
        cracklingAudio.volume = 0f;
        portalLight.intensity = 0f;
    }
}
