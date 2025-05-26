namespace Ex03.GarageLogic.GarageDB;

internal class GarageDb
{
    private const string k_EndOfFile = "*****";
    private const string k_FileName = "Vehicles.db";
    public List<List<string>> m_DbVehicles;
    
    public GarageDb()
    {
        m_DbVehicles = ReadLinesFromFile();
    }
    
    public static List<List<string>> ReadLinesFromFile()
    {
        List<List<string>> lines = new List<List<string>>();

        if (File.Exists(k_FileName))
        {
            try
            {
                List<string> lineFromFile;
                string[] allLines = File.ReadAllLines(k_FileName);

                for (int i = 0; i < allLines.Length; i++)
                {
                    string line = allLines[i];

                    if (line == k_EndOfFile)
                    {
                        break;
                    }
                    
                    lineFromFile = new List<string>(line.Split(','));
                    lines.Add(lineFromFile);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while reading the file: " + ex.Message, ex);
            }
        }
        else
        {
            throw new FileNotFoundException("The file was not found: " + k_FileName);
        }

        return lines;
    }
}