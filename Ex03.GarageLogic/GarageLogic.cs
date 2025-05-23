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
    }

    public void CreateVehicle(List<string> i_DetailsAboutVehicle)
    {
        Vehicle vehicle;
        string vehicleType = i_DetailsAboutVehicle[0];
        string licenseId = i_DetailsAboutVehicle[1];
        
        validateVehicleNotInGarage(licenseId);
        if (!VehicleCreator.SupportedTypes.Contains(vehicleType))
        {
            throw new ArgumentException($"{vehicleType} is not supported");
        }
        
        string modelName = i_DetailsAboutVehicle[2];
        vehicle = VehicleCreator.CreateVehicle(vehicleType, licenseId, modelName);
        m_Vehicles.Add(licenseId, vehicle);
    }

    public void UpdateVehicle(string i_LicenseID, List<string> i_DetailsAboutVehicle)
    {
        validateVehicleInGarage(i_LicenseID);
        Vehicle vehicle = m_Vehicles[i_LicenseID];
        string tireModel = i_DetailsAboutVehicle[1];
        string ownerName = i_DetailsAboutVehicle[3];
        string ownerPhone = i_DetailsAboutVehicle[4];
        string detail1 = i_DetailsAboutVehicle[5];
        string detail2 = i_DetailsAboutVehicle[6];
        float energyPercentage, currentAirPressure;
            
        try
        {
            energyPercentage = float.Parse(i_DetailsAboutVehicle[0]);
            currentAirPressure = float.Parse(i_DetailsAboutVehicle[2]);
        }
        catch (FormatException e)
        {
            throw new FormatException($"Invalid number format in vehicle details: {e.Message}", e);
        }

        vehicle.AddGeneralDetails(ownerName, ownerPhone, energyPercentage);
        vehicle.AddGeneralTires(tireModel, currentAirPressure);
        vehicle.AddSpecificDetails(detail1, detail2);
    }
    
    public void AddVehiclesFromDb()
    {
        foreach (List<string> lineFromFile in m_GarageDb.m_DbVehicles)
        {
            List<string> initialDetails = new List<string>();
            List<string> updateDetails = new List<string>();
            string licenseId = lineFromFile[1];

            for (int i = 0; i < 3; i++)
            {
                initialDetails.Add(lineFromFile[i]);
            }
            
            for (int i = 3; i < lineFromFile.Count; i++)
            {
                updateDetails.Add(lineFromFile[i]);
            }
            
            CreateVehicle(initialDetails);
            UpdateVehicle(licenseId, updateDetails);
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
        m_Vehicles[i_LicenseId].VehicleState = Vehicle.eVehicleState.InRepair;
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

    public void FillTank(string i_LicenseId, string i_FuelType, float i_AmountOfFuelToAdd)
    {
        //TODO: check valid number in UI
        validateVehicleInGarage(i_LicenseId);
        m_Vehicles[i_LicenseId].FillTank(i_FuelType, i_AmountOfFuelToAdd);
    }
    
    public void ChargeBattery(string i_LicenseId, float i_TimeToChargeInMinutes)
    {
        validateVehicleInGarage(i_LicenseId);
        m_Vehicles[i_LicenseId].ChargeBattery(i_TimeToChargeInMinutes);
    }

    public List<string> GetDetails(string i_LicenseId)
    {
        validateVehicleInGarage(i_LicenseId);
        return m_Vehicles[i_LicenseId].GetDetails();
    }
    
    public bool IsCar(string i_LicenseId)
    {
        validateVehicleInGarage(i_LicenseId);
        return m_Vehicles[i_LicenseId] is Car;
    }
    
    public bool IsTruck(string i_LicenseId)
    {
        validateVehicleInGarage(i_LicenseId);
        return m_Vehicles[i_LicenseId] is Truck;
    }
    
    public bool IsMotorcycle(string i_LicenseId)
    {
        validateVehicleInGarage(i_LicenseId);
        return m_Vehicles[i_LicenseId] is Motorcycle;
    }
    public bool IsElectric(string i_LicenseId)
    {
        validateVehicleInGarage(i_LicenseId);
        return m_Vehicles[i_LicenseId].IsElectric();
    }
    
    public int GetNumberOfTires(string i_LicenseId)
    {
        validateVehicleInGarage(i_LicenseId);
        return m_Vehicles[i_LicenseId].NumberOfTires();
    }
}