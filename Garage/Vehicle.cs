using System;
using System.Collections.Generic;
using System.Text;

namespace Garage
{
   
    class Vehicle
    {
        private string REGNO; // Must be unique
        public string ReGNO { get => REGNO; set => REGNO = value; }

        private string color;
        public string Color
        {
            get => color;
            set => color = value;
        }

        private int wheels;
        public int Wheels { get => wheels; set => wheels = value; }
        public bool ReGNo { get; internal set; }

        public Vehicle(string regno, string colour, int wheel)
        {
            REGNO = regno;
            color = colour;
            wheels = wheel;
        }
    }

    class Car : Vehicle
    {
        int seats;
        public int Seats { get => seats; set => seats = value; }

        public Car(string regno, string colour, int wheel, int seat) : base(regno, colour, wheel)
        {
            seats = seat;
        }
    }

    class Airplane : Vehicle
    {
        private bool isJet;
        public bool IsJet { get => isJet; set => isJet = value; }

        public Airplane(string regno, string colour, int wheel, bool jet) : base(regno, colour, wheel)
        {
            isJet = jet;
        }
    }

    class Motorcycle : Vehicle
    {
        private bool isElectric;
        public bool IsElectric { get => isElectric; set => isElectric = value; }

        public Motorcycle(string regno, string colour, int wheel, bool fueltype) : base(regno, colour, wheel)
        {
            isElectric = fueltype;
        }
    }

    class Bus : Vehicle
    {
        private bool SchoolBus;
        public bool Schoolbus { get => SchoolBus; set => SchoolBus = value; }

        public Bus(string regno, string colour, int wheel, bool Bustype) : base(regno, colour, wheel)
        {
            SchoolBus = Bustype;
        }
    }

    class Boat : Vehicle
    {
        private bool gasoline;
        public bool Gasoline { get => gasoline; set => gasoline = value; }

        public Boat(string regno, string colour, int wheel, bool hasGasoline) : base(regno, colour, wheel)
        {
            gasoline = hasGasoline;
        }
    }
}


