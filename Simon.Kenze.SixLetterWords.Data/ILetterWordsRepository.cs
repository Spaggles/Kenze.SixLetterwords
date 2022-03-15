using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simon.Kenze.SixLetterWords.Data
{
    public interface ILetterWordsRepository
    {
        List<string> Get();
    }
}
