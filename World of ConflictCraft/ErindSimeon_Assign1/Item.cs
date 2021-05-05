/*
 * Course: CSCI-473   Assignment :1    Spring 2021
 * 
 * Erind Hysa   zid: z1879691
 * Simeon Lico  zid: z1885981
 * 
 * Due Date : 01/28/2021
 * 
 * Description:
 * This file containa data for the Item class. 
 * it contains equipment id, name,type
 * ilvl,primary,stamina,requirement and flavor.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ErindSimeon_Assign1
{
    public class Item : IComparable
    {
        private static uint MAX_ILVL = 360;
        private static uint MAX_PRIMARY = 200;
        private static uint MAX_STAMINA = 275;
        private static uint MAX_LEVEL = 60;
        //private properties
        private readonly uint id;
        private string name;
        private ItemType type;
        private uint ilvl;
        private uint primary;
        private uint stamina;
        private uint requirement;
        private string flavor;

        // default constructor with 0 arguments
        public Item()
        {
            uint id = 0;
            string name = "";
            ItemType type = 0;
            uint ilvl = 0;
            uint primary = 0;
            uint stamina = 0;
            uint requrement = 0;
            string flavor = "";
        }
        //alternate constructor
        public Item(uint Id, string Name, ItemType Type, uint Ilvl, uint Primary, uint Stamina, uint Requirement, string Flavor)
        {
            id = Id;
            name = Name;
            type = Type;
            ilvl = Ilvl;
            primary = Primary;
            stamina = Stamina;
            requirement = Requirement;
            flavor = Flavor;
        }

        // constructor for a number of arguments
        public Item(string[] args)
        {
            id = Convert.ToUInt32(args[0]);
            name = args[1];
            type = (ItemType)Convert.ToUInt32(args[2]);
            ilvl = Convert.ToUInt32(args[3]);
            primary = Convert.ToUInt32(args[4]);
            stamina = Convert.ToUInt32(args[5]);
            requirement = Convert.ToUInt32(args[6]);
            flavor = args[7];
        }
        // id only Read access
        public uint Id
        {
            get { return id; }
        }
        // name has read and write access
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        // Type has both read and write access

        public ItemType Type
        {
            get { return type; }
            set
            {
                if (value < 0 || (int)value > 12)
                {
                    return;
                }
                type = value;
            }
        }

        //ilvl has read and write access, range is [0,MAX_ILVL]
        public uint Ilvl
        {
            get { return ilvl; }
            set
            {
                if (value < 0 || value > MAX_ILVL)
                {
                    return;
                }
                ilvl = value;
            }
        }
        // primary has read and write access
        public uint Primary
        {
            get { return primary; }
            set
            {
                if (value < 0 || value > MAX_PRIMARY)
                {
                    return;
                }
                primary = value;
            }
        }
        // stamina has read and write access,range is [0, MAX_STAMINA]
        public uint Stamina
        {
            get { return stamina; }
            set
            {
                if (value < 0 || value > MAX_STAMINA)
                {
                    return;
                }
                stamina = value;
            }
        }

        // requirement -- free R/W access, range is [0, MAX_LEVEL]
        public uint Requirement
        {
            get { return requirement; }
            set
            {
                if (value < 0 || value > MAX_LEVEL)
                {
                    return;
                }
                requirement = value;
            }
        }
        //flavor -- free R/W access
        public string Flavor
        {
            get { return flavor; }
            set { flavor = value; }
        }
        /*
         * Function : CompareTo sorts by name, if names are same then it checks ilvl
         * 
         * Used by Icomperable interface to sort by name
         * 
         * params: object alpha
         * 
         * Return: -1 , 0  1 based on the result of the ccomparison
         */
        public int CompareTo(object alpha)
        {
            if (alpha == null) // No good
                throw new ArgumentNullException();

            Item rightOp = alpha as Item;

            if (rightOp != null)
            {
                //return FullName.CompareTo(rightOp.FullName);

                if (name.CompareTo(rightOp.name) == 0)
                {
                    return ilvl.CompareTo(rightOp.ilvl);
                }
                else
                    return name.CompareTo(rightOp.name);

            }
            else
                throw new ArgumentException();
        }
        /*
         * Function: TOString, overrride , it formats the output like this (Type) Name |ilvl| --requirement-- flavor
         * ex:(Helmet) Newbie's Helmet |1| --1--
         *   "Every adventure has humble beginnings!"
         * Formats the output
         * Parametrs: none
         * return: the formated string
         */
        public override string ToString()
        {
            StringBuilder result = new StringBuilder(String.Format("({0}) {1} |{2}| --{3}--\n\t\"{4}\"", Type, Name, Ilvl, Requirement, Flavor));
            return result.ToString();         
        }
    }
}