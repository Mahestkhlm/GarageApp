using System;
using System.Collections.Generic;
using System.Linq;

namespace Garage
{
    class Program
    {
        static void Main(string[] args)
        {
            GarageApplication theApplication = new GarageApplication();
            theApplication.Run();
        }
        class GarageApplication
        {
            readonly GarageHandler theHandler = new GarageHandler();

            int capacity;
            public void Run()
            {
                Console.WriteLine("WELCOME TO THE GARAGE APPLICATION");
                Console.WriteLine("************************************");

                while (true)
                {
                    Console.WriteLine("ENTER ANY ONE OF THE FOLLOWING OPTIONS: ");
                    Console.WriteLine("********************************************************************************");
                    Console.WriteLine("\n1. CREATE A GARAGE");
                    Console.WriteLine("\n2. PARK A VEHICLE IN THE GARAGE");
                    Console.WriteLine("\n3. LIST THE VEHICLE IN THE GARAGE");
                    Console.WriteLine("\n4. REMOVE THE VEHICLE FROM THE GARAGE");
                    Console.WriteLine("\n5. DISPLAY THE VEHICLE DETAILS");
                    Console.WriteLine("\n6. SEARCH THE VEHICLE WITH INPUTED PROPERTIES")
                    Console.WriteLine("\n8. EXIT APP");



                    var inString = Console.ReadLine();
                    if (inString.Length == 0)
                    {
                        Console.WriteLine("EXIT APPLICATION!");
                        break;
                    }

                    switch (inString[0])
                    {
                        case '1':
                            BuildGarage();
                            break;
                        case '2':
                            ParkVehicle();
                            break;
                        case '3':
                            ListVehicles();
                            break;
                        case '4':
                            RemoveVehicle();
                            break;

                        case '5':
                            ShowVehicleData();
                            break;
                        case '6':
                            VehiclesWithProperties();
                            break;
                        case '0':
                            Console.WriteLine("EXIT APPLICATION!");
                            break;
                        default:
                            break;
                    }
                    if (inString[0] == '0') break;
                }
            }

            //if case:1 //buildgarage() builds garage withín the given limit of 1-100,if capacity entered is  greater than 100 or less than 1,
            //diaplys an error message ,else creates a parking space within the user inputed number..
            private void BuildGarage()
            {
                Console.Clear();
                Console.WriteLine("INFORMATION:THE GARAGE HAS A LIMIT OF (1-100)");
                var count = Console.ReadLine();
                if (int.TryParse(count, out capacity))
                {
                    if (capacity > 100)
                    {
                        Console.WriteLine("PLEASE ENTER ANYTHING BETWEEN 1 TO 100\n");
                    }
                    else if (capacity < 1)
                    {
                        Console.WriteLine("ENTER ATLEAST 1 NUMBER TO CAPACITY LIMIT\n");
                    }
                    else
                    {
                        theHandler.CreateGarage(capacity);
                        Console.WriteLine($"THE GARAGE IS AVAILABLE WITH {capacity} SLOTS.\n");
                    }
                }
                else Console.WriteLine("INVALID ENTRY!!\n");
            }
            //if case 2: parkVehicle()user should interpret yer or no to park vehicle,if not,thehandler is intialized
            //user can enter the vehicle details

            private void ParkVehicle()
            {
                string input;
                Vehicle localVehicle;
                bool yesOrNo;

                static bool askYesNo(string ques, out bool answer)
                {

                    Console.WriteLine(ques);
                    string Str = Console.ReadLine().ToLower();
                    if (Str == "y" || Str == "yes")
                    {
                        answer = true;
                        return true;
                    }
                    else if (Str == "n" || Str == "no")
                    {
                        answer = false;
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Could not interpret the answer as yes or no.\n");
                        answer = false; // Dummy value
                        return false;
                    }
                }

                if (!theHandler.Initialized())
                {

                    Console.WriteLine("The garage must be built before it can be used\n");
                    return;
                }
                Console.Clear();
                Console.WriteLine("WELCOME TO PARK YOUR VEHICLE!");
                Console.WriteLine("------------------------------");
                Console.WriteLine("ENTER THE REGISTRATION NUMBER OF THE VEHICLE:");
                string regno = Console.ReadLine().ToUpper();
                Console.WriteLine("ENTER THE VEHICLE COLOR:");
                string color = Console.ReadLine();
                Console.WriteLine("ENTER THE NUMBER OF WHEELS ");
                input = Console.ReadLine();
                if (!int.TryParse(input, out int wheels))
                {
                    Console.WriteLine("INVALID NUMBER!");
                    Console.WriteLine("SORRY, YOUR VEHICLE CANNOT BE PARKED!");
                    return;
                }
                Console.WriteLine("ENTER THE VEHICLE TYPE TO BE PARKED");
                Console.WriteLine("\nAeroplane, Bus, Boat, Car or MOC?");

                input = Console.ReadLine().ToLower();
                if (input == "aeroplane" || input == "airplane" || input == "plane")
                {
                    if (!askYesNo("Does the plane have jet engines?", out yesOrNo))

                        return;

                    localVehicle = new Airplane(regno, color, wheels, yesOrNo);
                }
                else if (input == "moc" || input == "motorcycle")
                {
                    if (!askYesNo("IS IT AN ELECTRIC MOTORCYCLE?", out yesOrNo))

                        return;

                    localVehicle = new Motorcycle(regno, color, wheels, yesOrNo);
                }
                //if user enters bus, if loop checks for school bus
                else if (input == "bus")
                {
                    if (!askYesNo("IS IT A SCHOOLBUS? ", out yesOrNo))

                        return;

                    localVehicle = new Bus(regno, color, wheels, yesOrNo);
                }
                //if user eneters boat to be parked ,asks for the properties.
                else if (input == "boat")
                {
                    if (!askYesNo("ENTER yes IF IT IS A GASOLINE", out yesOrNo))

                        return;

                    localVehicle = new Boat(regno, color, wheels, yesOrNo);
                }
                else if (input == "car")
                {
                    Console.WriteLine("TOTAL NUMBE ROF SEATS:");
                    input = Console.ReadLine();
                    if (!int.TryParse(input, out int seats))
                    {
                        Console.WriteLine("INVALID ENTRY");
                        Console.WriteLine("SORRY, YOU CANNOT PARK THE VEHICLE!\n");
                        return;
                    }
                    localVehicle = new Car(regno, color, wheels, seats);
                }
                else
                {
                    Console.WriteLine("ERROR WHILE PARKING!\n PLEASE ENTER THE CORRECT VEHICLE TYPE!!!");
                    return;
                }
                theHandler.AddVehicle(localVehicle);
                Console.WriteLine($"parking vehicle{localVehicle.ReGNO}.\n");
            }

            private void ShowVehicleData()
            {
                if (!theHandler.Initialized())
                {
                    Console.WriteLine("The garage must be built before it can be used\n");
                    return;
                }
                Console.WriteLine("ENTER THE REGISTRATION NUMBER OF THE VEHICLE:");
                Console.WriteLine("*************************************************");

                string regno = Console.ReadLine().ToUpper();
                var vehicle = theHandler.FindVehicle(regno);
                if (vehicle == null)
                {
                    Console.WriteLine("THE VEHICLE CANNOT BE FOUND!");
                    Console.WriteLine("ENTER THE CORRECT REGISTRATION NUMBER!");
                    return;
                }
                Console.WriteLine($"The vehicle Registration number{regno}:");
                Console.WriteLine($"Vehicle Color: {vehicle.Color}");
                Console.WriteLine($"Number of wheels: {vehicle.Wheels}\n");
            }

            private void RemoveVehicle()
            {
                if (!theHandler.Initialized())
                {
                    Console.WriteLine("THE GARAGE IS NOT OPEN RIGHTNOW,PLEASE CHOOSE OPTION 1 TO OPEN\n");
                    return;
                }
                Console.WriteLine("TO UNPARK,ENTER THE VEHICLE REGISTRATION NUMBER:{regno}");
                string inString = Console.ReadLine().ToUpper();
                if (theHandler.RemoveVehicle(inString))
                    Console.WriteLine($"VEHICLE {inString} SUCCESFULLY REMOVED.\n");
                else
                    Console.WriteLine($"Vehicle {inString}is cannot be found in garage.\n");
            }


            private void ListVehicles()
            {
                if (!theHandler.Initialized())
                {
                    Console.WriteLine("PLEASE BUILD THE GARAGE FIRST!");
                    return;
                }
                IEnumerable<Vehicle> localGarage = theHandler.ListVehicles();

                if (localGarage.Count() == 0)
                    Console.WriteLine("NO VEHICLES FOUND!");
                else
                {
                    Console.WriteLine("THE LIST OF ALL THE PARKED VEHICLES IN THE GARAGE");
                    foreach (Vehicle vehicle in localGarage)
                    {
                        Console.WriteLine(vehicle.ReGNO);
                    }
                }
                Console.WriteLine();
            }


            private void VehiclesWithProperties()
            {
                 if (!theHandler.Initialized())
                {  
                    Console.Clear(); 
                    Console.WriteLine("THE GARAGE IS EMPTY,PLEASE CHOOSE OPTION 1 TO OPEN\n");
                    return;
                }
                Console.WriteLine("ENTER THE VALUE OF THE SPECIFIC PROPERTY:");
                // Console.WriteLine("Empty line (Return) is interpreted as accepting any value.");
                Console.WriteLine("ENTER THE COLOR OF THE VEHICLE:");
                string color = Console.ReadLine();
                Console.WriteLine("NUMBER OF WHEELS OF THE VEHICLE:");
                if (!int.TryParse(Console.ReadLine(), out int wheels)) wheels = -1;
                var vehicles = theHandler.GetCharacterizedVehicles(wheels, color);
                foreach (Vehicle v in vehicles)
                {
                    Console.WriteLine(v.ReGNO);
                }
            }
           
        }
    }

}

