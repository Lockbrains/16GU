using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    public int _id;

    public string firstName;
    public string lastName;
    public string nativeName;
    public string nicknames;

    public struct Birthday
    {
        public int _year;
        public int _month;
        public int _day;

        public Birthday(int yy, int mm, int dd)
        {
            _year = yy;
            _month = mm;
            _day = dd;
        }

        public override string ToString()
        {
            return _month.ToString() + " / " + _day.ToString() + ", " + _year.ToString();
        }
    }

    public Birthday birthday;

    public float height;
    public float weight;

    public Texture2D profilePhoto;
    public Texture2D illustration;

    public Character(int id)
    {
        _id = id;
    }
}
