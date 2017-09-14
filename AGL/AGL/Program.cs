namespace AGL
{
    using System;
    using Client;

    internal class Program
    {
        /// <summary>
        /// Output a list of all the cats in alphabetical order under a heading of the gender of their owner.
        /// </summary>
        static void Main()
        {
            var aglClient = new AGLClient(); // Not doing a DI container for this task!

            var allPeople = aglClient.GetAllPeopleAsync().GetAwaiter().GetResult();

            var catNamesForMaleOwners = AGLClient.PetNamesOrdered(allPeople, "Male", "Cat");
            var catNamesForFemaleOwners = AGLClient.PetNamesOrdered(allPeople, "Female", "Cat");

            Console.WriteLine("Male");
            Console.WriteLine();
            foreach (var name in catNamesForMaleOwners)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();
            Console.WriteLine("Female");
            Console.WriteLine();
            foreach (var name in catNamesForFemaleOwners)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();
            Console.WriteLine("...press any key");
            Console.ReadKey();
        }
    }
}
