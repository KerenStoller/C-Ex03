namespace Ex03.GarageLogic;

public class GarageLogic
{
    private Dictionary<string, Vehicle> m_Vehicles;
    private GarageDB m_GarageDB;
    
    public GarageLogic()
    {
        m_GarageDB = new GarageDB();
        m_Vehicles = new Dictionary<string, Vehicle>();
        addVehiclesFromDb();
    }

    public void AddVehicleFromDetails(List<string> i_DetailsAboutCar)
    {
        //TODO: make sure the i_DetailsAboutCar is long enoguh
        Vehicle vehicle;
        string vehicleType = i_DetailsAboutCar[0];
        string licenseID = i_DetailsAboutCar[1];

        validateVehicleNotInGarage(licenseID);
        if (!VehicleCreator.SupportedTypes.Contains(vehicleType))
        {
            throw new ArgumentException($"{vehicleType} is not supported");
        }
        
        string modelName = i_DetailsAboutCar[2];
        vehicle = VehicleCreator.CreateVehicle(vehicleType, licenseID, modelName);
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
        AddVehicle(vehicle);
    }
    
    private void addVehiclesFromDb()
    {
        foreach (List<string> lineFromFile in m_GarageDB.m_dbVehicles)
        {
            AddVehicleFromDetails(lineFromFile);
        }
    }

    private void validateVehicleInGarage(string i_LicenseID)
    {
        if (!IsVehicleInGarage(i_LicenseID))
        {
            throw new ArgumentException("Vehicle is not in garage");
        }
    }
    
    private void validateVehicleNotInGarage(string i_LicenseID)
    {
        if (IsVehicleInGarage(i_LicenseID))
        {
            throw new ArgumentException("Vehicle is already in the garage");
        }
    }
    
    public bool IsVehicleInGarage(string i_LicenseID)
    {
        return m_Vehicles.ContainsKey(i_LicenseID);
    }

    public void WorkOnVehicle(string i_LicenseID)
    {
        validateVehicleInGarage(i_LicenseID);
        Vehicle vehicle = m_Vehicles[i_LicenseID];
        vehicle.VehicleState = Vehicle.e_VehicleState.InRepair;
    }
    
    private void AddVehicle(Vehicle vehicle)
    {
        validateVehicleNotInGarage(vehicle.r_LicenseID);
        m_Vehicles.Add(vehicle.r_LicenseID, vehicle);
    }

    public List<string> GetLicenseIDOfAllVehiclesInGarage(Vehicle.e_VehicleState? i_FilterByState = null)
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

    public void ChangeVehicleState(string i_LicenseID, Vehicle.e_VehicleState i_NewState)
    {
        validateVehicleInGarage(i_LicenseID);
        m_Vehicles[i_LicenseID].VehicleState = i_NewState;
    }

    public void InflateTires(string i_LicenseID)
    {
        validateVehicleInGarage(i_LicenseID);
        m_Vehicles[i_LicenseID].InflateTires();
    }

    public void FillTank(string i_LicenseID, EnergySystem.FuelSystem.e_FuelType i_FuelType, float i_AmountOfFuelToAdd)
    {
        validateVehicleInGarage(i_LicenseID);
        Vehicle vehicle = m_Vehicles[i_LicenseID];
        vehicle.FillTank(i_FuelType, i_AmountOfFuelToAdd);
    }

    public void ChargeBattery(string i_LicenseID, float i_TimeToChargeInMinutes)
    {
        validateVehicleInGarage(i_LicenseID);
        Vehicle vehicle = m_Vehicles[i_LicenseID];
        vehicle.ChargeBattery(i_TimeToChargeInMinutes);
    }

    public List<string> GetDetails(string i_LicenseID)
    {
        validateVehicleInGarage(i_LicenseID);
        Vehicle vehicle = m_Vehicles[i_LicenseID];
        return vehicle.GetDetails();
    }
}