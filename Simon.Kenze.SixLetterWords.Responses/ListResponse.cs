using System.Text;

namespace Simon.Kenze.SixLetterWords.Responses
{
    public class ListResponse<T>
    {
        public ListResponse()
        {
            ErrorMessages = new List<string>();
            Value = new List<T>();
        }

        public List<T> Value
        {
            get; set;
        }

        public List<string> ErrorMessages { get; set; }

        public bool IsSuccess => ErrorMessages.Count == 0;
    }
}