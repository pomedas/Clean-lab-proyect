using System;
using UnityEngine;


/// <summary>
/// list of the particle types
/// </summary>
public enum ParticleType
{
    All, Fire, Water, Earth, Air, Light, Darkness, Energy, Metal
}

public static class ParticleTypeMethods {

    static Color red = new Color(1, 0.219f, 0.219f);//0.86275f, 0.07843f, 0.23529f);//Color.red;
    static Color pink = new Color(0.909f, 0.263f, 0.576f);//0.39f, 0.58f, 0.93f);//(0.25490f,  0.41176f,  0.88235f);//Color.blue;
    static Color green = new Color(0.196f, 1, 0.494f);//Color.green;
    static Color orange = new Color(1, 0.624f, 0.108f);//139/255f, 69/255f, 19/255f);
    static Color yellow = new Color(1, 0.949f, 0);//Color.yellow;
    static Color violet = new Color(0.443f, 0.345f, 0.886f);//0.6f,  0.19608f,  0.8f);//(75/255f, 0, 130/255f);
    static Color lightblue = new Color(0.494f, 1, 0.961f);//0, 0.80784f, 0.81961f);//Color.cyan;
    static Color white = new Color(0.90196f, 0.83922f, 0.56471f);// Color.white

    /// <summary>
    /// return the color associated to the particle
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static Color GetColor(this ParticleType t) {
        switch (t)
        {
            case ParticleType.Fire:
                return red;
            case ParticleType.Water:
                return lightblue;
            case ParticleType.Earth:
                return orange;
            case ParticleType.Air:
                return green;
            case ParticleType.Light:
                return yellow;
            case ParticleType.Darkness:
                return violet; 
            case ParticleType.Energy:
                return white;
            case ParticleType.Metal:
                return pink;
            default: 
                return Color.clear;
        }
    }
    /// <summary>
    /// return the addon mass to be used to generate the star
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static float GetMass(this ParticleType t)
    {
        switch (t)
        {
            case ParticleType.Fire:
                return 0.4f;
            case ParticleType.Water:
                return 0.25f;
            case ParticleType.Air:
                return 0.2f;
            case ParticleType.Earth:
                return 0.5f;
            case ParticleType.Light:
                return 0.3f;
            case ParticleType.Darkness:
                return 0.3f;
            case ParticleType.Energy:
                return 0.2f;
            case ParticleType.Metal:
                return 0.5f;
            default:
                return 0.1f;
        }
    }
    /// <summary>
    /// return the orbit addon value to generate the star
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static float GetOrbitValue(this ParticleType t)
    {
        switch (t)
        {
            case ParticleType.Fire:
                return 0.5f;
            case ParticleType.Water:
                return 1.25f;
            case ParticleType.Air:
                return 1.5f;
            case ParticleType.Earth:
                return 1f;
            case ParticleType.Light:
                return 2f;
            case ParticleType.Darkness:
                return 1.5f;
            case ParticleType.Energy:
                return 0.75f;
            case ParticleType.Metal:
                return 2f;
            default:
                return 0.5f;
        }
    }
    /// <summary>
    /// return the effect the particle produce
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static EffectType GetEffect(this ParticleType t)
    {
        switch (t)
        {
            case ParticleType.Fire:
                return EffectType.explosion;
            case ParticleType.Water:
                return EffectType.boom;
            case ParticleType.Earth:
                return EffectType.lightning;
            case ParticleType.Air:
                return EffectType.thunder;
            case ParticleType.Light:
                return EffectType.LightOrb;
            case ParticleType.Darkness:
                return EffectType.Bell;
            case ParticleType.Energy:
                return EffectType.PlasmaBomb;
            case ParticleType.Metal:
                return EffectType.Magic;
            default:
                return EffectType.explosion;
        }
    }
    /// <summary>
    /// return the marterial of the planet according to the particle used to geenrate it
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static Material GetMaterial(this ParticleType t)
    {
        string path = "Materials/";
        switch (t)
        {
            case ParticleType.Fire:
                path += "Planet_09";
                break;
            case ParticleType.Water:
                path += "Planet_05";
                break;
            case ParticleType.Air:
                path += "Planet_06";
                break;
            case ParticleType.Earth:
                path += "Planet_02";
                break;
            case ParticleType.Light:
                path += "Planet_03";
                break;
            case ParticleType.Darkness:
                path += "Planet_10";
                break;
            case ParticleType.Energy:
                path += "Planet_07";
                break;
            case ParticleType.Metal:
                path += "Planet_08";
                break;
            default:
                path += "";
                break;
        }
        return( Resources.Load<Material>(path));

    }

    /// <summary>
    /// Return the semisison color associated to the particles
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static Color GetEmissionColor(this ParticleType t)
    {
        switch (t)
        {
            case ParticleType.Fire:
                return new Color(0.86275f, 0.07843f, 0.23529f);
            case ParticleType.Water:
                return new Color(0.25490f, 0.41176f, 0.88235f);
            case ParticleType.Air:
                return Color.green;
            case ParticleType.Earth:
                return new Color(139 / 255f, 69 / 255f, 19 / 255f);
            case ParticleType.Light:
                return Color.yellow;
            case ParticleType.Darkness:
                return new Color(0.6f, 0.19608f, 0.8f);
            case ParticleType.Energy:
                return new Color(0, 0.80784f, 0.81961f);
            case ParticleType.Metal:
                return Color.white;
            default:
                return Color.clear;
        }
    }
}

/// <summary>
/// Lit of all objects categories that can interact with eachother
/// </summary>
public enum RoamingObjectType
{
    Meteoroid, Comet, Particle1, Particle1_2together, Planet, Asteroid, Star, Blackhole, Other, Reward
}
public static class RoamingObjectMethods {
    /// <summary>
    /// return the color of the sparks to be left over a creation process
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static Color GetColor(this RoamingObjectType t)
    {
        switch (t)
        {
            case RoamingObjectType.Comet: return new Color(0.49804f,  1f,  0.83137f);//(0,  0.57f, 1);
            case RoamingObjectType.Meteoroid: return new Color(0.14118f,  0.90588f,  0.06667f);//(0f, 0.5f, 0f); //return new Color(1, 0.57f, 0);(0.18f, 0.54f, 0.34f)
            default: return Color.clear;
        }
    }

    public static AudioClip GetCreationAudio(this RoamingObjectType t) {
        AudioClip audio;
        string path = "Sounds/";
        switch (t)
        {
            case RoamingObjectType.Asteroid: path += "warning"; break;
            case RoamingObjectType.Planet: path += "creation_sm"; break;
            case RoamingObjectType.Star: path += "creation_lg"; break;
            case RoamingObjectType.Blackhole: path += "blackhole_appearance"; break;
            case RoamingObjectType.Reward: path += "Reward1"; break;
            default: break;
        }
        audio = Resources.Load<AudioClip>(path);
        return audio;
    }

    public static AudioClip GetMovingAudio(this RoamingObjectType t)
    {
        AudioClip audio;
        string path = "Sounds/";
        switch (t)
        {
            case RoamingObjectType.Asteroid: path += "asteroid_transition3"; break;
            case RoamingObjectType.Reward: path += "reward_transition"; break;
            default: break;
        }
        audio = Resources.Load<AudioClip>(path);
        return audio;
    }

    public static AudioClip GetInteractingAudio(this RoamingObjectType t)
    {
        AudioClip audio;
        string path = "Sounds/";
        switch (t)
        {
            case RoamingObjectType.Particle1: path += "loading"; break;
            case RoamingObjectType.Particle1_2together: path += "loading_2players"; break;
            case RoamingObjectType.Planet: path += "OnOrbit"; break;
            case RoamingObjectType.Blackhole: path += "blackhole_1player"; break;
            case RoamingObjectType.Asteroid: path += "Asteroid Interaction"; break;
            default: break;
        }
        audio = Resources.Load<AudioClip>(path);
        return audio;
    }

    public static AudioClip GetTransitionAudio(this RoamingObjectType t)
    {
        AudioClip audio;
        string path = "Sounds/";
        switch (t)
        {
            case RoamingObjectType.Blackhole: path += "blackhole_2player"; break;
            case RoamingObjectType.Planet: path += "Teleport_Object"; break;
            default: break;
        }
        audio = Resources.Load<AudioClip>(path);
        return audio;
    }

    public static AudioClip GetDestructionAudio(this RoamingObjectType t)
    {
        AudioClip audio;
        string path = "Sounds/";
        switch (t)
        {
            case RoamingObjectType.Planet: path += "Explosion_3"; break;
            case RoamingObjectType.Comet: path += "Explosion_1"; break;
            case RoamingObjectType.Meteoroid: path += "Explosion_1"; break;
            case RoamingObjectType.Blackhole: path += "blackhole_transition"; break;
            case RoamingObjectType.Reward: path += "Reward2"; break;
            default: break;
        }
        audio = Resources.Load<AudioClip>(path);
        return audio;
    }

    public static Mesh GetMesh(this RoamingObjectType t) { 
        GameObject g;
        string path = "Meshes/";
        
        switch (t)
        {
            case RoamingObjectType.Comet: path += "Planet_2"; break;
            case RoamingObjectType.Meteoroid: path += "Planet_1"; break;
            default: break;
        }
        g = Resources.Load<GameObject>(path);
        return g.GetComponent<MeshFilter>().sharedMesh;
    }
}


/// <summary>
/// list of the possible visual effect (+ generate type, used to determine any generation of possible objects)
/// </summary>
public enum EffectType
{
    generate, explosion, boom, lightning, thunder, LightOrb, Bell, PlasmaBomb, Magic
}