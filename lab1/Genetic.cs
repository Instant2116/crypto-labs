using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lab1
{

    class Genetic
    {
        static private char[] CryptoLetters =
       {
            'A', 'B', 'C', 'D', 'E', 'F', 'G',
            'H', 'I', 'J', 'K', 'L', 'M', 'N',
            'O', 'P', 'Q', 'R', 'S', 'T', 'U',
            'V', 'W', 'X', 'Y', 'Z'
            };
        private int lettersCount = 26;
        private Dictionary<string, double> trigramDictionary = new Dictionary<string, double>();
        private char[] CharContext;
        private string CharDecrypt;
        private int mutationProbability = 80;

        public Genetic(String Context)
        {
            CharContext = Context.ToCharArray();
        }

        public void getTrigram()
        {
            var text = File.ReadAllLines(@"D:\_vs2019\repos\crypto-labs\lab1\results\trigrams.txt");
            double sum = 0;
            var count = string.Empty;
            var threeLetter = string.Empty;
            double value = 0;
            foreach (var line in text)
            {
                int border = line.Length;
                count = line[4..border];
                value = Convert.ToDouble(count);
                sum += value;
            }

            foreach (var line in text)
            {
                threeLetter = line[0..3];
                int border = line.Length;
                count = line[4..border];
                value = Convert.ToDouble(count) / sum;
                sum += value;
                trigramDictionary.Add(threeLetter.ToUpper(), value);
            }
        }

        public string GeneticDecrypt()
        {
            int generation = 1;
            var chromosomes = GenerateStartPopulation(1000);
            double bestIndividual;
            char[] bestish = new char[lettersCount];
            while (generation < 30)
            {
                chromosomes = OneHundredBestChromosomes(chromosomes);
                bestish = chromosomes[0];
                bestIndividual = FitnesFunction(bestish);
                CharDecrypt = SubstitutionCipher(CharContext, bestish);
                Console.WriteLine($"\nOriginal:{ new string(CryptoLetters)}\nEncriptionKey: { new string(bestish)}\nGeneration: {generation} - BestIndividual: {bestIndividual}");
                Crossover(ref chromosomes);
                Mutation(ref chromosomes);
                generation++;

            }
            return CharDecrypt;
        }

        private string helper(char[] bestish)
        {
            string a = new string(bestish);
            a = Swap(a, 'K', 'I');
            var res = a.ToArray();
            a = new String(res);
            return SubstitutionCipher(CharContext, res);
        }

        private string Swap(string s, char a, char b)
        {
            return new string(s.Select(ch => (ch == a) ? b : (ch == b) ? a : ch).ToArray());
        }

        private void Mutation(ref List<char[]> chromosomes)
        {

            foreach (var chromosome in chromosomes)
            {
                Random random = new Random();
                var percentMutation = random.Next(100);
                if (percentMutation <= mutationProbability)
                {
                    var mutateGen1 = RandGen(chromosome);
                    var mutateGen2 = RandGen(chromosome);
                    if (mutateGen1 == mutateGen2)
                    {
                        mutateGen2++;
                    }

                    (chromosome[mutateGen1], chromosome[mutateGen2]) = (chromosome[mutateGen2], chromosome[mutateGen1]);
                }
            }
        }

        private int RandGen(char[] chromosome)
        {
            Random random = new Random();
            var mutateGen = random.Next(0, lettersCount - 1);
            return mutateGen;
        }

        private void Crossover(ref List<char[]> chromosomes)
        {
            Random random = new Random();

            var children = new List<char[]>();
            for (var i = 0; i < chromosomes.Count; i++)
            {
                for (int j = 0; j < 100; j++) //special place
                {
                    var Mother = chromosomes[i];
                    var Father = chromosomes[random.Next(0, 99)];
                    var child = new char[Mother.Length];
                    var gens = CryptoLetters.ToList();
                    bool queue = true;
                    for (var k = 0; k < Mother.Length; k++)
                    {
                        var isContainMotherGen = child.Contains(Mother[k]);
                        var isContainFatherGen = child.Contains(Father[k]);

                        if (isContainMotherGen && isContainFatherGen)
                        {
                            var randomGen = gens[random.Next(0, gens.Count)];
                            child[k] = randomGen;
                            gens.Remove(randomGen);
                        }
                        else if (isContainMotherGen)
                        {
                            child[k] = Father[k];
                            gens.Remove(Father[k]);
                        }
                        else if (isContainFatherGen)
                        {
                            child[k] = Mother[k];
                            gens.Remove(Mother[k]);
                        }
                        else
                        {
                            if (queue)
                            {
                                child[k] = Mother[k];
                                gens.Remove(Mother[k]);
                                queue = false;
                            }
                            else
                            {
                                child[k] = Father[k];
                                gens.Remove(Father[k]);
                                queue = true;
                            }

                        }

                    }
                    children.Add(child);
                }
            }
            chromosomes = children;
        }


        private List<char[]> OneHundredBestChromosomes(List<char[]> chromosomes)
        {
            double[] bestGen = new double[chromosomes.Count];
            List<char[]> chromosomesTemp = chromosomes;
            for (int i = 0; i < chromosomes.Count; i++)
            {
                bestGen[i] = FitnesFunction(chromosomes[i]);

            }
            SortTop(ref bestGen, ref chromosomesTemp);
            chromosomesTemp.RemoveRange(100, chromosomesTemp.Count - 100);

            return chromosomesTemp;
        }

        private void SortTop(ref double[] bestGen, ref List<char[]> chromosomes)
        {
            bool sorted;
            for (int i = 0; i < bestGen.Length - 1; i++)
            {
                sorted = true;
                for (int j = bestGen.Length - 1; j > i; j--)
                {

                    if (bestGen[j] < bestGen[j - 1])
                    {
                        double temp = bestGen[j - 1];
                        char[] tempChar = chromosomes[j - 1];
                        bestGen[j - 1] = bestGen[j];
                        chromosomes[j - 1] = chromosomes[j];
                        bestGen[j] = temp;
                        chromosomes[j] = tempChar;
                        sorted = false;
                    }
                }
                if (sorted)
                    break;
            }
            Array.Reverse(bestGen);
            chromosomes.Reverse();
        }


        private double FitnesFunction(char[] chromosome)
        {
            var decryptionOption = SubstitutionCipher(CharContext, chromosome);
            double percent = 0;
            for (int i = 0; i < decryptionOption.Length - 2; i++)
            {
                var threeLetters = decryptionOption.Substring(i, 3);
                if (trigramDictionary.ContainsKey(threeLetters))
                {
                    percent += trigramDictionary[threeLetters];
                }
            }
            var percentSimilarity = percent / (decryptionOption.Length - 2);

            return percentSimilarity;
        }


        private string SubstitutionCipher(char[] StrangeText, char[] chromosome)
        {
            var TempDecript = new char[CharContext.Length];
            for (int i = 0; i < StrangeText.Length; i++)
            {
                var letter = StrangeText[i];
                string chromosomeStr = new string(chromosome);
                var indexOfChange = chromosomeStr.IndexOf(letter);
                TempDecript[i] = CryptoLetters[indexOfChange];
            }
            return new string(TempDecript);
        }

        private List<char[]> GenerateStartPopulation(int numberOfStart)
        {
            var ChromosomeSet = new List<char[]>();

            Random random = new Random();
            for (int i = 0; i < numberOfStart; i++)
            {
                List<char> tempLetterList = CryptoLetters.ToList();
                var chromosome = new char[lettersCount];
                for (int j = 0; j < CryptoLetters.Length; j++)
                {
                    var RandLetter = tempLetterList[random.Next(0, tempLetterList.Count)];
                    chromosome[j] = RandLetter;
                    tempLetterList.Remove(RandLetter);
                }
                ChromosomeSet.Add(chromosome);

            }
            return ChromosomeSet;
        }
    }
}






