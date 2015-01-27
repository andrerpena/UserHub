using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace UserHub.Model.Helpers
{
    public static class RandomHelper
    {
        /// <summary>
        /// Retorna a lista informada, porém em ordem aleatória
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalList"></param>
        /// <param name="random"></param>
        /// <returns></returns>
        public static List<T> ShuffleList<T>(List<T> originalList, Random random)
        {
            if (originalList == null) throw new ArgumentNullException("originalList");
            if (random == null) throw new ArgumentNullException("random");

            return originalList.OrderBy(en => random.Next()).ToList();
        }

        /// <summary>
        /// Retorna um número aleatório
        /// </summary>
        /// <param name="min">Número mínimo (inclusivo)</param>
        /// <param name="max">Número máximo (inclusivo)</param>
        /// <param name="random"></param>
        /// <returns></returns>
        public static int GetRandomInt(int min, int max, Random random)
        {
            if (random == null) throw new ArgumentNullException("random");
            return random.Next(min, max + 1);
        }

        /// <summary>
        /// Retorna uma data aleatória, de hoje até daqui a maxDaysAhead dias
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="maxDaysAhead"></param>
        /// <param name="random"></param>
        /// <returns></returns>
        public static DateTimeOffset GetRandomDate(DateTimeOffset startDate, int maxDaysAhead, Random random)
        {
            if (random == null) throw new ArgumentNullException("random");

            var dias = GetRandomInt(0, maxDaysAhead, random);
            return startDate.AddDays(dias);
        }

        /// <summary>
        /// Retorna uma hora aleatória com minutos redondos
        /// </summary>
        /// <param name="minHour"></param>
        /// <param name="maxHour"></param>
        /// <param name="random"></param>
        /// <returns></returns>
        public static TimeSpan GetRandomTimeSpan(int minHour, int maxHour, Random random)
        {
            if (random == null) throw new ArgumentNullException("random");

            var hora = GetRandomInt(minHour, maxHour, random);
            var minuto = GetRandomInt(0, 5, random);
            return new TimeSpan(hora, minuto * 10, 0);
        }

        /// <summary>
        /// Retorna um objeto aleatório do banco de dados
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="random"></param>
        /// <returns></returns>
        public static T GetRandomObject<T>(DbContext context, Random random) where T : class
        {
            if (context == null) throw new ArgumentNullException("context");
            if (random == null) throw new ArgumentNullException("random");

            var set = context.Set<T>().ToList();
            var randomIndex = GetRandomInt(0, set.Count() - 1, random);
            return set.ElementAt(randomIndex);
        }

        /// <summary>
        /// Retorna um objeto aleatório de uma coleção
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="random"></param>
        /// <returns></returns>
        public static T GetRandomObject<T>(ICollection<T> collection, Random random)
        {
            if (collection == null) throw new ArgumentNullException("collection");
            if (random == null) throw new ArgumentNullException("random");

            var randomIndex = GetRandomInt(0, collection.Count() - 1, random);
            return collection.ElementAt(randomIndex);
        }

        /// <summary>
        /// Retorna uma lista de objetos aleatórios do banco de dados
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="random"></param>
        /// <returns></returns>
        public static List<T> GetRandomObjects<T>(DbContext context, int min, int max, Random random) where T : class
        {
            if (context == null) throw new ArgumentNullException("context");
            if (random == null) throw new ArgumentNullException("random");

            var number = GetRandomInt(min, max, random);
            var result = new List<T>();

            for (var i = 0; i < number; i++)
                result.Add(GetRandomObject<T>(context, random));

            return result;
        }

        public static string GetRandomText(int minWords, int maxWords, int minSentences, int maxSentences, int minParagraphs, int maxParagraphs, Random random)
        {
            if (random == null) throw new ArgumentNullException("random");

            var words = new[]{"lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
                "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
                "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"};

            // retorna uma palavra pelo índice
            Func<int, bool, string> getWord =
                (index, capital) => !capital ? words[index] : StringHelper.FirstCharToUpper(words[index]);

            var numSentences = random.Next(maxSentences - minSentences) + minSentences + 1;
            var numParagraphs = random.Next(maxParagraphs - minParagraphs) + minParagraphs + 1;
            var numWords = random.Next(maxWords - minWords) + minWords + 1;

            string result = String.Empty;

            for (int p = 0; p < numParagraphs; p++)
            {
                for (int s = 0; s < numSentences; s++)
                {
                    for (int w = 0; w < numWords; w++)
                    {
                        if (w > 0) { result += " "; }
                        result += getWord(random.Next(words.Length), w == 0);
                    }
                    result += ". ";
                }
                result += "/r/n/r/n";
            }

            return result;
        }
    }
}
