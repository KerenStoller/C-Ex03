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

    public void AddVehicleFromDetails(List<string> i_DetailsAboutCar)
    {
        //TODO: make sure the i_DetailsAboutCar is long enoguh
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

    public List<string> GetLicenseIdOfAllVehiclesInGarage(Vehicle.eVehicleState? i_FilterByState = null)
    {
        List<string> listToReturn;
        
        if (i_FilterByState == null)
        {
            listToReturn = m_Vehicles.Keys.ToList();;
        }
        else
        {
            listToReturn = new List<string>();
            
            foreach (KeyValuePair<string, Vehicle> pair in m_Vehicles)
            {
                if (pair.Value.VehicleState == i_FilterByState)
                {
                    listToReturn.Add(pair.Key);
                }
            }
        }
        
        return  listToReturn;
    }

    public void ChangeVehicleState(string i_LicenseId, Vehicle.eVehicleState i_NewState)
    {
        validateVehicleInGarage(i_LicenseId);
        m_Vehicles[i_LicenseId].VehicleState = i_NewState;
    }

    public void InflateTires(string i_LicenseId)
    {
        validateVehicleInGarage(i_LicenseId);
        m_Vehicles[i_LicenseId].InflateTires();
    }

    public void FillTank(string i_LicenseId, EnergySystem.FuelSystem.eFuelType i_FuelType, float i_AmountOfFuelToAdd)
    {
        validateVehicleInGarage(i_LicenseId);
        Vehicle vehicle = m_Vehicles[i_LicenseId];
        vehicle.FillTank(i_FuelType, i_AmountOfFuelToAdd);
    }

    public void ChargeBattery(string i_LicenseId, float i_TimeToChargeInMinutes)
    {
        validateVehicleInGarage(i_LicenseId);
        Vehicle vehicle = m_Vehicles[i_LicenseId];
        vehicle.ChargeBattery(i_TimeToChargeInMinutes);
    }

    public List<string> GetDetails(string i_LicenseId)
    {
        validateVehicleInGarage(i_LicenseId);
        Vehicle vehicle = m_Vehicles[i_LicenseId];
        return vehicle.GetDetails();
    }
}