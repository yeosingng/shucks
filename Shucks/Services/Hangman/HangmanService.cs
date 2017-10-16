using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shucks.Services.Hangman
{
    public class HangmanService
    {
        bool gameOngoing;

        string currentWord;
        List<Char> currentWordProgress;
        HashSet<Char> currentGuesses;
        HangmanWordGenerator wordGenerator;

        int numOfGuesses;
        int correctIndexes;

        public HangmanService()
        {
            wordGenerator = new HangmanWordGenerator();
            currentGuesses = new HashSet<char>();
            currentWordProgress = new List<char>();
        }

        public String processCommands(String guess)
        {
            if (gameOngoing != true) 
            {
                return initializeHangmanGame();
            }
            else
            {
                return handleGuesses(guess);
            }
        }

        private String initializeHangmanGame()
        {
            // find word 
            currentGuesses.Clear();
            currentWordProgress.Clear();
            gameOngoing = true;
            currentWord = wordGenerator.GenerateRandomWord();
            correctIndexes = 0;
            numOfGuesses = 5;

            setUpCurrentWordProgress();

            return formatCurrentWordProgressToReturn();
        }

        private void setUpCurrentWordProgress()
        {
            int size = 0;
            while (size < currentWord.Length)
            {
                currentWordProgress.Add('_');
                size++;
            }
        }

        private String handleGuesses(String guess)
        {
            if ( (guess.Length == 1) && (currentGuesses.Contains(Char.Parse(guess))) )
            {
               return "That letter has already been guessed";
            }
            else if (guess == currentWord)
            {
                correctIndexes = currentWord.Length;
            }
            else if (guess.Length != 1)
            {
                numOfGuesses--;
            }
            else
            {
                checkAndUpdateCurrentGuesses(Char.Parse(guess));
            }
            return generateMessageDependingOnGameEnd();
        }

        private void checkAndUpdateCurrentGuesses(char guess)
        {
            currentGuesses.Add(guess);
            bool correct = false;
            int index = 0;
            foreach (char charInWord in currentWord)
            {
                if (guess == charInWord)
                {
                    correct = true;
                    currentWordProgress[index] = charInWord;
                    correctIndexes++;
                }
                index++;
            }

            if (!correct)
            {
                numOfGuesses--;
            }
        }

        private String generateMessageDependingOnGameEnd()
        {
            if (correctIndexes == currentWord.Length)
            {
                gameOngoing = false;
                return String.Format("Nice, the word is: {0}", currentWord);
            }
            else if (numOfGuesses == 0)
            {
                gameOngoing = false;
                return String.Format("Nah, the word was: {0}", currentWord);
            }

            return "" + formatCurrentWordProgressToReturn();
        }

        private string formatCurrentWordProgressToReturn()
        {
            string retStr = "";
            foreach (char c in currentWordProgress)
            {
                if (c.Equals('_'))
                {
                    retStr += "\\_ ";
                } else
                {
                    retStr += c;
                }
            }
            return String.Format("There are {0} guesses left\n\n", numOfGuesses) + retStr;
        }

        public string guesses()
        {
            if (currentGuesses.ToArray().Length != 0)
            {
                return formatGuesses();
            }
            else
            {
                return "There are no guesses so far";
            }
        }

        private string formatGuesses()
        {
            string retStr = "";
            foreach (char c in currentGuesses.ToArray())
            {
                retStr += String.Format(" {0}", c);
            }
            return retStr;
        }
    }
}
