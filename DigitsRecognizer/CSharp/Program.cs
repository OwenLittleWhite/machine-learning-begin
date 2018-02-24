using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp
{

    public class Observation
    {
        public Observation(string label, int[] pixels)
        {
            this.Label = label;
            this.Pixels = pixels;
        }
        public string Label { get; private set; }
        public int[] Pixels { get; private set; }
    }
    public class DataReader
    {
        private static Observation ObservationFactory(string data)
        {
            var commaSeparated = data.Split(',');
            var label = commaSeparated[0];
            var pixels =
                commaSeparated
                .Skip(1)
                .Select(x => Convert.ToInt32(x))
                .ToArray();
            return new Observation(label, pixels);
        }
        public static Observation[] ReadObservations(string dataPath)
        {
            var data =
                File.ReadAllLines(dataPath)
                .Skip(1)
                .Select(ObservationFactory)
                .ToArray();
            return data;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var distance = new ManhattanDistance();
            var classifier = new BasicClassifier(distance);
            var trainingPath = @"E:\personal\deepLearning\DigitsRecognizer\data\trainingsample.csv";
            var training = DataReader.ReadObservations(trainingPath);
            classifier.Train(training);

            var validationPath = @"E:\personal\deepLearning\DigitsRecognizer\data\validationsample.csv";
            var validation = DataReader.ReadObservations(validationPath);

            var correct = Evaluator.Correct(validation, classifier);
            Console.WriteLine("Correctly classified: {0:P2}", correct);
            Console.ReadLine();
        }
    }
}
