namespace Ex03.GarageLogic;

public class GarageLogic
{
    private Dictionary<string, Vehicle> m_Vehicles;
    private GarageDB m_GarageDB;
    
    public GarageLogic()
    {
        m_GarageDB = new GarageDB(m_Vehicles);
    }

    public bool IsVehicleInGarage(string i_LicenseID)
    {
        return m_Vehicles.ContainsKey(i_LicenseID);
    }

    public void WorkOnVehicle(string i_LicenseID)
    {
        if (IsVehicleInGarage(i_LicenseID))
        {
            Vehicle vehicle = m_Vehicles[i_LicenseID];
            vehicle.VehicleState = Vehicle.e_VehicleState.InRepair;
        }
        else
        {
            //TODO: throw exc
        }
    }
    
    public void AddVehicle(Vehicle vehicle)
    {
        if (!IsVehicleInGarage(vehicle.r_LicenseID))
        {
            m_Vehicles.Add(vehicle.r_LicenseID, vehicle);
        }
        else
        {
            //TODOL: throw exc
        }
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
        if (IsVehicleInGarage(i_LicenseID))
        {
            m_Vehicles[i_LicenseID].VehicleState = i_NewState;
        }
        else
        {
            //TODO: throw exception
        }
    }

    public void InflateTires(string i_LicenseID)
    {
        if (IsVehicleInGarage(i_LicenseID))
        {
            m_Vehicles[i_LicenseID].InflateTires();
        }
        else
        {
            //TODO: throw excpetion
        }
    }

    public void FillTank(string i_LicenseID, EnergySystem.FuelSystem.e_FuelType i_FuelType, float i_AmountOfFuelToAdd)
    {
        if (IsVehicleInGarage(i_LicenseID))
        {
            Vehicle vehicle = m_Vehicles[i_LicenseID];
            try
            {
                vehicle.FillTank(i_FuelType, i_AmountOfFuelToAdd);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //TODO :throw;
            }
        }
        else
        {
            //TODO: throw exception
        }
    }

    public void ChargeBattery(string i_LicenseID, float i_TimeToChargeInMinutes)
    {
        if (IsVehicleInGarage(i_LicenseID))
        {
            Vehicle vehicle = m_Vehicles[i_LicenseID];
            try
            {
                vehicle.ChargeBattery(i_TimeToChargeInMinutes);
            }
            catch (Exception e)
            {
                //TODO: throw exc
            }
        }
        else
        {
            //TODO: throw exception
        }
    }

    public List<string> GetDetails(string i_LicenseID)
    {
        List<string> listToReturn = new List<string>();

        if (IsVehicleInGarage(i_LicenseID))
        {
            Vehicle vehicle = m_Vehicles[i_LicenseID];
            listToReturn =  vehicle.GetDetails();
        }
        else
        {
            //TODO: throw exc
        }

        return listToReturn;
    }
}