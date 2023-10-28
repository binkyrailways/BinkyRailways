namespace BinkyRailways.Import
{
    public class ImportMessage
    {
        public ImportMessage(string message, string path)
        {
            Path = path;
            Message = message;
        }

        public string Message { get; private set; }
        public string Path { get; private set; }
    }
}
