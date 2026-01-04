using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class MathParticles : MonoBehaviour
{
    ParticleSystem ps;
    ParticleSystem.Particle[] particles;

    [Header("Instellingen")]
    public float straal = 2.2f;        
    public float draaiSnelheid = 1f;   
    public float stijgSnelheid = 0.2f; 
    public float yOffset = -0.1f;      

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void LateUpdate()
    {
        InitializeIfNeeded();

        // Haal alle actieve deeltjes op
        int numParticlesAlive = ps.GetParticles(particles);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            // We gebruiken de 'startLifetime' en 'remainingLifetime' om te weten hoe oud een deeltje is
            float leeftijd = particles[i].startLifetime - particles[i].remainingLifetime;

            // Wiskundige formule voor een spiraal (Helix)
            // De hoek verandert naarmate het deeltje ouder wordt
            float hoek = leeftijd * draaiSnelheid;

            float x = Mathf.Cos(hoek) * straal;
            float z = Mathf.Sin(hoek) * straal;
            float y = (leeftijd * stijgSnelheid) + yOffset;

            // Zet de nieuwe positie
            particles[i].position = new Vector3(x, y, z);
        }

        // Pas de wijzigingen toe op het systeem
        ps.SetParticles(particles, numParticlesAlive);
    }

    void InitializeIfNeeded()
    {
        if (particles == null || particles.Length < ps.main.maxParticles)
        {
            particles = new ParticleSystem.Particle[ps.main.maxParticles];
        }
    }
}