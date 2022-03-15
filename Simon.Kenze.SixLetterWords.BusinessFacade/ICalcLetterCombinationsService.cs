using Simon.Kenze.SixLetterWords.Objects.Filters;
using Simon.Kenze.SixLetterWords.Responses;

namespace Simon.Kenze.SixLetterWords.BusinessFacade
{
    public interface ICalcLetterCombinationsService
    {
        public ListResponse<string> GetPossibleCombinationsOfTwo(CalcLetterCombinationsFilter filter);
        public ListResponse<string> GetPossibleCombinationsOfX(CalcLetterCombinationsFilter filter);
    }
}