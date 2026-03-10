using System;

namespace Exercise5
{
    public interface IEnglishSpeaker
    {
        string Greet();
        string Goodbye();
    }

    public interface ISpanishSpeaker
    {
        string Greet();
        string Goodbye();
    }

    public interface IFrenchSpeaker
    {
        string Greet();
        string Goodbye();
    }

    public class PolyglotPerson : IEnglishSpeaker, ISpanishSpeaker, IFrenchSpeaker
    {
        public string Name { get; set; } = string.Empty;

        string IEnglishSpeaker.Greet() => $"{Name} says: Hello!";
        string IEnglishSpeaker.Goodbye() => "Goodbye!";

        string ISpanishSpeaker.Greet() => $"{Name} says: ¡Hola!";
        string ISpanishSpeaker.Goodbye() => "¡Adiós!";

        string IFrenchSpeaker.Greet() => $"{Name} says: Bonjour!";
        string IFrenchSpeaker.Goodbye() => "Au revoir!";

        public void GreetInLanguage(string language)
        {
            string greeting = language.ToLower() switch
            {
                "english" => ((IEnglishSpeaker)this).Greet(),
                "spanish" => ((ISpanishSpeaker)this).Greet(),
                "french" => ((IFrenchSpeaker)this).Greet(),
                _ => "Language not supported"
            };

            Console.WriteLine(greeting);
        }
    }

    class Program
    {
        static void Main()
        {
            PolyglotPerson person = new() { Name = "Divya" };

            IEnglishSpeaker english = person;
            Console.WriteLine(english.Greet());

            ISpanishSpeaker spanish = person;
            Console.WriteLine(spanish.Greet());

            IFrenchSpeaker french = person;
            Console.WriteLine(french.Greet());

            person.GreetInLanguage("spanish");
        }
    }
}