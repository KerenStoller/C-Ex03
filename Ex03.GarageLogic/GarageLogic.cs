using Ex03.GarageLogic.EnergySystem;

namespace Ex03.GarageLogic;

public class GarageLogic
{
    private Dictionary<string, Vehicle> m_Vehicles;
    private GarageDb m_GarageDb;
    
    public GarageLogic()
    {
        m_GarageDb = new GarageDb();
        m_Vehicles = new Dictionary<string, Vehicle>();
        //addVehiclesFromDb(); should be called from the user interface
    }

    public void initialVehicleCreation(List<string> i_initialDetails)
    {
        // TODO
    }
    
    public void AddVehicleFromDetails(List<string> i_DetailsAboutCar)
    {
        //TODO: make sure the i_DetailsAboutCar is long enoguh 
        //TDOD: need to add owner name and phone number
        Vehicle vehicle;
        string vehicleType = i_DetailsAboutCar[0];
        string licenseId = i_DetailsAboutCar[1];

        validateVehicleNotInGarage(licenseId);
        if (!VehicleCreator.SupportedTypes.Contains(vehicleType))
        {
            throw new ArgumentException($"{vehicleType} is not supported");
        }
        
        string modelName = i_DetailsAboutCar[2];
        vehicle = VehicleCreator.CreateVehicle(vehicleType, licenseId, modelName);
        string tireModel = i_DetailsAboutCar[4];
        float energyPercentage, currentAirPressure, currentEnergyAmount;
            
        try
        {
            energyPercentage = float.Parse(i_DetailsAboutCar[3]);
            currentAirPressure = float.Parse(i_DetailsAboutCar[5]);
            currentEnergyAmount = float.Parse(i_DetailsAboutCar[6]);
        }
        catch (FormatException e)
        {
            throw new FormatException($"Invalid number format in vehicle details: {e.Message}", e);
        }
        
        vehicle.AddGeneralDetails(energyPercentage, currentEnergyAmount, tireModel, currentAirPressure);
        vehicle.AddSpecificDetails(i_DetailsAboutCar[7], i_DetailsAboutCar[8]);
        addVehicle(vehicle);
    }
    
    public void AddVehiclesFromDb()
    {
        foreach (List<string> lineFromFile in m_GarageDb.m_DbVehicles)
        {
            AddVehicleFromDetails(lineFromFile);
        }
    }

    private void validateVehicleInGarage(string i_LicenseId)
    {
        if (!IsVehicleInGarage(i_LicenseId))
        {
            throw new ArgumentException("Vehicle is not in garage");
        }
    }
    
    private void validateVehicleNotInGarage(string i_LicenseId)
    {
        if (IsVehicleInGarage(i_LicenseId))
        {
            throw new ArgumentException("Vehicle is already in the garage");
        }
    }
    
    public bool IsVehicleInGarage(string i_LicenseId)
    {
        return m_Vehicles.ContainsKey(i_LicenseId);
    }

    public void WorkOnVehicle(string i_LicenseId)
    {
        validateVehicleInGarage(i_LicenseId);
        Vehicle vehicle = m_Vehicles[i_LicenseId];
        vehicle.VehicleState = Vehicle.eVehicleState.InRepair;
    }
    
    private void addVehicle(Vehicle i_Vehicle)
    {
        validateVehicleNotInGarage(i_Vehicle.r_LicenseId);
        m_Vehicles.Add(i_Vehicle.r_LicenseId, i_Vehicle);
    }

    public List<string> GetLicenseIdOfAllVehiclesInGarage(string? i_FilterByState)
    {
        List<string> listToReturn;
        
        if (i_FilterByState == null ||!Enum.TryParse(i_FilterByState, ignoreCase: true, out Vehicle.eVehicleState FilterByState))
        {
            listToReturn = m_Vehicles.Keys.ToList();;
        }
        else
        {
            
            listToReturn = new List<string>();
            
            foreach (KeyValuePair<string, Vehicle> pair in m_Vehicles)
            {
                if (pair.Value.VehicleState == FilterByState)
                {
                    listToReturn.Add(pair.Key);
                }
            }
        }
        
        return  listToReturn;
    }

    public void ChangeVehicleState(string i_LicenseId, string i_NewStateInString)
    {

        bool validState = Enum.TryParse(i_NewStateInString, ignoreCase: true, out Vehicle.eVehicleState i_NewState);

        if (!validState)
        {
            throw new ArgumentException("Invalid vehicle state");
        }

        validateVehicleInGarage(i_LicenseId);
        m_Vehicles[i_LicenseId].VehicleState = i_NewState;
    }

    public void InflateTires(string i_LicenseId)
    {
        validateVehicleInGarage(i_LicenseId);
        m_Vehicles[i_LicenseId].InflateTires();
    }

    public void FillTank(string i_LicenseId, string i_FuelTypeInString, string i_AmountOfFuelToAddInString)
    {

        bool validInput = float.TryParse(i_AmountOfFuelToAddInString, out float i_AmountOfFuelToAdd);
        if (!validInput)
        {
            throw new FormatException("Invalid amount of fuel to add, must be a number");
        }

        bool validFuelType = Enum.TryParse(i_FuelTypeInString, ignoreCase:true, out FuelSystem.eFuelType i_FuelType);

        if (!validFuelType)
        {
            throw new ArgumentException("Invalid fuel type");
        }

        validateVehicleInGarage(i_LicenseId);
        Vehicle vehicle = m_Vehicles[i_LicenseId];
        vehicle.FillTank(i_FuelType, i_AmountOfFuelToAdd); // TODO: need to add value range exception
    }


    // example of input handling in the logic using exception
    public void ChargeBattery(string i_LicenseId, string i_TimeToChargeInMinutes)
    {
        bool validInput = float.TryParse(i_TimeToChargeInMinutes, out float timeToCharge);

        if(!validInput)
        {
            throw new FormatException("Invalid time to charge, must be a number");
        }

        validateVehicleInGarage(i_LicenseId);
        Vehicle vehicle = m_Vehicles[i_LicenseId];
        vehicle.ChargeBattery(timeToCharge); // TODO: need to add value range exception
    }

    public List<string> GetDetails(string i_LicenseId)
    {
        validateVehicleInGarage(i_LicenseId);
        Vehicle vehicle = m_Vehicles[i_LicenseId];
        return vehicle.GetDetails();
    }
    
    public bool isCar(string i_LicenseId)
    {
        validateVehicleInGarage(i_LicenseId);
        Vehicle vehicle = m_Vehicles[i_LicenseId];
        return vehicle is Car;
    }
    
    public bool isTruck(string i_LicenseId)
    {
        validateVehicleInGarage(i_LicenseId);
        Vehicle vehicle = m_Vehicles[i_LicenseId];
        return vehicle is Truck;
    }
    
    public bool isMotorcycle(string i_LicenseId)
    {
        validateVehicleInGarage(i_LicenseId);
        Vehicle vehicle = m_Vehicles[i_LicenseId];
        return vehicle is Motorcycle;
    }
    public bool isElectric(string i_LicenseId)
    {
        validateVehicleInGarage(i_LicenseId);
        Vehicle vehicle = m_Vehicles[i_LicenseId];
        return vehicle.IsElectric();
    }
    
    public int getNumberOfTires(string i_LicenseId)
    {
        validateVehicleInGarage(i_LicenseId);
        Vehicle vehicle = m_Vehicles[i_LicenseId];
        return vehicle.NumberOfTires();
    }
    
}