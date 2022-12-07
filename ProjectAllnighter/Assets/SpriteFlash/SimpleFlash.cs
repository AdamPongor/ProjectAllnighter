using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

    public class SimpleFlash : MonoBehaviour
    {


        [Tooltip("Material to switch to during the flash.")]
        [SerializeField] private Material flashMaterial;

        [Tooltip("Duration of the flash.")]
        [SerializeField] private float duration;

        public UnityEvent resetDamageAble;

        // The SpriteRenderer that should flash.
        private SpriteRenderer spriteRenderer;

        // The material that was in use, when the script started.
        private Color originalColor;

        // The currently running coroutine.
        private Coroutine flashRoutine;



        void Start()
        {
            
            // Get the SpriteRenderer to be used,
            // alternatively you could set it from the inspector.
            spriteRenderer = GetComponent<SpriteRenderer>();

            // Get the material that the SpriteRenderer uses, 
            // so we can switch back to it after the flash ended.
            originalColor = spriteRenderer.material.color;
        }


        public void Flash(Color color)
        {
        flashMaterial.color = color;

        // If the flashRoutine is not null, then it is currently running.
        if (flashRoutine != null)
            {
                // In this case, we should stop it first.
                // Multiple FlashRoutines the same time would cause bugs.
                StopCoroutine(flashRoutine);
            }

            // Start the Coroutine, and store the reference for it.
            flashRoutine = StartCoroutine(FlashRoutine(color));
        }

        public void StopFlash()
        {
        if (flashRoutine != null)
            {
                StopCoroutine(flashRoutine);
            }
        spriteRenderer.material.color = originalColor;
        resetDamageAble?.Invoke();
    }

        private IEnumerator FlashRoutine(Color color)
        {
            // Swap to the flashMaterial.
            spriteRenderer.material.color = color;

            // Pause the execution of this function for "duration" seconds.
            yield return new WaitForSeconds(duration);

            // After the pause, swap back to the original material.
            spriteRenderer.material.color = originalColor;

            resetDamageAble?.Invoke();
            // Set the routine to null, signaling that it's finished.
            flashRoutine = null;
        }

    }
