using System;
using System.Collections.Generic;

namespace RPGGame
{

  
    public class Character
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }

        public Character(string name, int maxHealth)
        {
            Name = name;
            Level = 1;
            MaxHealth = maxHealth;
            Health = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;

            if (Health < 0)
                Health = 0;

            Console.WriteLine($"{Name} took {damage} damage. Health: {Health}/{MaxHealth}");
        }

        public void Heal(int amount)
        {
            Health += amount;

            if (Health > MaxHealth)
                Health = MaxHealth;

            Console.WriteLine($"{Name} healed {amount}. Health: {Health}/{MaxHealth}");
        }

        public virtual void LevelUp()
        {
            Level++;
            MaxHealth += 10;
            Health = MaxHealth;

            Console.WriteLine($"{Name} leveled up to {Level}");
        }

        public virtual void DisplayStats()
        {
            Console.WriteLine($"Name: {Name} | Level: {Level} | Health: {Health}/{MaxHealth}");
        }
    }

   
    public class PlayableCharacter : Character
    {
        public int Experience { get; set; }
        public int ExperienceToNextLevel { get; set; }

        public PlayableCharacter(string name, int maxHealth) : base(name, maxHealth)
        {
            Experience = 0;
            ExperienceToNextLevel = 100;
        }

        public void GainExperience(int exp)
        {
            Experience += exp;

            Console.WriteLine($"{Name} gained {exp} XP");

            if (Experience >= ExperienceToNextLevel)
            {
                Experience = 0;
                LevelUp();
            }
        }
    }

    public class Warrior : PlayableCharacter
    {
        public int Strength { get; set; }
        public int Armor { get; set; }

        public Warrior(string name) : base(name, 120)
        {
            Strength = 20;
            Armor = 10;
        }

        public void Attack(Character enemy)
        {
            Console.WriteLine($"{Name} attacks {enemy.Name}");
            enemy.TakeDamage(Strength);
        }

        public void Defend()
        {
            Console.WriteLine($"{Name} defends and reduces damage.");
        }

        public override void LevelUp()
        {
            base.LevelUp();

            Strength += 5;
            Armor += 3;

            Console.WriteLine($"{Name}'s Strength increased to {Strength}");
            Console.WriteLine($"{Name}'s Armor increased to {Armor}");
        }

        public override void DisplayStats()
        {
            base.DisplayStats();
            Console.WriteLine($"Strength: {Strength} | Armor: {Armor}");
        }
    }

    
    public class Mage : PlayableCharacter
    {
        public int Mana { get; set; }
        public int MaxMana { get; set; }
        public int SpellPower { get; set; }

        public Mage(string name) : base(name, 80)
        {
            MaxMana = 100;
            Mana = 100;
            SpellPower = 25;
        }

        public void CastSpell(Character enemy)
        {
            if (Mana >= 20)
            {
                Mana -= 20;

                Console.WriteLine($"{Name} casts spell on {enemy.Name}");
                enemy.TakeDamage(SpellPower);
            }
            else
            {
                Console.WriteLine($"{Name} does not have enough mana.");
            }
        }

        public void RestoreMana()
        {
            Mana = MaxMana;
            Console.WriteLine($"{Name} restored mana.");
        }

        public override void LevelUp()
        {
            base.LevelUp();

            MaxMana += 20;
            Mana = MaxMana;
            SpellPower += 5;

            Console.WriteLine($"{Name}'s Mana increased to {MaxMana}");
            Console.WriteLine($"{Name}'s SpellPower increased to {SpellPower}");
        }

        public override void DisplayStats()
        {
            base.DisplayStats();
            Console.WriteLine($"Mana: {Mana}/{MaxMana} | SpellPower: {SpellPower}");
        }
    }

   
    public class Paladin : Warrior
    {
        public int Faith { get; set; }

        public Paladin(string name) : base(name)
        {
            Faith = 15;
        }

        public void HolyStrike(Character enemy)
        {
            int damage = Strength + Faith;

            Console.WriteLine($"{Name} uses Holy Strike on {enemy.Name}");
            enemy.TakeDamage(damage);
        }

        public void HealAlly(Character ally)
        {
            int healAmount = Faith * 2;

            Console.WriteLine($"{Name} heals {ally.Name}");
            ally.Heal(healAmount);
        }

        public override void LevelUp()
        {
            base.LevelUp();

            Faith += 4;

            Console.WriteLine($"{Name}'s Faith increased to {Faith}");
        }

        public override void DisplayStats()
        {
            base.DisplayStats();
            Console.WriteLine($"Faith: {Faith}");
        }
    }


    
    class Program
    {
        static void Main()
        {
            Warrior warrior = new Warrior("Thor");
            Mage mage = new Mage("Merlin");
            Paladin paladin = new Paladin("Uther");

            List<PlayableCharacter> characters = new List<PlayableCharacter>()
            {
                warrior,
                mage,
                paladin
            };

            Console.WriteLine("=== Game Start ===\n");

            foreach (var character in characters)
            {
                character.DisplayStats();
                Console.WriteLine();
            }

            Console.WriteLine("=== Battle Simulation ===\n");

            warrior.Attack(mage);
            mage.CastSpell(warrior);
            paladin.HolyStrike(mage);
            paladin.HealAlly(warrior);

            Console.WriteLine("\n=== Characters Gain Experience ===\n");

            foreach (var character in characters)
            {
                character.GainExperience(120);
            }

            Console.WriteLine("\n=== Updated Character Stats ===\n");

            foreach (var character in characters)
            {
                character.DisplayStats();
                Console.WriteLine();
            }

        }
    }

}