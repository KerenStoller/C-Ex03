namespace Ex03.GarageLogic;
using Ex03.GarageLogic.GarageDB;

public class GarageLogic
{
    private Dictionary<string, Vehicle> m_Vehicles;
    private GarageDb m_GarageDb;
    private const bool v_SetAllTiresAtOnce = true;

    public enum eEnumOptions
    {
        CarColor, 
        CarDoors, 
        MotorcycleLicense,
        FuelType,
        VehicleState,
        SupportedTypes
    }
    
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

    public void UpdateVehicle(string i_LicenseId, List<string> i_DetailsAboutVehicle, bool i_AreAllTiresTheSame)
    {
        validateVehicleInGarage(i_LicenseId);
        Vehicle vehicle = m_Vehicles[i_LicenseId];
        int numberOfTires = vehicle.NumberOfTires();
        int index = 0;
        List<KeyValuePair<string, float>> listOfTires = new List<KeyValuePair<string, float>>();
        string tireModel, ownerName, ownerPhone, detail1, detail2;
        float energyPercentage, currentAirPressure;
            
        try
        {
            energyPercentage = float.Parse(i_DetailsAboutVehicle[index++]);
            
            for (int i = 0; i < numberOfTires; i++)
            {
                tireModel = i_DetailsAboutVehicle[index++];
                currentAirPressure = float.Parse(i_DetailsAboutVehicle[index++]);

                if(i_AreAllTiresTheSame)
                {
                    vehicle.AddGeneralTires(tireModel, currentAirPressure);
                    break;
                }
                
                listOfTires.Add(new KeyValuePair<string, float>(tireModel, currentAirPressure));
            }
        }
        catch (FormatException e)
        {
            m_Vehicles.Remove(i_LicenseId);
            throw new FormatException($"Invalid number format in vehicle details: {e.Message}", e);
        }

        if(!i_AreAllTiresTheSame)
        {
            vehicle.AddSpecificTires(listOfTires);
        }
            
        ownerName = i_DetailsAboutVehicle[index++];
        ownerPhone = i_DetailsAboutVehicle[index++];
        detail1 = i_DetailsAboutVehicle[index++];
        detail2 = i_DetailsAboutVehicle[index];

        try
        {
            vehicle.AddGeneralDetails(ownerName, ownerPhone, energyPercentage);
            vehicle.AddSpecificDetails(detail1, detail2);
        }
        catch
        {
            m_Vehicles.Remove(i_LicenseId);
            throw;
        }
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
            UpdateVehicle(licenseId, updateDetails, v_SetAllTiresAtOnce);
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

    public List<string> GetLicenseIdOfAllVehiclesInGarage(string i_FilterByState)
    {
        List<string> listToReturn = new List<string>();
        
        if (i_FilterByState == "")
        {
            listToReturn = m_Vehicles.Keys.ToList();
        }
        else
        {
            Vehicle.eVehicleState filterState = Vehicle.validateState(i_FilterByState);
            
            foreach (KeyValuePair<string, Vehicle> pair in m_Vehicles)
            {
                if (pair.Value.VehicleState == filterState)
                {
                    listToReturn.Add(pair.Key);
                }
            }
        }
        
        return  listToReturn;
    }

    public void ChangeVehicleState(string i_LicenseId, string i_NewStateInString)
    {
        validateVehicleInGarage(i_LicenseId);
        m_Vehicles[i_LicenseId].UpdateState(i_NewStateInString);
    }

    public void InflateTires(string i_LicenseId)
    {
        validateVehicleInGarage(i_LicenseId);
        m_Vehicles[i_LicenseId].InflateTires();
    }

    public void FillTank(string i_LicenseId, string i_FuelType, float i_AmountOfFuelToAdd)
    {
        validateVehicleInGarage(i_LicenseId);
        m_Vehicles[i_LicenseId].FillTank(i_FuelType, i_AmountOfFuelToAdd);
    }
    
    public void ChargeBattery(string i_LicenseId, float i_TimeToChargeInMinutes)
    {
        validateVehicleInGarage(i_LicenseId);
        m_Vehicles[i_LicenseId].ChargeBattery(i_TimeToChargeInMinutes);
    }

    public Dictionary<string, string> GetDetails(string i_LicenseId)
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
    
    public static List<string> GetEnumOptions(eEnumOptions i_EnumOption)
    {
        List<string> listToReturn = new List<string>();
        
        switch (i_EnumOption)
        {
            case eEnumOptions.CarColor:
                listToReturn = Enum.GetNames(typeof(Car.eColor)).ToList();
                break;
            case eEnumOptions.CarDoors:
                listToReturn = Enum.GetNames(typeof(Car.eNumberOfDoors)).ToList();
                break;
            case eEnumOptions.MotorcycleLicense:
                listToReturn = Enum.GetNames(typeof(Motorcycle.eLicenseType)).ToList();
                break;
            case eEnumOptions.FuelType:
                listToReturn = Enum.GetNames(typeof(FuelSystem.eFuelType)).ToList();
                break;
            case eEnumOptions.VehicleState:
                listToReturn = Enum.GetNames(typeof(Vehicle.eVehicleState)).ToList();
                break;
            case eEnumOptions.SupportedTypes:
                listToReturn = VehicleCreator.SupportedTypes;
                break;
        }
        
        return listToReturn;
    }
}