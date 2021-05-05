/*
 * Course: CSCI-473   Assignment :1    Spring 2021
 * 
 * Erind Hysa   zid: z1879691
 * Simeon Lico  zid: z1885981
 * 
 * Due Date : 01/28/2021
 * 
 * Description:
 * This file contains the player class. With all getters and setters for 
 * the private data types. It implements the iComperable interface to sort
 * by name. There are 2 action methods for player ( equip gear and unequp gear)
 * Overloads ToString() to print the output nicely
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ErindSimeon_Assign1
{

    public class Player : IComparable
    {
        //global variables 
        private static uint MAX_LEVEL = 60;
        private static uint GEAR_SLOTS = 14;
        private static uint MAX_INVENTORY_SIZE = 20;
        //private attributes
        

        private readonly uint id;
        private readonly string name;
        private readonly Race race;
        private uint level;
        private uint exp;
        private uint guildID;
        private List<uint> gear = new List<uint>();
        private List<uint> inventory = new List<uint>();
        //bools to keep track of alternating trinket and ring
        public bool ring = false;
        public bool trinket = false;

        /*
         * Constructor Player
         * Default constructor for Player
         * parameters none
         */
        public Player()
        {
            id = 0;
            name = "";
            race = 0;
            level = 0;
            exp = 0;
            guildID = 0;
            List<uint> gear = new List<uint>();
            List<uint> inventory = new List<uint>();
        }

        /* constructor that takes a number of arguments
         * params: an array of arguments
         */

        public Player(string[] args)
        {
            id = Convert.ToUInt32(args[0]);
            name = args[1];
            race = (Race)Convert.ToUInt32(args[2]);
            level = Convert.ToUInt32(args[3]);
            exp = Convert.ToUInt32(args[4]);
            guildID = Convert.ToUInt32(args[5]);
            gear = new List<uint>();
            for (int i = 6; i < GEAR_SLOTS + 6; i++)
            {
                gear.Add(Convert.ToUInt32(args[i]));
            }
            inventory = new List<uint>();

        }
        /*
         * Alternative constructor
         * params: uint id, string name, Race race, uint level, uint exp, uint guildID, List<uint> gear, List<uint> inventory
         */

        public Player(uint id, string name, Race race, uint level, uint exp, uint guildID, List<uint> gear, List<uint> inventory)
        {
            this.id = id;
            this.name = name;
            this.race = race;
            this.level = level;
            this.exp = exp;
            this.guildID = guildID;
            gear = new List<uint>();
            this.gear = gear;
            this.inventory = inventory;
        }
        // id has only read access
        public uint Id
        {
            get { return id; }
        }
        // name -- only R access
        public string Name
        {
            get { return name; }
        }
        // race -- only R access
        public Race Race
        {
            get { return race; }
        }
        //level -- free R/W access, range is [0, MAX_LEVEL].
        public uint Level
        {
            get { return level; }
            set
            {
                if (value < 0 || value > MAX_LEVEL)
                {
                    return;
                }
                Console.WriteLine("Ding!");
                level = value;

            }
        }
        //exp -- normal R access, but the W access should instead increment the value of exp by... value. 
        //If this should make the exp value exceed the required experience for this player to increase their level (but not exceed MAX_LEVEL), it should do as such.
        public uint Exp
        {
            get { return exp; }
            set
            {
                exp += value;

                while (exp > (1000 * Level))
                {
                    Level++;
                    if (exp < (1000 * Level))
                    {
                        break;
                    }
                    exp = exp - ((1000 * Level));
                }
            }
        }

        // guildID -- free R/W access
        public uint GuildID
        {
            get { return guildID; }
            set { guildID = value; }
        }
        //gear -- instead of a Property, you should create an Indexer to allow access to gear.
        public List<uint> Gear
        {
            get { return gear; }
            set { gear = value; }
        }
        //inventory -- will not have a corresponding Property.
        public List<uint> Inventory
        {
            get { return inventory; }
            set { inventory = value; }
        }

        /*
        * Function : CompareTo sorts by name
        * Comares player object by name
        * it return -1 , 0 or 1 based on the result of condition (-1 if its< . 0 if == and 1 if
        * params: object alpha
        * return -1 || 0 || -1
        * 
        */

        public int CompareTo(object alpha)
        {
            if (alpha == null) // No good
                throw new ArgumentNullException();

            Player rightOp = alpha as Player;

            if (rightOp != null)
            {
                //return FullName.CompareTo(rightOp.FullName);

                if (name.CompareTo(rightOp.name) == 0)
                {
                    return level.CompareTo(rightOp.level);
                }
                else
                    return name.CompareTo(rightOp.name);

            }
            else
                throw new ArgumentException();
        }

        /*
         * Function: TOString, overrride  It prints Name: Race: Level: Guild
         * ex: Name: Scobomb           Race: Tauren            Level: 60               Guild: Death and Taxes
         * Formats the output
         * Parametrs: none
         * return: the formated string
         */

        public override string ToString()
        {
            StringBuilder result = new StringBuilder(String.Format("Name: {0,-10}\tRace: {1,-10}\tLevel: {2,-10}\tGuild: {3,-10}", Name, Race, Level, globals.map[GuildID]));
            return result.ToString();
        }
 
        /*
         * EquipGear(uint newgearID)
         * it equips the player with a new gear,
         * it checks  if the Player's Level matches or exceeds the Requirement for the piece of gear in question — if they don't meet the level requirement, throw a new Exception
         * If we're clear to equip the gear, place that ID value into the correct element of gear list
         * params: newgearId
         * return: nothing
         */

        public void EquipGear(uint newGearId)
        {
                    // it checks  if the Player's Level matches or exceeds the Requirement for the piece of gear in question — 
                    //if they don't meet the level requirement, throw a new Exception
                    if (Level < globals.equipment[newGearId].Requirement)
                    {
                        throw new Exception("Cant do it");
                    }
                    //case where itemtype is not trinket or ring
                    if (globals.equipment[newGearId].Type != ItemType.Trinket && globals.equipment[newGearId].Type != ItemType.Ring)
                    {
                        Gear[Convert.ToInt32(globals.equipment[newGearId].Type)] = newGearId;

                    }
                    //if itemtype is ring alternate by puttting it in positions 10,11
                    else if (globals.equipment[newGearId].Type == ItemType.Ring)
                    {
                        if (ring)
                        {
                            Gear[11] = newGearId;
                            ring = false;

                        }
                        else
                        {
                            Gear[10] = newGearId;
                            ring = true;

                        }
                    }
                    //if its trinket alternate by putting it in positions 12,13
                    else
                    {
                        if (trinket)
                        {
                            Gear[13] = newGearId;
                            trinket = false;
                        }
                        else
                        {
                            Gear[12] = newGearId;
                            trinket = true;

                        }
                    }
        }
        /*
         * EquopGear(int gearSlot)
         * it unequips the player on a specified gear slot
         * if gear at slot number exists it unequips it at puts it to inventory
         * if inventory is full that it throws a new exception
         * params: gearslot
         * return: nothing
         */
        public void UnequipGear(int gearSlot)
        {
            
            //check to see if gear exists
            if (Gear[gearSlot] != null)
            {
                if (Inventory.Count <= MAX_INVENTORY_SIZE)
                {
                    Inventory.Add(Gear[Convert.ToInt32(gearSlot)]);
                }
                else
                {
                    throw new Exception("Inventory is full!");
                }
                Gear[Convert.ToInt32(gearSlot)] = 0;

            }
            else
            {
                throw new Exception("Player doesnt have any gear at that index");
            }
        }
                
     

        

    }
}