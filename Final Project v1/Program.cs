using System;
using System.Collections.Generic;

namespace Final_Project_v1
{
    class Factory
    {
        private string name;
        private string location;
        public List<Vehicle> vehicles;
        public List<Company> companies;
        public List<Material> invintory;
        public Factory(string n, string l)
        {
            name = n;
            location = l;
            vehicles = new List<Vehicle>();
            companies = new List<Company>();
            invintory = new List<Material>();
        }
        public void Build(Vehicle type, int quantity)
        {
            int total = 0;
            foreach(var i in type.parts)
            {
                foreach(var j in invintory)
                {
                    if (i.item1 == j.name & (i.number1 * quantity) > j.quantity)
                    {
                        int lowest = Int32.MaxValue;
                        foreach(var l in companies)
                        {
                            if(i.item1 == l.item & l.price < lowest)
                            {
                                lowest = l.price;
                            }
                        }
                        total += j.Buy((i.number1 * quantity) - j.quantity, lowest);
                        j.quantity = 0;
                    }
                    else if(i.item1 == j.name & (i.number1 * quantity) <= j.quantity)
                    {
                        j.quantity -= (i.number1 * quantity);
                    }
                    if (i.item2 == j.name & (i.number2 * quantity) > j.quantity)
                    {
                        int lowest = Int32.MaxValue;
                        foreach (var l in companies)
                        {
                            if (i.item2 == l.item & l.price < lowest)
                            {
                                lowest = l.price;
                            }
                        }
                        total += j.Buy((i.number2 * quantity) - j.quantity, lowest);
                        j.quantity = 0;
                    }
                    else if (i.item1 == j.name & (i.number2 * quantity) <= j.quantity)
                    {
                        j.quantity -= (i.number2 * quantity);
                    }
                }
            }
            Console.WriteLine("Total cost of order was: $" + total + "\n");
        }
        public void Build(Vehicle type)
        {
            int total = 0;
            foreach (var i in type.parts)
            {
                foreach (var j in invintory)
                {
                    if (i.item1 == j.name & (i.number1) > j.quantity)
                    {
                        int lowest = Int32.MaxValue;
                        foreach (var l in companies)
                        {
                            if (i.item1 == l.item & l.price < lowest)
                            {
                                lowest = l.price;
                            }
                        }
                        total += j.Buy((i.number1) - j.quantity, lowest);
                        j.quantity = 0;
                    }
                    else if (i.item1 == j.name & (i.number1) <= j.quantity)
                    {
                        j.quantity -= (i.number1);
                    }
                    if (i.item2 == j.name & (i.number2) > j.quantity)
                    {
                        int lowest = Int32.MaxValue;
                        foreach (var l in companies)
                        {
                            if (i.item2 == l.item & l.price < lowest)
                            {
                                lowest = l.price;
                            }
                        }
                        total += j.Buy((i.number2) - j.quantity, lowest);
                        j.quantity = 0;
                    }
                    else if (i.item1 == j.name & (i.number2) <= j.quantity)
                    {
                        j.quantity -= (i.number2);
                    }
                }
            }
            Console.WriteLine("Total cost of order was: $" + total + "\n");
        }
        public void AddMaterial(string name, int q)
        {
            invintory.Add(new Material(name, q));
        }
        public void AddCompany(string name, string item, int price)
        {
            companies.Add(new Company(name, item, price));
        }
        public void Factoryinformation()
        {
            Console.WriteLine("The factories name is {0} and it is located in {1}.", name, location);
            foreach (var i in invintory)
            {
                Console.WriteLine("There are {0} units of {1} at {2}.", i.quantity, i.name, name);
            }
            Console.WriteLine();
            Console.WriteLine("The vehicles that can be produced at the factory are: ");
            foreach (var i in vehicles)
            {
                Console.WriteLine(i.name + " - " + i.GetType().Name);
            }
            Console.WriteLine();
            foreach (var i in companies)
            {
                Console.WriteLine("{0} can be bought from {1} for ${2} a unit", i.item, i.name, i.price);
            }
            return;
        }
        public void ShowVehicles()
        {
            foreach (var i in vehicles)
            {
                Console.WriteLine(i.name);
            }
        }
    }
    class Company
    {
        public string name;
        public string item;
        public int price;

        public Company(string n, string i, int p)
        {
            name = n;
            item = i;
            price = p;
        }
    }
    abstract class Vehicle
    {
        public string name { get; set; }
        public List<Part> parts;
        public void AddPart(string n, string i1, string i2, int n1, int n2)
        {
            parts.Add(new Part(n, i1, i2, n1, n2));
        }
        public void ShowParts()
        {
            Console.WriteLine("Parts needed to make one {0} are: ", name);
            foreach (var i in parts)
            {
                Console.WriteLine("{0} - needs {1} unit(s) of {2} and {3} unit(s) of {4}", i.name, i.number1, i.item1, i.number2, i.item2);
            }
            Console.WriteLine();
        }
    }

    class Car : Vehicle
    {
        public Car(string n)
        {
            name = n;
            this.parts = new List<Part>();
        }
    }
    class Truck : Vehicle
    {
        public Truck(string n)
        {
            name = n;
            this.parts = new List<Part>();
        }
    }
    class Van : Vehicle
    {
        public Van(string n)
        {
            name = n;
            this.parts = new List<Part>();
        }
    }
    class Part
    {
        public string name { get; set; }
        public string item1;
        public string item2;
        public int number1;
        public int number2;
        public Part(string n, string i1, string i2, int n1, int n2)
        {
            name = n;
            item1 = i1;
            item2 = i2;
            number1 = n1;
            number2 = n2;
        }
    }
    class Material
    {
        public string name { get; set; }
        public int quantity { get; set; }
        public Material(string name, int q)
        {
            this.name = name;
            quantity = q;
        }
        public int Buy(int num, int price)
        {
            return (num * price); 
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Factory factory = new Factory("Factoryx" , "Calgary");
            factory.AddMaterial("Steel", 25);
            factory.AddMaterial("Plastic", 50);
            factory.AddMaterial("Rubber", 10);
            factory.AddMaterial("Glass", 0);
            factory.AddCompany("RealSteel", "Steel", 20);
            factory.AddCompany("Plastic Masters", "Plastic", 10);
            factory.AddCompany("Rubber Royalty", "Rubber", 5);
            factory.AddCompany("Glass Town", "Glass", 50);
            Car Sedan = new Car("Sedan");
            Sedan.AddPart("Body", "Steel", "Glass", 10, 2);
            Sedan.AddPart("Tires", "Rubber", "Steel", 3, 1);
            Sedan.AddPart("Windows", "Glass", "Steel", 5, 1);
            factory.vehicles.Add(Sedan);
            Truck Ram = new Truck("Ram");
            Ram.AddPart("Body", "Steel", "Plastic", 12, 4);
            Ram.AddPart("Tires", "Rubber", "Steel", 8, 3);
            Ram.AddPart("Windows", "Steel", "Glass", 7, 4);
            factory.vehicles.Add(Ram);
            Van Odyssey = new Van("Odyssey");
            Odyssey.AddPart("Body", "Steel", "Plastic", 14, 5);
            Odyssey.AddPart("Tires", "Steel", "Rubber", 3, 8);
            Odyssey.AddPart("Windows", "Steel", "Glass", 8, 5);
            factory.vehicles.Add(Odyssey);
            string choice;
            bool end = false;
            while (end == false)
            {
                Console.WriteLine("Select the number for the option you want: \n" +
                    "1. Show factory information \n" +
                    "2. Show vehicle information \n" +
                    "3. Add a new vehicle model to build \n" +
                    "4. Recieve vehicle order \n" +
                    "5. Buy materials \n" +
                    "6. Add new material \n" +
                    "7. Add a new company to buy from\n" +
                    "8. Exit \n");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        factory.Factoryinformation();
                        Console.WriteLine();
                        break;
                    case "2":
                        string name = GetName(factory);
                        foreach (var j in factory.vehicles)
                        {
                            if (name == j.name)
                            {
                                j.ShowParts();
                            }
                        }
                        break;
                    case "3":
                        string kind = GetKind();
                        switch (kind)
                        {
                            case "car":
                                string more;
                                string test = "yes";
                                string newName = GetNewName(factory);
                                Car car = new Car(newName);
                                do
                                {
                                    Console.WriteLine("What is the name of the part:");
                                    string part = Console.ReadLine();
                                    Console.WriteLine("Enter first material for " + part);
                                    string m1 = GetMaterial(factory, null);
                                    Console.WriteLine("How many units of {0} are needed:", m1);
                                    int u1 = GetUnits();
                                    Console.WriteLine("Enter second material for " + part);
                                    string m2 = GetMaterial(factory, m1);
                                    Console.WriteLine("How many units of {0} are needed:", m2);
                                    int u2 = GetUnits();
                                    car.AddPart(part, m1, m2, u1, u2);
                                    do
                                    {
                                        Console.WriteLine("Add another part? y/n");
                                        more = Console.ReadLine();
                                        if(more == "y" || more == "n")
                                        {
                                            test = "no";
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid answer");
                                        }
                                    } while (test == "yes");
                                } while (more == "y");
                                factory.vehicles.Add(car);
                                break;
                            case "truck":
                                string more2;
                                string test2 = "yes";
                                string newName2 = GetNewName(factory);
                                Truck truck = new Truck(newName2);
                                do
                                {
                                    Console.WriteLine("What is the name of the part:");
                                    string part = Console.ReadLine();
                                    Console.WriteLine("Enter first material for " + part);
                                    string m1 = GetMaterial(factory, null);
                                    Console.WriteLine("How many units of {0} are needed:", m1);
                                    int u1 = GetUnits();
                                    Console.WriteLine("Enter second material for " + part);
                                    string m2 = GetMaterial(factory, m1);
                                    Console.WriteLine("How many units of {0} are needed:", m2);
                                    int u2 = GetUnits();
                                    truck.AddPart(part, m1, m2, u1, u2);
                                    do
                                    {
                                        Console.WriteLine("Add another part? y/n");
                                        more2 = Console.ReadLine();
                                        if (more2 == "y" || more2 == "n")
                                        {
                                            test2 = "no";
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid answer");
                                        }
                                    } while (test2 == "yes");
                                } while (more2 == "y");
                                factory.vehicles.Add(truck);
                                break;
                            case "van":
                                string more3;
                                string test3 = "yes";
                                string newName3 = GetNewName(factory);
                                Van van = new Van(newName3);
                                do
                                {
                                    Console.WriteLine("What is the name of the part:");
                                    string part = Console.ReadLine();
                                    Console.WriteLine("Enter first material for " + part);
                                    string m1 = GetMaterial(factory, null);
                                    Console.WriteLine("How many units of {0} are needed:", m1);
                                    int u1 = GetUnits();
                                    Console.WriteLine("Enter second material for " + part);
                                    string m2 = GetMaterial(factory, m1);
                                    Console.WriteLine("How many units of {0} are needed:", m2);
                                    int u2 = GetUnits();
                                    van.AddPart(part, m1, m2, u1, u2);
                                    do
                                    {
                                        Console.WriteLine("Add another part? y/n");
                                        more3 = Console.ReadLine();
                                        if (more3 == "y" || more3 == "n")
                                        {
                                            test3 = "no";
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid answer");
                                        }
                                    } while (test3 == "yes");
                                } while (more3 == "y");
                                factory.vehicles.Add(van);
                                break;
                        }
                        break;
                    case "4":
                        string type = GetType(factory);
                        int quantity = GetQuantity();
                        if (quantity == 1)
                        {
                            foreach (var j in factory.vehicles)
                            {
                                if (type == j.name)
                                {
                                    factory.Build(j);
                                }
                            }
                        }
                        else
                        {
                            foreach (var j in factory.vehicles)
                            {
                                if (type == j.name)
                                {
                                    factory.Build(j, quantity);
                                }
                            }
                        }
                        break;
                    case "5":
                        Console.WriteLine("What material should be bought:");
                        string buyMaterial = GetMaterial(factory, null);
                        Console.WriteLine("How many units of {0} should be bought:", buyMaterial);
                        int buyUnits = GetUnits();
                        foreach(var j in factory.invintory)
                        {
                            if(buyMaterial == j.name)
                            {
                                int lowest = Int32.MaxValue;
                                foreach (var l in factory.companies)
                                {
                                    if (j.name == l.item & l.price < lowest)
                                    {
                                        lowest = l.price;
                                    }
                                }
                                int buyPrice = j.Buy(buyUnits, lowest);
                                Console.WriteLine("Spent ${0} on {1} unit(s) of {2}", buyPrice, buyUnits, buyMaterial);
                                j.quantity += buyUnits;
                                Console.WriteLine();
                            }
                        }
                        break;
                    case "6":
                        string newMaterial = GetNewMaterial(factory);
                        Console.WriteLine("A company should be added to buy {0} from", newMaterial);
                        string item = GetCName(factory);
                        int price = GetCPrice(factory);
                        factory.AddMaterial(newMaterial, 0);
                        factory.AddCompany(item, newMaterial, price);
                        break;
                    case "7":
                        string n = GetCName(factory);
                        string i = GetCItem(factory);
                        int p = GetCPrice(factory);
                        factory.AddCompany(n, i, p);
                        Console.WriteLine();
                        break;
                    case "8":
                        end = true;
                        break;
                    default:
                        Console.WriteLine("Invalid selection \n");
                        break;
                }
            }
        }
        static string GetCName(Factory factory)
        {
            string test;
            string n;
            do
            {
                test = "no";
                Console.WriteLine("Enter company name: ");
                n = Console.ReadLine();
                foreach (var i in factory.companies)
                {
                    if (i.name == n)
                    {
                        test = "yes";
                        Console.WriteLine("Company name must be unique.");
                    }
                }
            } while (test == "yes");
            return n;
        }
        static string GetCItem(Factory factory)
        {
            string test;
            string i;
            do
            {
                test = "yes";
                Console.WriteLine("Enter material this company sells: ");
                i = Console.ReadLine();
                foreach (var j in factory.companies)
                {
                    if (j.item == i)
                    {
                        test = "no";
                    }
                }
                if (test == "yes")
                {
                    Console.WriteLine(i + " is not a valid material\n" +
                        "Materials the factory needs are:");
                    foreach(var j in factory.companies)
                    {
                        Console.WriteLine(j.item);
                    }
                    Console.WriteLine();
                }
            } while (test == "yes");
            return i;
        }
        static int GetCPrice(Factory factory)
        {
            string test = "yes";
            int p = 0;
            do
            {
                Console.WriteLine("What is the price per unit: ");
                int.TryParse(Console.ReadLine(), out p);
                if (p == 0)
                {
                    Console.WriteLine("Price must be a non 0 intager");
                    test = "yes";
                }
                else
                {
                    test = "no";
                }
            } while (test == "yes");
            return p;
        }
        static string GetType(Factory factory)
        {
            string name;
            string test = "yes";
            do
            {
                Console.WriteLine("What vehicle was ordered: ");
                name = Console.ReadLine();
                foreach (var i in factory.vehicles)
                {
                    if (name == i.name)
                    {
                        test = "no";
                    }
                }
                if (test == "yes")
                {
                    Console.WriteLine("That is not a vehicle this factory makes, valid choices are:");
                    factory.ShowVehicles();
                    Console.WriteLine();
                }
            } while (test == "yes");
            return name;
        }
        static int GetQuantity()
        {
            string test = "yes";
            int num = 0;
            do
            {
                Console.WriteLine("How many were ordered: ");
                int.TryParse(Console.ReadLine(), out num);
                try
                {
                    if (num < 1)
                    {
                        throw new Exception("Must use a whole number");
                    }
                    else
                    {
                        test = "no";
                    }
                }
                catch (Exception error)
                {
                    Console.WriteLine(error.Message);
                }
            } while (test == "yes");
            return num;
        }
        static string GetName(Factory factory)
        {
            string name;
            string test = "yes";
            do
            {
                Console.WriteLine("Display information for which vehicle: ");
                name = Console.ReadLine();
                foreach (var i in factory.vehicles)
                {
                    if (name == i.name)
                    {
                        test = "no";
                    }
                }
                if (test == "yes")
                {
                    Console.WriteLine("That is not a vehicle this factory makes, valid choices are:");
                    factory.ShowVehicles();
                    Console.WriteLine();
                }
            } while (test == "yes");
            return name;
        }
        static string GetKind()
        {
            string kind;
            string test = "yes";
            do
            {
                Console.WriteLine("What kind of vehicle is this:");
                kind = Console.ReadLine();
                if(kind == "car" || kind == "truck" || kind == "van")
                {
                    test = "no";
                }
                else
                {
                    Console.WriteLine("Vehicle must be either a car, truck, or van \n");
                }
            } while (test == "yes");
            return kind;
        }
        static string GetNewName(Factory factory)
        {
            string name;
            string test;
            do
            {
                test = "no";
                Console.WriteLine("What is the name of this vehicle:");
                name = Console.ReadLine();
                foreach(var i in factory.vehicles)
                {
                    if(name == i.name)
                    {
                        test = "yes";
                        Console.WriteLine("Duplicate names are not allowed \n");
                    }
                }
            } while (test == "yes");
            return name;
        }
        static string GetMaterial(Factory factory, string used)
        {
            string m;
            string test = "yes";
            do
            {
                m = Console.ReadLine();
                foreach(var i in factory.invintory)
                {
                    if(m == i.name)
                    {
                        test = "no";
                    }
                }
                if(test == "yes")
                {
                    Console.WriteLine("Unrecogzied material, valid options are:");
                    foreach (var i in factory.invintory)
                    {
                        Console.WriteLine(i.name);
                    }
                    Console.WriteLine();
                }
                else if(m == used)
                {
                    Console.WriteLine("Can't use the same material again, valid options are:");
                    foreach (var i in factory.invintory)
                    {
                        if(i.name != used)
                        {
                            Console.WriteLine(i.name);
                        }
                    }
                    Console.WriteLine();
                    test = "yes";
                }
            } while (test == "yes");
            return m;
        }
        static int GetUnits()
        {
            int units = 0;
            string test = "yes";
            do
            {
                int.TryParse(Console.ReadLine(), out units);
                if (units < 1)
                {
                    Console.WriteLine("Units must be a whole number");
                    test = "yes";
                }
                else
                {
                    test = "no";
                }
            } while (test == "yes");
            return units;
        }
        static string GetNewMaterial(Factory factory)
        {
            string material;
            string test;
            do
            {
                test = "no";
                Console.WriteLine("Add what material:");
                material = Console.ReadLine();
                foreach(var i in factory.invintory)
                {
                    if(material == i.name)
                    {
                        test = "yes";
                        Console.WriteLine("That material is already being used. \n");
                    }
                }
            } while (test == "yes");
            return material;
        }
    }
}