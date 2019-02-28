using System.Collections.Generic;
using UnityEngine;

public class ShakeTransform : MonoBehaviour {

	public class ShakeEvent {
        float duration;
        float timeremaining;

        ShakeTransformEventData data;

        public ShakeTransformEventData.Target Target 
        {
            get 
            {
                return data.target;
            }
        }

        Vector3 noiseOffset;
        public Vector3 noise;

        public ShakeEvent(ShakeTransformEventData data)
        {
            this.data = data;

            duration = data.duration;
            timeremaining = duration;

            float randConstant = 32f;
            noiseOffset.x = Random.Range(0.0f, randConstant);
            noiseOffset.y = Random.Range(0.0f, randConstant);
            noiseOffset.z = Random.Range(0.0f, randConstant);
        }

        public void Update()
        {
            float deltatime = Time.deltaTime;
            timeremaining -= deltatime;
            float noiseOffsetDelta = deltatime * data.frequency;

            noiseOffset += Vector3.one * noiseOffsetDelta;

            noise.x = Mathf.PerlinNoise(noiseOffset.x, 0.0f);
            noise.y = Mathf.PerlinNoise(noiseOffset.y, 1.0f);
            noise.z = Mathf.PerlinNoise(noiseOffset.z, 2.0f);

            noise -= Vector3.one * 0.5f;
            noise *= data.amplitude;

            float agePercent = (1.0f - timeremaining) / duration;
            noise *= data.blendOverLifetime.Evaluate(agePercent);
        }

        public bool isAlive()
        {
            return timeremaining > 0.0f;
        }
	}

    List<ShakeEvent> shakeEvents = new List<ShakeEvent>();

    public void AddShakeEvent(ShakeTransformEventData data)
    {
        ShakeEvent newData = new ShakeEvent(data);
        shakeEvents.Add(newData);
    }

    public void AddShakeEvent(float amplitude, float frequency, float duration, AnimationCurve blendOverLifetime, ShakeTransformEventData.Target target)
    {
        ShakeTransformEventData data = ScriptableObject.CreateInstance<ShakeTransformEventData>();
        data.init(amplitude, frequency, duration, blendOverLifetime, target);

        AddShakeEvent(data);
    }

    private void LateUpdate()
    {
        Vector3 positionOffset = Vector3.zero;
        Vector3 rotationOffset = Vector3.zero;
        for (int i = shakeEvents.Count - 1; i >= 0; i--)
        {
            ShakeEvent se = shakeEvents[i];
            se.Update();
            if (se.Target == ShakeTransformEventData.Target.Position)
            {
                positionOffset += se.noise;
            }
            else if (se.Target == ShakeTransformEventData.Target.Rotation)
            {
                rotationOffset += se.noise;
            }
            if (!se.isAlive())
            {
                shakeEvents.RemoveAt(i);
            }
            else
            {
                transform.localPosition = positionOffset;
                transform.localEulerAngles = rotationOffset;
            }
        }
        if (shakeEvents.Count == 0)
        {
            transform.localPosition = positionOffset;
            transform.localEulerAngles = positionOffset;
        }

    }
}
