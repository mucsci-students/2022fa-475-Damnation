using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_Controller : MonoBehaviour
{
    //assigned in Inspector
    [SerializeField] private Gradient emissionColor;
    [SerializeField] private Color portalEffectColor;
    [SerializeField] private GameObject portalEffectObj;
    [SerializeField] private Renderer portalRenderer, portalFloorRenderer;

    [SerializeField] private ParticleSystem orbParticles;
    [SerializeField] private Light portalLight;
    [SerializeField] private AudioSource orbAudio, flashAudio, portalAudio;

    private float maxVolOrb = 0.08f, maxVolportal = 0.8f, maxIntPortalLight = 4;
    private float transitionSpeed = 0.3f;

    //assigned when Awake
    private bool inTransition, activated;
    private Material portalMat, portalEffectMat;
    private float fadeFloat;

    private Coroutine transitionCor;

    private bool textAcitve;

    private void Awake()
    {
        Setup();
    }

    //For testing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            textAcitve = !textAcitve;
            TogglePortal(textAcitve);
        }
    }

    //Call this function to activate or deactivate the effects
    public void TogglePortal(bool _activate)
    {
        if (inTransition)
            return;
    
        if (_activate && !activated)//toggle on
        {
            activated = true;

            transitionCor = StartCoroutine(PreActivate());
        }
        else if (!_activate && activated)//toggle off
        {
            activated = false;
    
            transitionCor = StartCoroutine(TransitionSequence());
        }
    }

    private IEnumerator PreActivate()
    {
        inTransition = true;

        orbAudio.volume = maxVolOrb;
        orbAudio.Play();
        orbParticles.Play();

        yield return new WaitForSeconds(2.2f);

        flashAudio.Play();
        portalAudio.Play();

        yield return new WaitForSeconds(0.1f);

        portalEffectObj.SetActive(true);

        transitionCor = StartCoroutine(TransitionSequence());
    }

    private IEnumerator TransitionSequence()
    {
        inTransition = true;

        while (inTransition)
        {
            if (activated)//transition to on
            {
                fadeFloat = Mathf.MoveTowards(fadeFloat, 1f, Time.deltaTime * transitionSpeed);

                orbAudio.volume -= Time.deltaTime * 0.1f;

                if (fadeFloat >= 1f)//transition finished
                {
                    inTransition = false;
                    orbAudio.Stop();
                }
            }
            else //transition to off
            {
                fadeFloat = Mathf.MoveTowards(fadeFloat, 0f, Time.deltaTime * transitionSpeed);

                if (fadeFloat <= 0f)//transition finished
                {
                    inTransition = false;
    
                    portalAudio.Stop();
                    portalEffectObj.SetActive(false);
                }
            }
    
            //fade in/out
            portalAudio.volume = maxVolportal * fadeFloat;

            portalMat.SetColor("_EmissionColor", emissionColor.Evaluate(fadeFloat));
            portalEffectMat.SetFloat("_PortalFade", fadeFloat);
    
            portalLight.intensity = maxIntPortalLight * fadeFloat; 
    
            yield return null;
        }
    }

    private void Setup()
    {
        //Getting/creating material instance
        portalMat = portalRenderer.material;
        
        if (portalFloorRenderer != null)
            portalFloorRenderer.material = portalMat;

        portalEffectMat = portalEffectObj.transform.GetComponent<Renderer>().material;

        //Deactivate effects on Start
        portalMat.SetColor("_EmissionColor", emissionColor.Evaluate(0));
        portalEffectObj.SetActive(false);
        portalEffectMat.SetFloat("_PortalFade", 0f);
        portalEffectMat.SetColor("_ColorMain", portalEffectColor);
        portalAudio.volume = 0f;
        portalLight.intensity = 0f;
    }
}
