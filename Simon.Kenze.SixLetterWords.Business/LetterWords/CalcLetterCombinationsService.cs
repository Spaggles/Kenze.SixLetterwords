using Simon.Kenze.SixLetterWords.BusinessFacade;
using Simon.Kenze.SixLetterWords.Data;
using Simon.Kenze.SixLetterWords.Objects.Filters;
using Simon.Kenze.SixLetterWords.Responses;

namespace Simon.Kenze.SixLetterWords.Business.LetterWords
{
    public class CalcLetterCombinationsService : ICalcLetterCombinationsService
    {
        private readonly ILetterWordsRepository _letterWordsRepository;

        public CalcLetterCombinationsService(ILetterWordsRepository letterWordsRepository)
        {
            _letterWordsRepository = letterWordsRepository;
        }

        ListResponse<string> ICalcLetterCombinationsService.GetPossibleCombinationsOfTwo(CalcLetterCombinationsFilter filter)
        {
            ListResponse<string> response = new ListResponse<string>();
            List<string> result = new List<string>();

            if (filter.DesiredResultingCombinationStringLength <= 2)
            {
                response.ErrorMessages.Add("Please specify a DesiredResultingCombinationStringLength of atleast 2");
                return response;
            }

            List<string> input = _letterWordsRepository.Get();

            if (input == null || input.Count == 0)
            {
                response.ErrorMessages.Add("No usable input was supplied");
                return response;
            }

            // filter out input that is too long and filter out duplicates
            List<string> filteredInput = input
                .Where(x => x.Length <= filter.DesiredResultingCombinationStringLength)
                .Distinct()
                .ToList();

            List<string> fullWords = filteredInput.Where(x => x.Length == filter.DesiredResultingCombinationStringLength).ToList();

            List<string> partialWordOptions = filteredInput.Except(fullWords).ToList();

            foreach (string partialOption in partialWordOptions)
            {
                int lengthOption = filter.DesiredResultingCombinationStringLength - partialOption.Length;

                foreach (string otherPartialOption in partialWordOptions.Where(x => x.Length == lengthOption))
                {
                    if (fullWords.Any(x => x == (partialOption + otherPartialOption)))
                        result.Add($"{partialOption}+{otherPartialOption}={partialOption + otherPartialOption}");

                    if (fullWords.Any(x => x == (otherPartialOption + partialOption)))
                        result.Add($"{otherPartialOption}+{partialOption}={otherPartialOption + partialOption}");
                }
            }

            response.Value = result.Distinct().ToList();

            return response;
        }

        ListResponse<string> ICalcLetterCombinationsService.GetPossibleCombinationsOfX(CalcLetterCombinationsFilter filter)
        {
            ListResponse<string> response = new ListResponse<string>();
            List<string> result = new List<string>();

            if (filter.DesiredResultingCombinationStringLength <= 2)
            {
                response.ErrorMessages.Add("Please specify a DesiredResultingCombinationStringLength of atleast 2");
                return response;
            }

            List<string> input = _letterWordsRepository.Get();

            if (input == null || input.Count == 0)
            {
                response.ErrorMessages.Add("No usable input was supplied");
                return response;
            }

            // filter out input that is too long and filter out duplicates
            List<string> filteredInput = input
                .Where(x => x.Length <= filter.DesiredResultingCombinationStringLength)
                .Distinct()
                .ToList();

            List<string> fullWords = filteredInput.Where(x => x.Length == filter.DesiredResultingCombinationStringLength).ToList();

            List<string> partialWordOptions = filteredInput.Except(fullWords).ToList();

            foreach (string partialOption in partialWordOptions)
            {
                CalculateCombinationsRecursively(fullWords, partialWordOptions,
                    filter.DesiredResultingCombinationStringLength, new List<string> { partialOption },ref result);
            }

            response.Value = result.Distinct().ToList();

            return response;
        }

        public static void CalculateCombinationsRecursively(List<string> fullWords,
            List<string> partialWordOptions, int reqLength, List<string> currentCombination, ref List<string> result)
        {
            int currLength = string.Join("", currentCombination).Length;

            foreach (string partialOption in partialWordOptions.Where(x => (x.Length + currLength) <= reqLength))
            {
                foreach (string fullwordPartialMatch in fullWords.Where(x => x.Contains(String.Join("",currentCombination) + partialOption)))
                {
                    currentCombination.Add(partialOption);

                    int currLengthAfterMerge = string.Join("", currentCombination).Length;

                    if (currLengthAfterMerge == reqLength)
                    {
                        result.Add($"{String.Join("+", currentCombination)}={fullwordPartialMatch}");
                        return;
                    }

                    CalculateCombinationsRecursively(fullWords, partialWordOptions, reqLength, currentCombination, ref result);
                }
            }

            return;
        }
    }
}
