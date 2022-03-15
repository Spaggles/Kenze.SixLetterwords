using System.Reflection;

namespace Simon.Kenze.SixLetterWords.Data
{
    public class LetterWordsRepository : ILetterWordsRepository
    {
        public List<string> Get()
        {
            string fileInput = String.Empty;

            using (var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("Simon.Kenze.SixLetterWords.Data.input.txt")))
            {
                fileInput = reader.ReadToEnd();
            }

            List<string> splitInput = fileInput.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList();

            return splitInput;
        }
    }
}