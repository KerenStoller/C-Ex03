namespace Ex03.GarageLogic;

public class GarageDb
{
    private const string k_EndOfFile = "*****";
    private const string k_FileName = "GarageDB.db";
    public List<List<string>> m_DbVehicles;
    
    //TODO - get all vehicles in db, create them with vehicle creator and save them to vehicles
    public GarageDb()
    {
        m_DbVehicles = ReadLinesFromFile();
    }
    
    public static List<List<string>> ReadLinesFromFile()
    {
        //TODO: use readalllines
        List<List<string>> lines = new List<List<string>>();

        if (File.Exists(k_FileName))
        {
            using (StreamReader reader = new StreamReader(k_FileName))
            {
                string? line;
                List<string> lineFromFile;
                
                while ((line = reader.ReadLine()) != null)
                {
                    if (line == k_EndOfFile)
                    {
                        break;
                    }
                    
                    lineFromFile = new List<string>(line.Split(',', StringSplitOptions.TrimEntries));
                    lines.Add(lineFromFile);
                }
            }
            //TODO: add try catch
        }
        else
        {
            //TODO: throw exc
        }

        return lines;
    }
}