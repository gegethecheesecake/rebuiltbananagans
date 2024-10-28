using System;
using System.Collections.Generic;

namespace SimpleRPG
{
    // Base Character Class
    public class Character
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }

        public Character(string name, int health, int strength, int defense)
        {
            Name = name;
            Health = health;
            Strength = strength;
            Defense = defense;
        }

        public virtual void Attack(Character target)
        {
            int damage = Math.Max(0, Strength - target.Defense);
            target.Health -= damage;
            Console.WriteLine($"{Name} attacks {target.Name} for {damage} damage.");
        }

        public bool IsAlive()
        {
            return Health > 0;
        }
    }

    // Player Class
    public class Player : Character
    {
        public List<string> Inventory { get; private set; }

        public Player(string name) : base(name, 100, 15, 5)
        {
            Inventory = new List<string>();
        }

        public void AddItem(string item)
        {
            Inventory.Add(item);
            Console.WriteLine($"{item} has been added to your inventory.");
        }

        public void UseItem(string item)
        {
            if (Inventory.Contains(item))
            {
                Inventory.Remove(item);
                if (item == "Health Potion")
                {
                    Health += 20;
                    Console.WriteLine($"{Name} uses a Health Potion and restores 20 health.");
                }
                else
                {
                    Console.WriteLine($"{Name} uses {item}, but nothing happens.");
                }
            }
            else
            {
                Console.WriteLine($"{item} is not in your inventory.");
            }
        }
    }

    // Enemy Class
    public class Enemy : Character
    {
        public Enemy(string name, int health, int strength, int defense) 
            : base(name, health, strength, defense) { }

        public override void Attack(Character target)
        {
            base.Attack(target);
            Console.WriteLine($"{Name} (enemy) retaliates against {target.Name}.");
        }
    }

    // Main Game Class
    public class Game
    {
        private Player player;
        private List<Enemy> enemies;

        public Game()
        {
            player = new Player("Hero");
            enemies = new List<Enemy>
            {
                new Enemy("Goblin", 30, 10, 2),
                new Enemy("Orc", 50, 12, 4),
                new Enemy("Dragon", 100, 20, 10)
            };
        }

        public void Start()
        {
            Console.WriteLine("Welcome to the Simple RPG Game!");
            Console.WriteLine($"You are {player.Name} with {player.Health} health.");
            
            while (player.IsAlive() && enemies.Count > 0)
            {
                Console.WriteLine("\n
