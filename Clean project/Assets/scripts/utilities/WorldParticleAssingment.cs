using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Class to determine the particle emitters present in each world
/// </summary>
[Serializable]
public class WorldParticleAssingment
{
    /// <summary>
    /// matrix of particles by world (number may change between different worlds)
    /// </summary>
    private static ParticleType[][] world;

    /// <summary>
    /// cardinality of the subsets of particles
    /// </summary>
    private static int subsetSize = 4;

    /// <summary>
    /// create the table
    /// </summary>
    public WorldParticleAssingment() { 
        //currently used fixed particle setup, to be brouht into the randomness aproach
        /*world[0] = new ParticleType[]{ ParticleType.Fire, ParticleType.Water, ParticleType.Earth, ParticleType.Air };
        world[1] = new ParticleType[]{ ParticleType.Darkness, ParticleType.Light, ParticleType.Energy, ParticleType.Metal }; 
        world[2] = new ParticleType[]{ ParticleType.Fire, ParticleType.Water, ParticleType.Earth, ParticleType.Air };
        world[3] = new ParticleType[]{ ParticleType.Fire, ParticleType.Water, ParticleType.Earth, ParticleType.Air };
        world[4] = new ParticleType[]{ ParticleType.Fire, ParticleType.Water, ParticleType.Earth, ParticleType.Air };*/


        //this is for the generation of the worlds according to the particles. yet to be added to the real thing
        IEnumerable<IEnumerable<ParticleType>> L = Subsets();
        //debug purpose code to print the results
        /*foreach(var s in L){
            Debug.Log(string.Join(" ", s.Select(x=> x.ToString())));

        }
        Debug.Log(L.Count());*/

        world = new ParticleType[L.Count()][];
        L = Shuffle(L);
        int i = 0;
        foreach (var s in L)
        {
            world[i] = s.ToArray();
            i++;
        }
    }

    private IEnumerable<IEnumerable<ParticleType>> Shuffle(IEnumerable<IEnumerable<ParticleType>> list)
    {
        IEnumerable<IEnumerable<ParticleType>> newlist = list;

        newlist = newlist.OrderBy(a => UnityEngine.Random.Range(0,list.Count()));

        return newlist;
    } 

    /// <summary>
    /// obtaine the particles types of the avaialble emitters in the world
    /// </summary>
    /// <param name="index">index of the current world</param>
    /// <returns>array of particle types avaialable in the world</returns>
    public static ParticleType[] gettheworldparticles(int index) {
        return world[index];
    }

    /// <summary>
    /// get te number of available world
    /// </summary>
    /// <returns></returns>
    public static int getNumberOfWorld()
    {
        return world.Length;
    }

    /// <summary>
    /// get the whole set of subsets of particle types characterizing each one world
    /// </summary>
    /// <returns></returns>
    private static  IEnumerable<IEnumerable<ParticleType>> Subsets(){
        //create the set of alternatives exluding the ParticleType.All, which is just for leaning
        List<ParticleType> sequence = new List<ParticleType>();
        for(int i = 1; i < Enum.GetValues(typeof(ParticleType)).Length; i++){
            sequence.Add((ParticleType)i);
        }

        //create the set of only one element
        var oneElementSequence = sequence.Select(x => new[] { x }).ToList();

        //add the initial empty set
        var result = new List<List<ParticleType>>();
        result.Add(new List<ParticleType>());

        //geenrate the powerset, skipping sequence which are too long
        foreach (var oneElemSequence in oneElementSequence) {
            int lenght = result.Count();

            for (int i = 0; i < lenght; i++) {
                if (result[i].Count >= subsetSize) {
                    continue;
                }
                result.Add(result[i].Concat(oneElemSequence).ToList());
            }
        }

        //return only the set of sets with correct lenght
        return result.Where(x => x.Count == subsetSize);
    }
}