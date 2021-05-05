using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// container class
/// </summary>
[Serializable]
public class CreationTable
{
    /// <summary>
    /// container of the list of resutls of the combination of all particles
    /// </summary>
    public CreationtableItem[] content;
}

/// <summary>
/// Element determing the result of the combination of two particles
/// </summary>
[Serializable]
public class CreationtableItem {
    /// <summary>
    /// type of particle 1 stringified to be decoded from file
    /// </summary>
    public string type1;
    /// <summary>
    /// type of particle 2 stringified to be decoded from file
    /// </summary>
    public string type2;
    /// <summary>
    /// codfied result item. can be a generation of something or a visual effect
    /// </summary>
    public string result;

    /// <summary>
    /// method to be used internally to create a dummy item to be verified, not to be used otherwise
    /// </summary>
    /// <param name="t1">type of the first particle</param>
    /// <param name="t2">type of the second particle</param>
    public CreationtableItem(ParticleType t1, ParticleType t2)
    {
        type1 = t1.ToString();
        type2 = t2.ToString();
    }

    /// <summary>
    /// general purpose equality function
    /// </summary>
    /// <param name="obj">object to be compared with</param>
    /// <returns>true if equal, false otherwise</returns>
    public override bool Equals(object obj)
    {
        return this.Equals(obj as CreationtableItem);
    }

    /// <summary>
    /// two creationtable Items are equal if the two particle type are the same (order is not meaningfull)
    /// redefinition of the proper Equal function for the type, check also special cases 
    /// </summary>
    /// <param name="other">object to be compared with</param>
    /// <returns>true if equal, false otherwise</returns>
    public bool Equals(CreationtableItem other)
    { 
        if(Object.ReferenceEquals(other, null)){
            return false;
        }
        if (Object.ReferenceEquals(other, this))
        {
            return true;
        }
        if (this.GetType() != other.GetType())
        {
            return false;
        }
        return (this.type1 == other.type1 && this.type2 == other.type2) || (this.type1 == other.type2 && this.type2 == other.type1);
    }

    /// <summary>
    /// equality operator among two creationtable Item
    /// </summary>
    /// <param name="t1">firt element to be checked</param>
    /// <param name="t2">second element to be checked</param>
    /// <returns>true if equal, false otherwise</returns>
    public static bool operator ==(CreationtableItem t1, CreationtableItem t2) {
        if (Object.ReferenceEquals(t1, null)) {
            if (Object.ReferenceEquals(t2, null))
            {
                return true;
            }
            return false;
        }
        return t1.Equals(t2);
    }

    /// <summary>
    /// disequality operator among two creationtable Item
    /// </summary>
    /// <param name="t1">firt element to be checked</param>
    /// <param name="t2">second element to be checked</param>
    /// <returns>true if unequal, false otherwise</returns>
    public static bool operator !=(CreationtableItem t1, CreationtableItem t2)
    {
        
        return !(t1 == t2);
    }

    public override int GetHashCode()
    {

        return base.GetHashCode();
    }
}
