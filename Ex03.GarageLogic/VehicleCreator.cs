using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class VehicleCreator
    {
        public static Vehicle CreateVehicle(string i_VehicleType)
        {
            Vehicle newVehicle = null;

            switch(i_VehicleType)
            {
                case "FuelCar":
                    newVehicle = new FuelCar();
                    break;
                case "ElectricCar":
                    newVehicle = new ElectricCar();
                    break;
                case "FuelMotorcycle":
                    newVehicle = new FuelMotorcycle();
                    break;
                case "ElectricMotorcycle":
                    newVehicle = new ElectricMotorcycle();
                    break;
                case "Truck":
                    newVehicle = new Truck();
                    break;
            }

            return newVehicle;
        }

        public static List<string> SupportedTypes
        {
            get{return new List<string> { "FuelCar", "ElectricCar", "FuelMotorcycle", "ElectricMotorcycle", "Truck" }; }
        }
    }
}
