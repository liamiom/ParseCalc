namespace ParseCalc;

public static class SaveFile
{
    public static bool ContentChanged { get; set; }

    private static string _savePath = "";
    private static string SavePath
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_savePath))
            {
                _savePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Parse\\";
                if (!Directory.Exists(SavePath))
                {
                    Directory.CreateDirectory(SavePath);
                }
            }

            return _savePath;
        }
    }

    public static string[] SaveFileList => 
        Directory.GetFiles(SavePath).Select(i => i.Replace(SavePath, "").Replace(".sav", "")).OrderByDescending(i => i).ToArray();

    private static readonly string _tadSplit = "##TabEnd##";

    public static void Save(TabControl tabControl1)
    {
        if (!ContentChanged)
        {
            return;
        }

        string fileName = SavePath + DateTime.Now.ToString("yyyy-MMM-dd_hh-mm-ss") + ".sav";
        string[] saveItems = tabControl1
            .Items
            .Select(i => (i.Tag?.ToString() ?? "") + _tadSplit)
            .ToArray();

        File.WriteAllLines(fileName, saveItems);
    }

    public static void Load(string fileName, ref TabControl tabControl1)
    {
        fileName = SavePath + fileName + ".sav";
        string savedText = File.ReadAllText(fileName);
        List<string> saveFileData = savedText.Replace(_tadSplit, "¬").Split('¬').ToList();
        tabControl1.LoadSaveFile(saveFileData);
        ContentChanged = false;
    }
}
