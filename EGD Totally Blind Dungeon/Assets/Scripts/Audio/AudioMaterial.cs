using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaterial : MonoBehaviour
{
    private List<AudioMat> matList = new List<AudioMat>()
    {
        {AudioMat.Wood },
        {AudioMat.Metal },
        {AudioMat.Stone },
        {AudioMat.Grass }
    };
    public enum AudioMat // your custom enumeration
    {
        Wood,
        Metal,
        Stone,
        Grass
    };
    public AudioMat mat = AudioMat.Wood;

    [Range(0, 1)]
    public float dampening;

    public int compareMats(AudioMat other)
    {
        int x = 0;
        int y = 0;
        for(int i = 0; i < matList.Count; i++)
        {
            if(mat == matList[i])
            {
                x = i;
            }
            if(other == matList[i])
            {
                y = i;
            }
        }
        int ret = (x * matList.Count ) + y;
        return ret;
    }
    
    public int matToIndex()
    {
        for(int i = 0; i < matList.Count; i++)
        {
            if(mat == matList[i])
            {
                return i;
            }
        }
        return -1;
    }
}
