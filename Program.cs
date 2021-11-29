using System;

namespace Neuro_network
{
    class Program
    {
        static void Main(string[] args)
        {
            // Обучение
            //decimal km = 100;
            //var miles = 62.1371m;
            const decimal usd = 1;
            const decimal rub = 74.59m;
            var neuron = new Neuron(); 

            var i = 0;
            do
            {
                i++;
                neuron.Train(usd, rub);
                if (i % 1000000 == 0)
                { 
                    Console.WriteLine($"Итераций:{i}]\tОщтбка:\t{neuron.LastError}");
                }
            } while (neuron.LastError > neuron.Smoothing || neuron.LastError < - neuron.Smoothing);

            Console.WriteLine("Обучение завершенно");
            Console.WriteLine($"{neuron.ProcessInputData(50)} rub в {100} usd");

            Console.WriteLine($"{neuron.ProcessInputData(541)} rub в {541} usd");

            Console.WriteLine($"{neuron.RestoreInputDate(200m)} usd в  {200m} rub");
        }

        private class Neuron
        {
            private decimal weight = 0.5m; // Отвечает за вес единственного Inputa нейрона
            public decimal LastError { get; private set; }// Для вычисления ошибки 
            public decimal Smoothing { get; set; } = 0.00001m; // Сглаживаниеи результата
            // Мы берем входящие значение input т умножать его на вес
            public decimal ProcessInputData(decimal input) // Этод метод который будет  получать Input входящий сигнал из  вне  и который наш нейрон должен будет обработвть
            {
                return input * weight;
            }

            public decimal RestoreInputDate(decimal output)// Метод который делает обратный процесс
            {
                return output / weight;
            }

            public void Train(decimal input, decimal expectResult) //  actionResult Резульиат дейчтвия // expectResult Ожидаемый результат
            {
                var actionResult = input * weight;
                LastError = expectResult - actionResult;
                var correction = (LastError / actionResult) * Smoothing;// Умножаеи на сдшадивание точность будет больще но обучаться нерон будет дольще
                weight += correction;
            }
        }
    }
}
