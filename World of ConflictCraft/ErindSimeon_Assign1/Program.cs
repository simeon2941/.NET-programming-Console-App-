/*
 * Course: CSCI-473   Assignment :1    Spring 2021
 * 
 * Erind Hysa   zid: z1879691
 * Simeon Lico  zid: z1885981
 * 
 * Due Date : 01/28/2021
 * 
 * Description:
 * This is the main file containing the main routine. This program
 * reads equipment.txt guild.txt and players.txt from 3 .txt files
 * and populates them into 3 dictionaries. It lets the user to pick
 * out of 10 option to print all players, to print all gear,
 * to join a guild, to leave a guild, to print gear list for a particualar
 * player, to equip gear, to unequip gear to award experience and to exit.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ErindSimeon_Assign1
{
    /*
     * public static class cllaed globals that hold global storage dictionaries
     */
    public static class globals
    {
        public static Dictionary<uint, string> map = new Dictionary<uint, string>();
        public static Dictionary<uint, Player> player = new Dictionary<uint, Player>();
        public static Dictionary<uint, Item> equipment = new Dictionary<uint, Item>();
        public static SortedSet<Player> allPlayers = new SortedSet<Player>();
        public static SortedSet<Item> allItems = new SortedSet<Item>();

    }
    /*
     * enum ItemType which holds item types
     */
    public enum ItemType
    {
        Helmet, Neck, Shoulders, Back, Chest,
        Wrist, Gloves, Belt, Pants, Boots,
        Ring, Trinket
    };
    /* 
     * enum Race which holds differences race for player
     */

    public enum Race { Orc, Troll, Tauren, Forsaken };


    class Program
    {

        private static uint GEAR_SLOTS = 14;    
        public static string playerName;  //string player name used in different methods to ask for players name
        public static string itemName;    // string item name used in different methods to as for itemName

        /*
         * This functions will read the 3 .txt files and populate their respective dictionary
         * after reading players.txt it will populate player dictionary, after reading guilds.txt 
         * it will populate map dictionary and after reading equipments.txt it will populate the 
         * equipment dictionary
         * params: Dictionary<uint, Player> player, Dictionary<uint, string> map, Dictionary<uint, Item> equipment
         * returns: none
         */

        public static void ReadInputFiles(Dictionary<uint, Player> player, Dictionary<uint, string> map, Dictionary<uint, Item> equipment)
        {
            string slacker;
            string[] tokens;
            string ReadGuild;
            string[] tokens1;
            string ReadEquipment;
            string[] tokens2;
            /*
             * try to read the files players.txt, if it cant catch the exceptions
             * split each line into tokens and then populate the dictionary player
             */
            try
            {
                using (StreamReader inFile = new StreamReader("..//..//players.txt"))
                {
                    slacker = inFile.ReadLine();

                    while (slacker != null)
                    {
                        tokens = slacker.Split('\t');
                        player.Add(Convert.ToUInt32(tokens[0]), new Player(tokens));

                        slacker = inFile.ReadLine();
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            /*
            * try to read the files guilds.txt, if it cant catch the exceptions
            * split each line into tokens and then populate the dictionary map
            */
            try
            {
                using (StreamReader inFile1 = new StreamReader("..//..//guilds.txt"))
                {
                    ReadGuild = inFile1.ReadLine();
                    while (ReadGuild != null)
                    {
                        tokens1 = ReadGuild.Split('\t');
                        map.Add(Convert.ToUInt32(tokens1[0]), tokens1[1]);
                        ReadGuild = inFile1.ReadLine();
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            /*
            * try to read the files equipment.txt, if it cant catch the exceptions
            * split each line into tokens and then populate the dictionary equipment
            */
            try
            {
                using (StreamReader inFile = new StreamReader("..//..//equipment.txt"))
                {
                    ReadEquipment = inFile.ReadLine();

                    while (ReadEquipment != null)
                    {
                        tokens2 = ReadEquipment.Split('\t');
                        equipment.Add(Convert.ToUInt32(tokens2[0]), new Item(tokens2));

                        ReadEquipment = inFile.ReadLine();
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
        /*
         *  method Menu prints out the Menu of options
         *  params: none
         *  return: none
         */
        public static void Menu()
        {
            Console.WriteLine("\nWelcome to the world of ConflictCraft: Testing Environment. Please select an option from the list below: ");
            Console.WriteLine("\t1.) Print All Players");
            Console.WriteLine("\t2.) Print All Guilds");
            Console.WriteLine("\t3.) List All Gear");
            Console.WriteLine("\t4.) Print Gear List for Player");
            Console.WriteLine("\t5.) Leave Guild");
            Console.WriteLine("\t6.) Join Guild");
            Console.WriteLine("\t7.) Equip Gear");
            Console.WriteLine("\t8.) Unequip Gear");
            Console.WriteLine("\t9.) Award Experience");
            Console.WriteLine("\t10.) Quit");
        }
        /*
         * method item will print the itemType
         * param: none
         * return:none
         */
        public static void item()
        {
            Console.WriteLine("\t0 = Helmet");
            Console.WriteLine("\t1 = Neck");
            Console.WriteLine("\t2 = Shoulders");
            Console.WriteLine("\t3 = Back");
            Console.WriteLine("\t4 = Chest");
            Console.WriteLine("\t5 = Wrist");
            Console.WriteLine("\t6 = Gloves");
            Console.WriteLine("\t7 = Belt");
            Console.WriteLine("\t8 = Pants");
            Console.WriteLine("\t9 = boots");
            Console.WriteLine("\t10 = ring");
            Console.WriteLine("\t11 = trinket");
        }
       /* Method PrintPlayers() will print all the players
        * it will call ToString() to format them nicely
        * params:none
        * return:none
        */
        public static void PrintPlayers()
        {
            foreach (KeyValuePair<uint, Player> kvp in globals.player.OrderBy(key => key.Value.Name))
            {
                if (globals.map[kvp.Value.GuildID] != "")
                {
                    Console.WriteLine(kvp.Value.ToString());
                }
                else
                {
                    Console.WriteLine("Name: {0,-10}\tRace: {1,-10}\tLevel: {2,-10}", kvp.Value.Name, kvp.Value.Race, Convert.ToInt32(kvp.Value.Level));
                }
            }
        }

        /*
         * ListAllGear() this method with iterate through equipment dictionary
         * and it will print all the available equipments on the dictionary,
         * it will call ToString() to format the output nicely
         * params: none
         * return: none
         */
        public static void ListAllGear()
        {
            foreach (KeyValuePair<uint, Item> kvp in globals.equipment)
            {
                Console.WriteLine(kvp.Value.ToString());
            }
        }
        /*
         * method PrintListGearForPlayer() will aks user for players name, it will check if that name exists on player dictionary
         * that if it does it will iterate on the equipment dictionary and will call ToString() to ouput nicelyt the equipment
         * for a particular player
         * params: none
         * return: none
         */
        public static void PrintListGearForPlayer()
        {
            string playerName;
            Console.WriteLine("Enter the player name: ");
            playerName = Console.ReadLine();
            foreach (KeyValuePair<uint, Player> kvp in globals.player)
            {
                
                if (kvp.Value.Name.ToUpper() == playerName.ToUpper())
                {
                    Console.WriteLine(kvp.Value.ToString());
                    for (int gear = 0; gear < GEAR_SLOTS; gear++)
                    {
                        if (globals.equipment.ContainsKey(kvp.Value.Gear[gear]))
                        {
                            Console.WriteLine(globals.equipment[kvp.Value.Gear[gear]].ToString());
                        }
                        else
                        {
                            Console.WriteLine("{0} : Empty", (ItemType)gear);
                        }

                    }

                }
            }
        }
    
        /*
         * method LeaveGuild() will as user to enter the players name, it will check if thats a valid player
         * in players dictionary, if it is it will remove that player from its guild on the map dictionary
         * by changing the guild id into 0 wich points to no guild.
         * params:none
         * return:none
         */

        public static void LeaveGuild()
        {
            Console.WriteLine("Enter the player name: ");
            playerName = Console.ReadLine();
            foreach (KeyValuePair<uint, Player> kvp in globals.player)
            {
                if (kvp.Value.Name.ToUpper() == playerName.ToUpper())
                {
                    kvp.Value.GuildID = 0;
                    globals.map.Add(0, "");
                    Console.WriteLine("{0} has left their Guild.", playerName);
                }

            }
        }
        /*method JoinGuild() asksuser for a player name, check if that player exists on player dictionary
         * then asks the user for a guilds name, if the name exists in map dictionary then player GuildId
         * will be updated to match that of the new guild
         * params:none
         * return:none
         */
        public static void JoinGuild()
        {
            string guildName;
            Console.WriteLine("Enter the player name: ");
            playerName = Console.ReadLine();
            Console.WriteLine("Enter the Guild they will join: ");
            guildName = Console.ReadLine();
            foreach (KeyValuePair<uint, Player> kvp in globals.player)
            {
                if (kvp.Value.Name.ToUpper() == playerName.ToUpper())
                {
                    foreach (KeyValuePair<uint, string> g in globals.map)
                    {
                        if (g.Value.ToUpper() == guildName.ToUpper())
                        {
                            kvp.Value.GuildID = g.Key;
                            Console.WriteLine("{0} has joined {1}!", playerName, guildName);
                        }
                    }

                }

            }
        }
        /*
         * equipGear() asks user for a player name and the name of item they want to equip,
         * it iterates through equipment dictionary to find out the key of the entered equipment,
         * if the item name exists then it gets the key. After getting the key it iterates on players
         * dictionary and if the entered name matches then it calls EquipGear(tempId) to equip the player
         * with the entered gear.
         * params:none
         * return: none
         */
        public static void equipGear()
        {     
            string itemName;
            uint tempId = 99;

            Console.WriteLine("Enter the player name: ");
            playerName = Console.ReadLine();
            Console.WriteLine("Enter the item name they will equip: ");
            itemName = Console.ReadLine();
            foreach (KeyValuePair<uint, Item> i in globals.equipment)
            {
                if (i.Value.Name.ToUpper() == itemName.ToUpper())
                {
                    tempId = i.Key;
                    break;
                }
            }
            foreach (KeyValuePair<uint, Player> p in globals.player)
            {
                if (p.Value.Name.ToUpper() == playerName.ToUpper())
                {
                    try
                    {
                        p.Value.EquipGear(tempId);
                        Console.WriteLine("{0} successfully equipped {1}!", playerName, itemName);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }                  
                }                    
            }
        }
        /*
        * unEquipGear method asks user for players number and a slot number out of item method
        * it iterates through players dictionary and if player has this item it removes it and adds it
        * to the inventory if inventory its not full.
        * params: none
        * return: none
        */
        public static void unEquipGear()
        {
            string playerName;
            string slotNumber;
            Menu();
            Console.WriteLine("Enter the player name: ");
            playerName = Console.ReadLine();
            Console.WriteLine("Enter the item slot number they will unequip: ");
            item();
            slotNumber = Console.ReadLine();
            foreach (KeyValuePair<uint, Player> p in globals.player)
            {
                if (p.Value.Name.ToUpper() == playerName.ToUpper())
                {
                    try
                    {
                        p.Value.UnequipGear(Convert.ToInt32(slotNumber));
                        Console.WriteLine("{0} successfully unequipped {1}!", playerName,(ItemType) Convert.ToInt32(slotNumber));
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    
                }
            }
        }
        /*
         * awardExperience() will ask the user for a players name, it will check if thats a valid
         * name, if it is and its level its smaller thanthe max_level that it will update its level
         * params:none
         * return:npne
         */
        public static void awardExperience()
        {
            string playerName;
            string amtExperience;
            Menu();
            Console.WriteLine("Enter the player name: ");
            playerName = Console.ReadLine();
            Console.WriteLine("Enter the amount of experience to award:");
            amtExperience = Console.ReadLine();
            foreach (KeyValuePair<uint, Player> p in globals.player)
            {
                if (p.Value.Name.ToUpper() == playerName.ToUpper())
                {
                    p.Value.Exp = Convert.ToUInt32(amtExperience);
                }
            }

        }
                        /*
         * Method PrintGuild()
         * it will loop through the map dictionary and will print all the values
         * which are the names of guilds
         * params: none
         * returns: none
         */
        public static void PrintGuild()
        {
            foreach (KeyValuePair<uint, string> kvp in globals.map)
            {
                Console.WriteLine(kvp.Value);
            }
        }
        /*
         * this method with iterate through players dictionary and item dictionary and pass
         * the items of those dictionaries in 2 corresponding SortedSet both sorted by name
         * params:none
         * returns:none
         */
        public static void hiddenOption()
        {
            List<uint> allGear = new List<uint>();
            List<uint> allInventory = new List<uint>();


            foreach (KeyValuePair<uint, Player> p in globals.player)
            {

                for (int i = 0; i < GEAR_SLOTS; i++)
                {

                    allGear.Add(Convert.ToUInt32(p.Value.Gear[i]));
                }

                globals.allPlayers.Add(new Player(p.Value.Id, p.Value.Name, p.Value.Race, p.Value.Level, p.Value.Exp, p.Value.GuildID, allGear, allInventory));

            }
            foreach (KeyValuePair<uint, Item> i in globals.equipment)
            {
                globals.allItems.Add(new Item(i.Value.Id, i.Value.Name, i.Value.Type, i.Value.Ilvl, i.Value.Primary, i.Value.Stamina, i.Value.Requirement, i.Value.Flavor));

            }
            foreach (Player p in globals.allPlayers)
            {
                Console.WriteLine(p.ToString());
            }
            foreach (Item i in globals.allItems)
            {
                Console.WriteLine(i.ToString());
            }
        }
        /* main driver to keep calling the menu and interact with the user
         */

        static void Main(string[] args)
        {
            ReadInputFiles(globals.player, globals.map, globals.equipment);
            string userInput;
            string playerName;
            string guildName;
            string itemName;
            Console.WriteLine("Welcome to the world of ConflictCraft: Testing Environment!");
            Menu();
            userInput = Console.ReadLine();
            while ((userInput != "10") && userInput.ToUpper() != "Q" && userInput.ToUpper() != "QUIT" && userInput.ToUpper() != "EXIT")
            {
                switch (userInput)
                {
                    case "1":
                        PrintPlayers();
                        Menu();
                        break;
                    case "2":
                        PrintGuild();
                        Menu();
                        break;
                    case "3":

                        ListAllGear();
                        Menu();
                        break;
                    case "4":

                        PrintListGearForPlayer();
                        Menu();
                        break;
                    case "5":

                        LeaveGuild();
                        Menu();
                        break;
                    case "6":

                        JoinGuild();
                        Menu();
                        break;
                    case "7":

                        try
                        {
                            equipGear();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        
                        Menu();
                        break;
                    case "8":
                        try
                        {
                            unEquipGear();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        Menu();
                        break;
                    case "9":
                        awardExperience();
                        Menu();
                        break;
                    case "10":
                        break;
                    case  "T":
                        hiddenOption();
                        break;
                    default:
                        Console.WriteLine("Please enter one of the options below!");
                        Menu();
                        break;
                }
                userInput = Console.ReadLine();
            }
        }
    }
}
